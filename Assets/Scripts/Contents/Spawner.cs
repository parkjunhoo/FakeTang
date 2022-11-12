using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public static Spawner Instance;

    [SerializeField]
    int _merchantPercentage = 0;
    float _merchantSpawnRadius = 12.0f;
    bool isMerchantDelay = false;

    [SerializeField]
    int _monsterCount = 0;
    public static int MonsterCount { get { return Instance._monsterCount; } set { Instance._monsterCount = value; } }

    int _monsterLevel = 1;
    public static int MonsterLevel { get { return Instance._monsterLevel; } set { Instance._monsterLevel = value; } }


    [SerializeField]
    int _reserveCount = 0;

    [SerializeField]
    int _keepMonsterCount = 15;
    [SerializeField]
    float _spawnRadius = 5.0f;
    [SerializeField]
    float _spawnTime = 10.0f;
    Transform playerTransform;

    UI_Hud Hud;
    void Start()
    {
        Instance = this;
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }


    void FixedUpdate()
    {
        if (!isMerchantDelay)
        {
            isMerchantDelay = true;
            _merchantPercentage++;
            if (_merchantPercentage > 100) _merchantPercentage = 100;
            if (Utill.RandomInHundred(_merchantPercentage))
            {
                _merchantPercentage = 0;
                SpawnMerchant();
            }
            StartCoroutine(MerchantCount());
        }
        
        
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

    IEnumerator MerchantCount()
    {
        yield return new WaitForSeconds(10f);
        isMerchantDelay = false;
    }


    void SpawnMerchant()
    {
        Vector3 playerPosition = playerTransform.position;

        float a = playerPosition.x;
        float b = playerPosition.y;

        float x = Random.Range(-_merchantSpawnRadius + a, _merchantSpawnRadius + a);
        float y_b = Mathf.Sqrt(Mathf.Pow(_merchantSpawnRadius, 2) - Mathf.Pow(x - a, 2));
        y_b *= Random.Range(0, 2) == 0 ? -1 : 1;
        float y = y_b + b;
        Vector3 randPos = new Vector3(x, y, 0);


        GameObject go = Managers.Game.Spawn(Define.WorldObject.Monster, "Merchants/Merchant");

        go.transform.position = randPos;
    }

}