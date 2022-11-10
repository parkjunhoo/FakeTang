using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public static MonsterSpawner Instance;


    [SerializeField]
    int _monsterCount = 0;

    public static int MonsterCount { get { return Instance._monsterCount; } set { Instance._monsterCount = value; } }
    [SerializeField]
    int _reserveCount = 0;

    [SerializeField]
    int _keepMonsterCount = 10;

    //Vector3 _spawnPos;

    [SerializeField]
    float _spawnRadius = 5.0f;

    [SerializeField]
    float _spawnTime = 10.0f;

    Transform playerTransform;

    void Start()
    {
        Instance = this;
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }


    void FixedUpdate()
    {
        while (_reserveCount + _monsterCount < _keepMonsterCount)
        {
            StartCoroutine(ReserveSpawn());
        }
    }

    IEnumerator ReserveSpawn()
    {
        _reserveCount++;
        yield return new WaitForSeconds(Random.Range(0, _spawnTime));
        


        Vector3 playerPosition = playerTransform.position;

        float a = playerPosition.x;
        float b = playerPosition.y;

        float x = Random.Range(-_spawnRadius + a, _spawnRadius + a);
        float y_b = Mathf.Sqrt(Mathf.Pow(_spawnRadius, 2) - Mathf.Pow(x - a, 2));
        y_b *= Random.Range(0, 2) == 0 ? -1 : 1;
        float y = y_b + b;

        //GameObject obj = Managers.Resource.Instantiate("Monsters/Flyingeye");
        GameObject obj = Managers.Game.Spawn(Define.WorldObject.Monster, "Monsters/Flyingeye");
        Vector3 randPos = new Vector3(x, y, 0);
        obj.transform.position = randPos;

        _monsterCount++; //Test
        _reserveCount--;
    }
}