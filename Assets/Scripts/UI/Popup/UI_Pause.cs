using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class UI_Pause : UI_Popup
{
    GameObject _player;
    Image ActiveSkill1, ActiveSkill2, ActiveSkill3, ActiveSkill4, ActiveSkill5;
    Image PassiveSkill1, PassiveSkill2, PassiveSkill3, PassiveSkill4, PassiveSkill5;
    Image[] ActiveSkills;
    Image[] PassiveSkills;


    public enum Texts
    {
    }
    public enum Buttons
    {
        ResumeBtn,
        HomeBtn,
    }
    public enum Images
    {
        ActiveSkill1,
        ActiveSkill2,
        ActiveSkill3,
        ActiveSkill4,
        ActiveSkill5,

        //PassiveSkill1,
        //PassiveSkill2,
        //PassiveSkill3,
        //PassiveSkill4,
        //PassiveSkill5,

    }
    public enum GameObjects
    {
    }
    public override void Init()
    {
        base.Init();
        _player = GameObject.FindGameObjectWithTag("Player");
        Bind<Button>(typeof(Buttons));
        Bind<Image>(typeof(Images));
        BindEvent(GetButton((int)Buttons.ResumeBtn).gameObject, (PointerEventData) =>
        {
            Managers.UI.ClosePopupUI();
        });
        BindEvent(GetButton((int)Buttons.HomeBtn).gameObject, (PointerEventData) =>
        {
            Managers.UI.CloseAllPopupUI();
            Managers.Scene.LoadScene(Define.Scene.Lobby);
        });

        ActiveSkill1 = GetImage((int)Images.ActiveSkill1);
        ActiveSkill2 = GetImage((int)Images.ActiveSkill2);
        ActiveSkill3 = GetImage((int)Images.ActiveSkill3);
        ActiveSkill4 = GetImage((int)Images.ActiveSkill4);
        ActiveSkill5 = GetImage((int)Images.ActiveSkill5);

        //PassiveSkill1 = GetImage((int)Images.PassiveSkill1);
        //PassiveSkill2 = GetImage((int)Images.PassiveSkill2);
        //PassiveSkill3 = GetImage((int)Images.PassiveSkill3);
        //PassiveSkill4 = GetImage((int)Images.PassiveSkill4);
        //PassiveSkill5 = GetImage((int)Images.PassiveSkill5);

        ActiveSkills = new Image[] { ActiveSkill1, ActiveSkill2, ActiveSkill3, ActiveSkill4, ActiveSkill5 };
        //PassiveSkills = new Image[] { PassiveSkill1, PassiveSkill2, PassiveSkill3, PassiveSkill4, PassiveSkill5 };


        Refresh();

    }

    public void Refresh()
    {
        Dictionary<int, int> _activeSkillTree = _player.GetComponent<PlayerController>().ActiveSkillTree;
        foreach (var entry in _activeSkillTree)
        {
            int i = 0;
            
            ActiveSkills[i].sprite = Managers.Resource.Load<Sprite>($"Sprites/Skills/{Enum.GetName(typeof(Define.ActiveSkill), entry.Key)}/icon");
            i++;
        }
    }
}
