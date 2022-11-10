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

    int _monsterLevel = 1;
    public static int MonsterLevel { get { return Instance._monsterLevel; } set { Instance._monsterLevel = value; } }


    [SerializeField]
    int _reserveCount = 0;

    [SerializeField]
    int _keepMonsterCount = 15;

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

        GameObject obj;
        //if (Utill.RandomInHundred(10))
        //{
        //    obj = Managers.Game.Spawn(Define.WorldObject.Monster, "Monsters/Goblin");
        //}
        //else obj = Managers.Game.Spawn(Define.WorldObject.Monster, "Monsters/Flyingeye");
        int r = Random.Range(0, 101);
        switch (r)
        {
            case <= 10:
                obj = Managers.Game.Spawn(Define.WorldObject.Monster, "Monsters/Goblin");
                break;

            default:
                obj = Managers.Game.Spawn(Define.WorldObject.Monster, "Monsters/Flyingeye");
                break;
        }
        

        obj.GetComponent<MonsterStat>().SetStat(_monsterLevel);
        Vector3 randPos = new Vector3(x, y, 0);
        obj.transform.position = randPos;

        _monsterCount++; 
        _reserveCount--;
    }
}