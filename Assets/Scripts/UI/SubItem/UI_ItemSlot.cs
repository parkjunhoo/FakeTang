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

public class UI_ItemSlot : UI_Base
{
    public enum Images
    {
        ItemIcon,
    }
    public enum Texts
    {
        CountText,
    }
    Image ItemIcon;
    TextMeshProUGUI CountText;
    public void SetInfo(string itemName, int count)
    {
        Bind<Image>(typeof(Images));
        Bind<TextMeshProUGUI>(typeof(Texts));
        ItemIcon = GetImage((int)Images.ItemIcon);
        CountText = GetMeshText((int)Texts.CountText);

        Debug.Log(itemName);
        Debug.Log(Managers.Resource.Load<GameObject>($"Prefabs/Items/{itemName}"));

        ItemIcon.sprite = Managers.Resource.Load<GameObject>($"Prefabs/Items/{itemName}").GetComponent<SpriteRenderer>().sprite;
        CountText.text = count.ToString();
    }
    public override void Init()
    {
        
    }
}
