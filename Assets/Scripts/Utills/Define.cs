using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define : MonoBehaviour
{

    public enum WorldObject
    {
        Unknown,
        Player,
        Monster,
    }
    public enum Scene
    {
        Unkown,
        Login,
        Lobby,
        Game,
    }

    public enum Sound
    {
        Bgm,
        Effect,
        MaxCount,
    }
    public enum UIEvent
    {
        Click,
        Drag,
        Enter,
        Exit,
    }

    public enum GameMode
    {
        Playing,
        Pause,
        GameOver,
    }

    public enum Layer
    {
        Player = 3,
        Skill = 6,
        Monster = 7,
    }

    public enum ActiveSkill
    {
        EnergyBolt=0,
        UnnamedSkill=1,
    }
}
