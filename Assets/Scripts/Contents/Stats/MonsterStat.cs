using Data;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterStat : Stat
{
    string monsterName;
    [SerializeField]
    int _exp = 0;
    int _gold = 1;
    public int Exp
    {
        get { return _exp; }
    }
    public int Gold
    {
        get { return _gold; }
    }
    private void Awake()
    {
        Spawner.OnMonsterLevelUp -= OnLevelUp;
        Spawner.OnMonsterLevelUp += OnLevelUp;
        monsterName = transform.root.name;
        int index = monsterName.IndexOf("(Clone)");
        if (index > 0) monsterName = monsterName.Substring(0, index);
    }

    void OnLevelUp()
    {
        SetStat(Spawner.MonsterLevel);
    }
    public void SetStat(int level, bool levelup = false)
    {

        switch (monsterName)
        {
            case "Flyingeye":
                Dictionary<int, Data.FlyingeyeStat> flyingeyeDict = Managers.Data.FlyingeyeDict;
                Data.FlyingeyeStat flyingeyeStat = flyingeyeDict[level];
                _level = flyingeyeStat.level;
                if(!levelup)_hp = flyingeyeStat.hp;
                _maxHp = flyingeyeStat.maxHp;
                _attack = flyingeyeStat.attack;
                _defense = flyingeyeStat.defense;
                _moveSpeed = flyingeyeStat.moveSpeed;
                _exp = flyingeyeStat.exp;
                _gold = flyingeyeStat.gold;
                break;

            case "Goblin":
                Dictionary<int, Data.GoblinStat> goblinDict = Managers.Data.GoblinDict;
                Data.GoblinStat goblinStat = goblinDict[level];
                _level = goblinStat.level;
                if (!levelup) _hp = goblinStat.hp;
                _maxHp = goblinStat.maxHp;
                _attack = goblinStat.attack;
                _defense = goblinStat.defense;
                _moveSpeed = goblinStat.moveSpeed;
                _exp = goblinStat.exp;
                _gold = goblinStat.gold;
                break;
        }
    }

}
