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
        while (_pickedSkill.Count < 3) //picked ��ų�� 3���� �ɶ����� ����
        {
            SkillInfo _skillInfo = PickRandomSkill();
            if (_skillInfo.Name == null) {  break; } //null���� ��ȯ��ٴ°� ���̻� �������ִ� ��ų�� ������ break
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
        //����ü ������ ���� ���� ����
        SkillInfo skillInfo;
        string name = "";
        string subText = "";
        int level;

        //��� ��ų ����Ʈ��
        var allActiveSkills = Enum.GetValues(typeof(Define.ActiveSkill)); 


        // �����ν�ų ���������� ����Ʈ
        List<int> maxLevelActiveSkills = new List<int>();
        foreach (var entry in _activeSkillTree)
        {
            if (_activeSkillTree[entry.Key] == 5) maxLevelActiveSkills.Add(entry.Key);
        }

        //������ �ִ� ��ų ����Ʈ
        List<int> activeSkillOptions = new List<int>(); 
        for (int i = 0; i < allActiveSkills.Length; i++)
        {
            bool exclude = false;
            for (int o = 0; o < maxLevelActiveSkills.Count; o++)
            {
                if ((maxLevelActiveSkills[o] == (int)allActiveSkills.GetValue(i))) { exclude = true; break; }
            }
            if (!exclude) activeSkillOptions.Add((int)allActiveSkills.GetValue(i)); // ������ų����Ʈ�� ��罺ų����Ʈ�� ���� ������ų�� ���� �ִ´�.
        }

        if (activeSkillOptions.Count == 0 || (activeSkillOptions.Count - _pickedSkill.Count) < 1) return new SkillInfo(); // �������ִ� ��ų�� ���ٸ� �� ����ü�� ��ȯ

        int random = Random.Range(0, activeSkillOptions.Count); //�������ִ� ��ų����Ʈ���� random�� ���� �̴´�.

        int RandomKey = activeSkillOptions[random]; //�������ִ� ��ų����Ʈ���� random�� �ε����� �̾ƿ´�.

        if (_activeSkillTree.TryGetValue(RandomKey, out level)) level++; //�÷��̾ ����ִ� ��ų�̶�� ������ų ������ ++
        else level = 1; //����������� ��ų�̶�� ������ 1��

        switch (RandomKey) //���� RandomKey�� ��ų���� StatData�� ��ų��������ü�� ��Ḧ �غ��Ѵ�.
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
        skillInfo = new SkillInfo(RandomKey, name, subText, level); //���� ���� ��ų��������ü ����


        return skillInfo; // ��ų���� ����ü ��ȯ
    }
}
