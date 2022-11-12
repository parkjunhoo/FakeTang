using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Hud : UI_Scene
{
    GameObject _player;
    PlayerStat _playerStat;
    float _maxExp;
    float _exp;
    int _gold;

    public static UI_Hud Instance;

    int _timerTime = 0;
    public static int TimerTime { get { return Instance._timerTime; } }

    public enum Texts
    {
        GoldInfoText,
        KillCountInfoText,
        TimerText,
        LevelText,
    }
    TextMeshProUGUI goldInfoText;
    TextMeshProUGUI killCountText;
    TextMeshProUGUI timerText;
    TextMeshProUGUI levelText;
    public enum Buttons
    {
        PauseBtn,
    }
    public enum GameObjects
    {
        ExpBar,
        Fill,
    }
    Slider expBar;
    GameObject fill;

    public override void Init()
    {
        base.Init();
        Instance = this;
        _player = Managers.Game.Player;
        _playerStat = _player.GetComponent<PlayerStat>();
        //////////////////////////////////////////////////////
        Bind<TextMeshProUGUI>(typeof(Texts));
        Bind<Button>(typeof(Buttons));
        Bind<GameObject>(typeof(GameObjects));
        //////////////////////////////////////////////////////
        goldInfoText = GetMeshText((int)Texts.GoldInfoText);
        killCountText = GetMeshText((int)Texts.KillCountInfoText);
        timerText = GetMeshText((int)Texts.TimerText);
        levelText = GetMeshText((int)Texts.LevelText);
        //////////////////////////////////////////////////////
        expBar = GetObject((int)GameObjects.ExpBar).GetComponent<Slider>();
        fill = GetObject((int)GameObjects.Fill);
        //////////////////////////////////////////////////////


        BindEvent(GetButton((int)Buttons.PauseBtn).gameObject, (PointerEventData) =>
        {
            Managers.UI.ShowPopupUI<UI_Pause>();
        });

        RefreshAll();

        StartCoroutine(TimerStart());
    }

    IEnumerator TimerStart()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(1f);
            if (GameScene.GameMode == Define.GameMode.Playing)
            {
                _timerTime++;
                string min = (_timerTime / 60).ToString("00");
                string sec = (_timerTime % 60).ToString("00");
                timerText.text = string.Format($"{min}:{sec}");
                RefreshAll();
            }
            
        }
    }

    float ExpBarRatio(int exp , int maxExp)
    {
        float cal = (float)exp / (float)maxExp;
        if (cal < 0.05) fill.SetActive(false);
        else fill.SetActive(true);
        return cal;
    }


    public void RefreshAll()
    {
        RefreshExpBar();
        RefreshGold();
        RefreshKillingCount();
        RefreshLevel();
    }
    public void RefreshExpBar()
    {
         expBar.value =  ExpBarRatio(_playerStat.Exp , _playerStat.MaxExp);
    }
    public void RefreshGold()
    {
        goldInfoText.text = _playerStat.Gold.ToString();
    }
    public void RefreshKillingCount()
    {
        killCountText.text = _playerStat.KillingCount.ToString();
    }
    public void RefreshLevel()
    {
        levelText.text = _playerStat.Level.ToString();
    }


}
