using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerController;

namespace Data
{
    #region Stat
    [Serializable]
    public class Stat
    {
        public int level;
        public int hp;
        public int maxHp;
        public int attack;
        public int defense;
        public float moveSpeed;
    }

    [Serializable]
    public class StatData : ILoader<int, Stat>
    {
        public List<Stat> stats = new List<Stat>();

        public Dictionary<int, Stat> MakeDict()
        {
            Dictionary<int, Stat> dict = new Dictionary<int, Stat>();
            foreach (Stat stat in stats) dict.Add(stat.level, stat);
            return dict;
        }
    }
    #endregion

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
}