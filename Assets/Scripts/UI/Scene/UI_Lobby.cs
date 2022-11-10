using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Lobby : UI_Scene
{
    //나중에는 서버에서 받아와야함
    int _level = 4;
    int _maxExp = 100;
    int _exp = 4;

    int _heart = 10;
    int _maxHeart = 30;

    int _diamond = 10;

    int _gold = 10;

    //서버에서 넘어온 데이터라고 가정
    static int _clicked = 2;
   // public Action BtnClicked = null;

    public int Clicked { get { return _clicked; } set { _clicked = value; Pagination(); } }

    public enum Texts
    {
        LevelText,
        HeartText,
        DiamondText,
        GoldText,
    }

    public enum Buttons
    {
        NavBtn1,
        NavBtn2,
        NavBtn3,
        NavBtn4,
        NavBtn5,
        StartBtn,
    }
    public enum GameObjects
    {
        View1,
        View2,
        View3,
        View4,
        View5,

        ExpBar,
        Fill,
    }

    public override void Init()
    {
        base.Init();
        //BtnClicked -= OnBtn;
        //BtnClicked += OnBtn;
        Bind<TextMeshProUGUI>(typeof(Texts));
        Bind<Button>(typeof(Buttons));
        Bind<GameObject>(typeof(GameObjects));

        Pagination();


        for (int i = 0; i < 5; i++)
        {
            int a = i;
            BindEvent(GetButton(i).gameObject, (PointerEventData data) =>
            {
                Clicked = a;
                //BtnClicked.Invoke();
            });
        }
        BindEvent(GetButton((int)Buttons.StartBtn).gameObject, (PointerEventData data) =>
        {
            Managers.Scene.LoadScene(Define.Scene.Game);
        });
        Refresh();
    }

    public void Refresh()
    {
        Get<GameObject>((int)GameObjects.ExpBar).GetComponent<Slider>().value = ExpBarRatio(_exp, _maxExp);
        Get<TextMeshProUGUI>((int)Texts.LevelText).text = _level.ToString();
        Get<TextMeshProUGUI>((int)Texts.HeartText).text = $"{_heart}/{_maxHeart}";
        Get<TextMeshProUGUI>((int)Texts.DiamondText).text = _diamond.ToString();
        Get<TextMeshProUGUI>((int)Texts.GoldText).text = _gold.ToString();
    }

    public void Pagination()
    {
        for (int i = 0; i < 5; i++)
        {
            if (i == _clicked) Get<GameObject>(i).SetActive(true);
            else Get<GameObject>(i).SetActive(false);
        }
    }

    float ExpBarRatio(int exp , int maxExp)
    {
        float cal = (float)exp / (float)maxExp;
        if (cal < 0.1) Get<GameObject>((int)GameObjects.Fill).SetActive(false);
        else Get<GameObject>((int)GameObjects.Fill).SetActive(true);
        return cal;
    }

    void OnBtn()
    {
        Pagination();
    }


}
