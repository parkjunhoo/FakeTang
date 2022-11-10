using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_HPBar : UI_Base
{
    enum GameObjects
    {
        HPBar,
    }


    public override void Init()
    {
        Bind<GameObject>(typeof(GameObjects));
    }

    private void LateUpdate()
    {

        Transform parent = transform.parent;
        Stat parentStat = parent.GetComponent<Stat>();
        transform.position = parent.position + Vector3.up * (parent.GetComponent<Collider2D>().bounds.size.y);
        transform.rotation = Camera.main.transform.rotation;

        float HPRatio = (float)parentStat.Hp / (float)parentStat.MaxHp;

        SetHpRatio(HPRatio);
    }

    public void SetHpRatio(float ratio)
    {

        GetObject((int)GameObjects.HPBar).GetComponent<Slider>().value = ratio;
    }
}
