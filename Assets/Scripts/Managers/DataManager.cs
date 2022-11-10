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
    public Dictionary<int, Data.Stat> StatDict { get; private set; } = new Dictionary<int, Data.Stat>();
    public Dictionary<int, Data.PlayerStat> PlayerStatDict { get; private set; } = new Dictionary<int, Data.PlayerStat>();



    public Dictionary<int, Data.EnergyBoltStat> EnergyBoltStatDict { get; private set; } = new Dictionary<int, Data.EnergyBoltStat>();
    public Dictionary<int, Data.UnnamedSkillStat> UnnamedSkillStatDict { get; private set; } = new Dictionary<int, Data.UnnamedSkillStat>();

    public void Init()
    {
        StatDict = LoadJson<Data.StatData, int, Data.Stat>("StatData").MakeDict();
        PlayerStatDict = LoadJson<Data.PlayerStatData, int, Data.PlayerStat>("PlayerStatData").MakeDict();
        EnergyBoltStatDict = LoadJson<Data.EnergyBoltStatData , int, Data.EnergyBoltStat>("EnergyBoltStatData").MakeDict();
        UnnamedSkillStatDict = LoadJson<Data.UnnamedSkillStatData, int, Data.UnnamedSkillStat>("UnnamedSkillStatData").MakeDict();
    }

    Loader LoadJson<Loader, Key, Value>(string path) where Loader : ILoader<Key, Value>
    {
        TextAsset textAsset = Managers.Resource.Load<TextAsset>($"Data/{path}");
        return JsonUtility.FromJson<Loader>(textAsset.text);
    }
}
