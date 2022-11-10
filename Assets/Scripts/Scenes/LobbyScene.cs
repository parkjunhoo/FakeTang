using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyScene : BaseScene
{
    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Login;

        Managers.Sound.Play("Sounds/Bgm",Define.Sound.Bgm);
    }
    public override void Clear()
    {

    }

    private void Update()
    {
    }
}
