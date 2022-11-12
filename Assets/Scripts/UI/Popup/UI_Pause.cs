using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class UI_Pause : UI_Popup
{
    GameObject _player;
    static int _pickedTab=0;
    


    public enum Texts
    {
        LevelText,
        HPText,
        AttackText,
        MoveSpeedText,
        DrainText,
        ExpText,
    }
    TextMeshProUGUI LevelText, HPText, AttackText, MoveSpeedText, DrainText, ExpText;
    public enum Buttons
    {
        ResumeBtn,
        HomeBtn,

        SkillTabBtn,
        ItemTabBtn,
        StatTabBtn,
    }
    GameObject SkillTabBtn, ItemTabBtn, StatTabBtn;

    public enum Images
    {
        Skill1,
        Skill2,
        Skill3,
        Skill4,
        Skill5,
    }
    Image Skill1, Skill2, Skill3, Skill4, Skill5;
    Image[] Skills;
    public enum GameObjects
    {
        SkillList,
        ItemList,
        StatList,
        ItemListContent,
    }
    GameObject SkillList, ItemList, StatList;
    GameObject ItemListContent;
    public override void Init()
    {
        base.Init();
        _player = Managers.Game.Player;
        Bind<Button>(typeof(Buttons));
        Bind<Image>(typeof(Images));
        Bind<GameObject>(typeof(GameObjects)) ;
        Bind<TextMeshProUGUI>(typeof(Texts));
        SkillTabBtn = GetButton((int)Buttons.SkillTabBtn).gameObject;
        ItemTabBtn = GetButton((int)Buttons.ItemTabBtn).gameObject;
        StatTabBtn = GetButton((int)Buttons.StatTabBtn).gameObject;
        ItemListContent = GetObject((int)GameObjects.ItemListContent);
        SkillList = GetObject((int)GameObjects.SkillList);
        ItemList = GetObject((int)GameObjects.ItemList);
        StatList = GetObject((int)GameObjects.StatList);

        LevelText = GetMeshText((int)Texts.LevelText);
        HPText = GetMeshText((int)Texts.HPText);
        AttackText = GetMeshText((int)Texts.AttackText);
        MoveSpeedText = GetMeshText((int)Texts.MoveSpeedText);
        DrainText = GetMeshText((int)Texts.DrainText);
        ExpText = GetMeshText((int)Texts.ExpText);



        BindEvent(GetButton((int)Buttons.ResumeBtn).gameObject, (PointerEventData) =>
        {
            Managers.UI.ClosePopupUI();
        });
        BindEvent(GetButton((int)Buttons.HomeBtn).gameObject, (PointerEventData) =>
        {
            Managers.UI.CloseAllPopupUI();
            Managers.Scene.LoadScene(Define.Scene.Lobby);
        });

        SkillList.SetActive(false);
        ItemList.SetActive(false);
        StatList.SetActive(false);

        if (_pickedTab == 0) SkillList.SetActive(true);
        if (_pickedTab == 1) ItemList.SetActive(true);
        if (_pickedTab == 2) StatList.SetActive(true);


        BindEvent(SkillTabBtn, (PointerEventData) =>
        {
            SkillList.SetActive(true);
            ItemList.SetActive(false);
            StatList.SetActive(false);

            _pickedTab = 0;

        });
        BindEvent(ItemTabBtn, (PointerEventData) =>
        {
            SkillList.SetActive(false);
            ItemList.SetActive(true);
            StatList.SetActive(false);

            _pickedTab = 1;
        });
        BindEvent(StatTabBtn, (PointerEventData) =>
        {
            SkillList.SetActive(false);
            ItemList.SetActive(false);
            StatList.SetActive(true);

            _pickedTab = 2;
        });


        Skill1 = GetImage((int)Images.Skill1);
        Skill2 = GetImage((int)Images.Skill2);
        Skill3 = GetImage((int)Images.Skill3);
        Skill4 = GetImage((int)Images.Skill4);
        Skill5 = GetImage((int)Images.Skill5);
        Skills = new Image[] { Skill1, Skill2, Skill3, Skill4, Skill5 };


        

        Refresh();
    }

    public void Refresh()
    {
        PlayerStat _playerStat = _player.GetComponent<PlayerStat>();
        Dictionary<int, int> _activeSkillTree = _player.GetComponent<PlayerController>().ActiveSkillTree;
        int i = 0;
        foreach (var entry in _activeSkillTree)
        {
            Skills[i].sprite = Managers.Resource.Load<Sprite>($"Sprites/Skills/{Enum.GetName(typeof(Define.ActiveSkill), entry.Key)}/icon");
            i++;
        }

        Dictionary<string, int> _itemTree = _player.GetComponent<PlayerController>().ItemTree;
        foreach (var entry in _itemTree)
        {
            Managers.UI.MakeSubItem<UI_ItemSlot>(ItemListContent.transform).SetInfo(entry.Key, entry.Value);
        }


        LevelText.text = $"Level:{_playerStat.Level}";
        HPText.text = $"HP:{_playerStat.Hp}/{_playerStat.MaxHp}";
        AttackText.text = $"Attack:{_playerStat.Attack}+{_playerStat.ExtraAttack}";
        MoveSpeedText.text = $"MoveSpeed:{_playerStat.MoveSpeed}¡¿{_playerStat.ExtraSpeed}";
        DrainText.text = $"Drain:{_playerStat.Drain}";
        ExpText.text = $"EXP:{_playerStat.Exp}/{_playerStat.MaxExp}";

    }

}
