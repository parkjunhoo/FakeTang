using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Setellite : Skill
{
    Vector3 radius;
    Vector3 offset;

    int _maxCount=1;
    protected override void Init()
    {
        base.Init();
        transform.position += new Vector3(AttackRange, AttackRange, 0f);
        
        offset = transform.position - Player.transform.position;
    }

    public override void SetStat(int level = 1)
    {
        Dictionary<int, Data.SetelliteStat> dict = Managers.Data.SetelliteStatDict;
        Data.SetelliteStat stat = dict[level];
        _level = stat.level;
        _attack = stat.attack;
        _moveSpeed = stat.moveSpeed;
        _attackRange = stat.attackRange;
        _maxCount = stat.maxCount;
    }



    private void FixedUpdate()
    {
        transform.position = Player.transform.position + offset;
        transform.RotateAround(Player.transform.position, Vector3.forward, _moveSpeed * Time.fixedDeltaTime);
        offset = transform.position - Player.transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
            Managers.Resource.Destroy(gameObject);
            Player.GetComponent<PlayerController>().setelliteCount--;
            collision.gameObject.GetComponent<MonsterController>().OnDamaged(Damage);
        }
    }
}
