using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerController;

public class LoopedMap : MonoBehaviour
{

    float joyX { get { return Managers.Input.joyX; } }
    float joyY { get { return Managers.Input.joyY; } }

    float halfSightX;
    float halfSightY;
    Camera _camera;

    float unitSizeX = 14f;
    float unitSizeY = 24f;


    [SerializeField]
    GameObject[] tileMaps;
    // 0 3 6 
    // 1 4 7
    // 2 5 8

    Vector2[] border;

    private void Start()
    {
        _camera = Camera.main;
        halfSightY = unitSizeY*0.5f;
        halfSightX = unitSizeX*0.5f;
        //halfSightY = _camera.orthographicSize*1.5f;
        //halfSightX = halfSightY * _camera.aspect*1.5f;
       
        border = new Vector2[]
        {
            new Vector2(-unitSizeX * 1.5f , unitSizeY * 1.5f), //-21 , 36
            new Vector2(unitSizeX * 1.5f , -unitSizeY * 1.5f) // 21, -36
        };
    }


    void Update()
    {
        if (joyX == 0 && joyY == 0) return;

        Check();
    }
    void Check()
    {
        if (border[1].x < _camera.transform.position.x + halfSightX)
        {
            border[0] += Vector2.right * unitSizeX;
            border[1] += Vector2.right * unitSizeX;
            Move(0);
        }
        else if (border[0].x > _camera.transform.position.x - halfSightX)
        {
            border[0] -= Vector2.right * unitSizeX;
            border[1] -= Vector2.right * unitSizeX;
            Move(2);
        }
        else if (border[0].y < _camera.transform.position.y + halfSightY)
        {
            border[0] += Vector2.up * unitSizeY;
            border[1] += Vector2.up * unitSizeY;
            Move(1);
        }
        else if (border[1].y > _camera.transform.position.y - halfSightY)
        {
            border[0] -= Vector2.up * unitSizeY;
            border[1] -= Vector2.up * unitSizeY;
            Move(3);
        }
    }

    void Move(int dir)
    {
        
        GameObject[] _tileMaps = new GameObject[9];
        System.Array.Copy(tileMaps, _tileMaps, 9);

        switch (dir)
        {
            case 0:
                for (int i = 0; i< 9; i++)
                {
                    int revise = i - 3;
                    if (revise < 0)
                    {
                        tileMaps[9 + revise] = _tileMaps[i];
                        _tileMaps[i].transform.position += Vector3.right * unitSizeX * 3;
                    }
                    else tileMaps[revise] = _tileMaps[i];
                }
                break;

            case 1:
                for (int i =0; i<9; i++)
                {
                    int revise = i % 3;
                    if (revise == 2)
                    {
                        tileMaps[i - 2] = _tileMaps[i];
                        _tileMaps[i].transform.position += Vector3.up * unitSizeY * 3;
                    }
                    else tileMaps[i + 1] = _tileMaps[i];
                }
                break;

            case 2:
                for (int i = 0; i<9; i++)
                {
                    int revise = i + 3;
                    if (revise > 8)
                    {
                        tileMaps[revise - 9] = _tileMaps[i];
                        _tileMaps[i].transform.position -= Vector3.right * unitSizeX * 3;
                    }
                    else tileMaps[revise] = _tileMaps[i];
                }
                break;

            case 3:
                for (int i =0; i<9; i++)
                {
                    int revise = i % 3;
                    if (revise == 0)
                    {
                        tileMaps[i + 2] = _tileMaps[i];
                        _tileMaps[i].transform.position -= Vector3.up * unitSizeY * 3;
                    }
                    else tileMaps[i - 1] = _tileMaps[i];
                }
                break;
        }
    }
}
