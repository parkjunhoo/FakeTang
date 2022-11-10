using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UnnamedSkill : Skill
{
    int penerationCount = 3;
    protected override void Init()
    {
        base.Init();
    }

    public override void SetStat(int level = 1)
    {
        Dictionary<int, Data.UnnamedSkillStat> dict = Managers.Data.UnnamedSkillStatDict;
        Data.UnnamedSkillStat stat = dict[level];

        _level = stat.level;
        _attack = stat.attack;
        _moveSpeed = stat.moveSpeed;
        _attackRange = stat.attackRange;
    }

    private void FixedUpdate()
    {
        TraceEnemy();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
            penerationCount--;
            if (penerationCount < 0) Managers.Resource.Destroy(gameObject);
            collision.gameObject.GetComponent<MonsterController>().OnDamaged(Damage);
        }
    }
}
