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
        monsterName = transform.root.name;
        int index = monsterName.IndexOf("(Clone)");
        if (index > 0) monsterName = monsterName.Substring(0, index);
    }

    public void SetStat(int level)
    {
        
        switch (monsterName)
        {
            case "Flyingeye":
                Dictionary<int, Data.FlyingeyeStat> dict = Managers.Data.FlyingeyeDict;
                Data.FlyingeyeStat stat = dict[level];
                _level = stat.level;
                _hp = stat.hp;
                _maxHp = stat.maxHp;
                _attack = stat.attack;
                _defense = stat.defense;
                _moveSpeed = stat.moveSpeed;
                _exp = stat.exp;
                _gold = stat.gold;
                break;
        }

        
    }
    
}
