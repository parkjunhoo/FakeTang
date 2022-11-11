using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static PlayerController;

namespace Data
{
    #region PlayerStat

    #region PlayerStat
    [Serializable]
    public class PlayerStat
    {
        public int level;
        public int hp;
        public int maxHp;
        public int attack;
        public int defense;
        public float moveSpeed;
        public int maxExp;
    }

    [Serializable]
    public class PlayerStatData : ILoader<int, PlayerStat>
    {
        public List<PlayerStat> playerStats = new List<PlayerStat>();

        public Dictionary<int, PlayerStat> MakeDict()
        {
            Dictionary<int, PlayerStat> dict = new Dictionary<int, PlayerStat>();
            foreach (PlayerStat playerStat in playerStats) dict.Add(playerStat.level, playerStat);
            return dict;
        }
    }
    #endregion

    #endregion

    #region MonsterStat

    #region FlyingeyeStat
    [Serializable]
    public class FlyingeyeStat
    {
        public int level;
        public int hp;
        public int maxHp;
        public int attack;
        public int defense;
        public float moveSpeed;
        public int exp;
        public int gold;
    }

    [Serializable]
    public class FlyingeyeStatData : ILoader<int, FlyingeyeStat>
    {
        public List<FlyingeyeStat> flyingeyeStats = new List<FlyingeyeStat>();

        public Dictionary<int, FlyingeyeStat> MakeDict()
        {
            Dictionary<int, FlyingeyeStat> dict = new Dictionary<int, FlyingeyeStat>();
            foreach (FlyingeyeStat stat in flyingeyeStats) dict.Add(stat.level, stat);
            return dict;
        }
    }
    #endregion

    #region GoblinStat
    [Serializable]
    public class GoblinStat
    {
        public int level;
        public int hp;
        public int maxHp;
        public int attack;
        public int defense;
        public float moveSpeed;
        public int exp;
        public int gold;
    }

    [Serializable]
    public class GoblinStatData : ILoader<int, GoblinStat>
    {
        public List<GoblinStat> goblinStats = new List<GoblinStat>();

        public Dictionary<int, GoblinStat> MakeDict()
        {
            Dictionary<int, GoblinStat> dict = new Dictionary<int, GoblinStat>();
            foreach (GoblinStat stat in goblinStats) dict.Add(stat.level, stat);
            return dict;
        }
    }
    #endregion

    #endregion



    #region SkillStat

    #region EnergyBoltStat
    [Serializable]
    public class EnergyBoltStat
    {
        public int level;
        public float attack;
        public float coolTime;
        public float moveSpeed;
        public float attackRange;
        public string subText;
    }

    [Serializable]
    public class EnergyBoltStatData : ILoader<int, EnergyBoltStat>
    {
        public List<EnergyBoltStat> energyBoltStats = new List<EnergyBoltStat>();

        public Dictionary<int, EnergyBoltStat> MakeDict()
        {
            Dictionary<int, EnergyBoltStat> dict = new Dictionary<int, EnergyBoltStat>();
            foreach (EnergyBoltStat stat in energyBoltStats) dict.Add(stat.level, stat);
            return dict;
        }
    }
    #endregion

    #region UnnamedSkillStat
    [Serializable]
    public class UnnamedSkillStat
    {
        public int level;
        public float attack;
        public float coolTime;
        public float moveSpeed;
        public float attackRange;
        public string subText;
    }

    [Serializable]
    public class UnnamedSkillStatData : ILoader<int, UnnamedSkillStat>
    {
        public List<UnnamedSkillStat> unnamedSkillStats = new List<UnnamedSkillStat>();

        public Dictionary<int, UnnamedSkillStat> MakeDict()
        {
            Dictionary<int, UnnamedSkillStat> dict = new Dictionary<int, UnnamedSkillStat>();
            foreach (UnnamedSkillStat stat in unnamedSkillStats) dict.Add(stat.level, stat);
            return dict;
        }
    }
    #endregion

    #region SetelliteStat
    [Serializable]
    public class SetelliteStat
    {
        public int level;
        public float attack;
        public float coolTime;
        public float moveSpeed;
        public float attackRange;
        public int maxCount;
        public string subText;
    }

    [Serializable]
    public class SetelliteStatData : ILoader<int, SetelliteStat>
    {
        public List<SetelliteStat> setelliteStats = new List<SetelliteStat>();

        public Dictionary<int, SetelliteStat> MakeDict()
        {
            Dictionary<int, SetelliteStat> dict = new Dictionary<int, SetelliteStat>();
            foreach (SetelliteStat stat in setelliteStats) dict.Add(stat.level, stat);
            return dict;
        }
    }
    #endregion

    #endregion


    #region ItemData

    #region ItemData
    [Serializable]
    public class ItemInfo
    {
        public string code;
        public string name;
        public string subText;
        public string value;
    }


    #region Value Class
    public class Shoes
    {
        [JsonProperty("moveSpeed")]
        public float moveSpeed { get; set; }
    }
    public class Sword
    {
        [JsonProperty("attack")]
        public int attack { get; set; }
    }
    #endregion


    [Serializable]
    public class ItemInfoData : ILoader<string, ItemInfo>
    {
        public List<ItemInfo> itemInfos = new List<ItemInfo>();

        public Dictionary<string, ItemInfo> MakeDict()
        {
            Dictionary<string, ItemInfo> dict = new Dictionary<string, ItemInfo>();
            foreach (ItemInfo itemData in itemInfos) dict.Add(itemData.name, itemData);
            return dict;
        }
    }
    #endregion

    #endregion
}