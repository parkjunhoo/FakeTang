using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEngine.EventSystems.EventTrigger;
using Random = UnityEngine.Random;

public class UI_SelectSkillBtn : UI_Base
{
    public enum Texts
    {
        SelectSkillBtnText,
        SelectSkillBtnSubText,
    }
    public enum Images
    {
        SelectSkillBtnIcon,
        Level1,
        Level2,
        Level3,
        Level4,
        Level5,
    }
    Image[] LevelImages = new Image[5];


    int _index;
    string _name;
    string _subText;
    int _level;
    public void SetInfo(int index)
    {
        UI_SelectSkill rootParent = FindObjectOfType<UI_SelectSkill>();
        _index = rootParent._pickedSkill[index].Index;
        _name = rootParent._pickedSkill[index].Name;
        _subText = rootParent._pickedSkill[index].SubText;
        _level = rootParent._pickedSkill[index].Level;
    }
    public override void Init()
    {
        Bind<TextMeshProUGUI>(typeof(Texts));
        Bind<Image>(typeof(Images));

        LevelImages[0] = GetImage((int)Images.Level1);
        LevelImages[1] = GetImage((int)Images.Level2);
        LevelImages[2] = GetImage((int)Images.Level3);
        LevelImages[3] = GetImage((int)Images.Level4);
        LevelImages[4] = GetImage((int)Images.Level5);

        Dictionary<int, int> _activeSkillTree = Managers.Game.Player.GetComponent<PlayerController>().ActiveSkillTree;
        if (name != null)
        {
            GetMeshText((int)Texts.SelectSkillBtnText).text = _name;
            GetMeshText((int)Texts.SelectSkillBtnSubText).text = _subText;
            GetImage((int)Images.SelectSkillBtnIcon).sprite = Managers.Resource.Load<Sprite>($"Sprites/Skills/{_name}/icon");
            BindEvent(gameObject, (PointerEventData) =>
            {
                _activeSkillTree[_index] = _level;
                Managers.UI.ClosePopupUI();
            });
            for(int i =0; i<_level; i++)
            {
                LevelImages[i].sprite = Resources.LoadAll<Sprite>("Sprites/UI/GUI")[24];
            }
        }
        else
        {
            BindEvent(gameObject, (PointerEventData) =>
            {
                Managers.UI.ClosePopupUI();
            });
        }
    }
}
