using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public interface ILoader<Key, Value>
{
    Dictionary<Key, Value> MakeDict();
}

public class DataManager
{

    #region PlayerDict
    public Dictionary<int, Data.PlayerStat> PlayerStatDict { get; private set; } = new Dictionary<int, Data.PlayerStat>();
    #endregion

    #region MonsterDict
    public Dictionary<int, Data.FlyingeyeStat> FlyingeyeDict { get; private set; } = new Dictionary<int, Data.FlyingeyeStat>();
    #endregion


    #region SkillDict
    public Dictionary<int, Data.EnergyBoltStat> EnergyBoltStatDict { get; private set; } = new Dictionary<int, Data.EnergyBoltStat>();
    public Dictionary<int, Data.UnnamedSkillStat> UnnamedSkillStatDict { get; private set; } = new Dictionary<int, Data.UnnamedSkillStat>();
    public Dictionary<int, Data.SetelliteStat> SetelliteStatDict { get; private set; } = new Dictionary<int, Data.SetelliteStat>();
    #endregion

    public void Init()
    {
        PlayerStatDict = LoadJson<Data.PlayerStatData, int, Data.PlayerStat>("PlayerStatData").MakeDict();
        EnergyBoltStatDict = LoadJson<Data.EnergyBoltStatData , int, Data.EnergyBoltStat>("EnergyBoltStatData").MakeDict();
        UnnamedSkillStatDict = LoadJson<Data.UnnamedSkillStatData, int, Data.UnnamedSkillStat>("UnnamedSkillStatData").MakeDict();
        SetelliteStatDict = LoadJson<Data.SetelliteStatData, int, Data.SetelliteStat>("SetelliteStatData").MakeDict();
        FlyingeyeDict = LoadJson<Data.FlyingeyeStatData, int, Data.FlyingeyeStat>("FlyingeyeStatData").MakeDict();
    }

    Loader LoadJson<Loader, Key, Value>(string path) where Loader : ILoader<Key, Value>
    {
        TextAsset textAsset = Managers.Resource.Load<TextAsset>($"Data/{path}");
        return JsonUtility.FromJson<Loader>(textAsset.text);
    }
}
