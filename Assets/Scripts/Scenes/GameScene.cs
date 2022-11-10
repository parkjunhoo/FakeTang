
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{
    public static GameScene Instance;

    Define.GameMode _gameMode = Define.GameMode.Playing;
    public static Define.GameMode GameMode { get { return Instance._gameMode; } set { Instance._gameMode = value; } }


    private void Update()
    {
        switch (_gameMode)
        {
            case Define.GameMode.Pause:
                Time.timeScale = 0f;
                if (Managers.UI.PopupStackCount == 0) _gameMode = Define.GameMode.Playing;
                break;

            case Define.GameMode.Playing:
                Time.timeScale = 1f;
                if (Managers.UI.PopupStackCount > 0) _gameMode = Define.GameMode.Pause;
                break;
        }
    }
    protected override void Init()
    {
        base.Init();
        Instance = this;

        SceneType = Define.Scene.Game;
        Application.targetFrameRate = 60;
        Time.timeScale = 1f;

        Managers.Game.Spawn(Define.WorldObject.Player, "Players/Knight");

        Managers.Resource.Instantiate("UI/Scene/UI_Hud");
        GameObject Joystick = Managers.Resource.Instantiate("UI/SubItem/Joystick/FloatingJoystick");
        Joystick.transform.SetParent(GameObject.FindGameObjectWithTag("Hud").transform, false);
        Joystick.transform.SetAsFirstSibling();

    }
    public override void Clear()
    {
        
    }
}
