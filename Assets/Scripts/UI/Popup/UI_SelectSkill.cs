using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class UI_SelectSkill : UI_Popup
{
    public struct SkillInfo
    {
        public int Index;
        public string Name;
        public string SubText;
        public int Level;


        public SkillInfo(int index, string name, string subText, int level)
        {
            Index = index;
            Name = name;
            SubText = subText;
            Level = level;
        }

    }
    GameObject _player;
    Dictionary<int, int> _activeSkillTree;
    public List<SkillInfo> _pickedSkill = new List<SkillInfo>();




    public enum Texts
    {
        NoticeText,
    }
    public enum GameObjects
    {
        SkillList,
    }
    public override void Init()
    {
        base.Init();
        _player = GameObject.FindGameObjectWithTag("Player");
        _activeSkillTree = _player.GetComponent<PlayerController>().ActiveSkillTree;
        while (_pickedSkill.Count < 3) //picked 스킬이 3개가 될때까지 뽑음
        {
            SkillInfo _skillInfo = PickRandomSkill();
            if (_skillInfo.Name == null) {  break; } //null값이 반환됬다는건 더이상 뽑을수있는 스킬이 없으니 break
            _pickedSkill.Add(_skillInfo);
            for(int i=0; i<_pickedSkill.Count; i++)
            {
                for(int x =i+1; x<_pickedSkill.Count; x++)
                {
                    if ((_pickedSkill.Count - x) < 0) break;
                    if (_pickedSkill[i].Name == _pickedSkill[x].Name) _pickedSkill.RemoveAt(x);
                }
            }
        }
        

        Bind<GameObject>(typeof(GameObjects));
        GameObject skillList = Get<GameObject>((int)GameObjects.SkillList);
        for (int i = 0; i < _pickedSkill.Count; i++)
        {
            Managers.UI.MakeSubItem<UI_SelectSkillBtn>(skillList.transform).SetInfo(i);
        }

    }



    SkillInfo PickRandomSkill()
    {
        //구조체 생성을 위한 변수 선언
        SkillInfo skillInfo;
        string name = "";
        string subText = "";
        int level;

        //모든 스킬 리스트업
        var allActiveSkills = Enum.GetValues(typeof(Define.ActiveSkill)); 


        // 만랩인스킬 구별을위한 리스트
        List<int> maxLevelActiveSkills = new List<int>();
        foreach (var entry in _activeSkillTree)
        {
            if (_activeSkillTree[entry.Key] == 5) maxLevelActiveSkills.Add(entry.Key);
        }

        //뽑을수 있는 스킬 리스트
        List<int> activeSkillOptions = new List<int>(); 
        for (int i = 0; i < allActiveSkills.Length; i++)
        {
            bool exclude = false;
            for (int o = 0; o < maxLevelActiveSkills.Count; o++)
            {
                if ((maxLevelActiveSkills[o] == (int)allActiveSkills.GetValue(i))) { exclude = true; break; }
            }
            if (!exclude) activeSkillOptions.Add((int)allActiveSkills.GetValue(i)); // 만랩스킬리스트와 모든스킬리스트를 비교후 만랩스킬을 빼고 넣는다.
        }

        if (activeSkillOptions.Count == 0 || (activeSkillOptions.Count - _pickedSkill.Count) < 1) return new SkillInfo(); // 뽑을수있는 스킬이 없다면 빈 구조체를 반환

        int random = Random.Range(0, activeSkillOptions.Count); //뽑을수있는 스킬리스트에서 random한 값을 뽑는다.

        int RandomKey = activeSkillOptions[random]; //뽑을수있는 스킬리스트에서 random을 인덱스로 뽑아온다.

        if (_activeSkillTree.TryGetValue(RandomKey, out level)) level++; //플레이어가 들고있는 스킬이라면 뽑은스킬 레벨을 ++
        else level = 1; //들고있지않은 스킬이라면 레벨을 1로

        switch (RandomKey) //뽑은 RandomKey로 스킬별로 StatData를 스킬인포구조체의 재료를 준비한다.
        {
            case (int)Define.ActiveSkill.EnergyBolt:
                Dictionary<int, Data.EnergyBoltStat> energyBoltDict = Managers.Data.EnergyBoltStatDict;
                Data.EnergyBoltStat EnergyBoltStat = energyBoltDict[level];
                name = "EnergyBolt";
                subText = EnergyBoltStat.subText;
                break;

            case (int)Define.ActiveSkill.UnnamedSkill:
                Dictionary<int, Data.UnnamedSkillStat> unnamedSkillDict = Managers.Data.UnnamedSkillStatDict;
                Data.UnnamedSkillStat unnamedSkillStat = unnamedSkillDict[level];
                name = "UnnamedSkill";
                subText = unnamedSkillStat.subText;
                break;

            case (int)Define.ActiveSkill.Setellite:
                Dictionary<int, Data.SetelliteStat> setelliteDict = Managers.Data.SetelliteStatDict;
                Data.SetelliteStat setelliteStat = setelliteDict[level];
                name = "Setellite";
                subText = setelliteStat.subText;
                break;
        }
        skillInfo = new SkillInfo(RandomKey, name, subText, level); //뽑은 재료로 스킬인포구조체 생성


        return skillInfo; // 스킬인포 구조체 반환
    }
}
