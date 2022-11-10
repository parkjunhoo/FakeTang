using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandDeco : MonoBehaviour
{
    void Start()
    {
        for(int x = -6; x<6; x++)
        {
            for(int y = 7; y>-7; y--)
            {
                if (Random.Range(1, 100) < 3)
                {
                    GameObject go = Managers.Resource.Instantiate($"Maps/Decoration/Plant/Grass{Random.Range(0, 14)}", gameObject.transform);
                    go.transform.position = transform.position + new Vector3(x, y, 0);
                }
                else if (Random.Range(1, 100) < 3)
                {
                    GameObject go = Managers.Resource.Instantiate("Maps/Decoration/Plant/Grass15", gameObject.transform);
                    go.transform.position = transform.position + new Vector3(x, y, 0);
                }
            }
        }
    }
}
