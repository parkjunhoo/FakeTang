using Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : Stat
{
    [SerializeField]
    int _exp = 0;
    [SerializeField]
    int _maxExp;
    [SerializeField]
    int _gold = 0;
    [SerializeField]
    int _killingCount=0;

    float _extraSpeed = 1f;


    public int Exp
    {
        get { return _exp; }
        set
        {
            _exp = value;
            while (_exp >= _maxExp) LevelUp();

        }
    }


    public float ExtraSpeed { get { return _extraSpeed; } set { _extraSpeed = value; } }
    public int MaxExp { get { return _maxExp; } set { _maxExp = value; } }
    public int Gold { get { return _gold; } set { _gold = value; } }
    public int KillingCount { get { return _killingCount; } set { _killingCount = value; } }


    public void Awake()
    {
        SetStat(1);
        _exp = 0;
        _gold = 0;
    }
    public void SetStat(int level)
    {
        Dictionary<int, Data.PlayerStat> dict = Managers.Data.PlayerStatDict;
        Data.PlayerStat stat = dict[level];
        _level = stat.level;
        _hp = stat.hp;
        _maxHp = stat.maxHp;
        _attack = stat.attack;
        _defense = stat.defense;
        _moveSpeed = stat.moveSpeed;
        _maxExp = stat.maxExp;
    }

    public void LevelUp()
    {
        _exp -= _maxExp;
        Data.PlayerStat playerStat;
        bool nextLevelCheck = Managers.Data.PlayerStatDict.TryGetValue(Level + 1, out playerStat);
        if (nextLevelCheck) { SetStat(Level + 1); StartCoroutine(PopupDelay()); }
        else Level++;
        
    }

    IEnumerator PopupDelay()
    {
        yield return new WaitForSeconds(1f);
        Managers.UI.ShowPopupUI<UI_SelectSkill>();
    }
}
