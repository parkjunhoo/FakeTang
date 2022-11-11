using Data;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class UI_Merchant : UI_Popup
{
    HashSet<int> _pickedNums = new HashSet<int>();

    public enum Texts
    {
        ItemBtnPriceText1,
        ItemBtnPriceText2,
    }
    public enum Buttons
    {
        ItemBtn1,
        ItemBtn2,

        CloseBtn,

        ConfirmBtn,
        CancelBtn,
    }
    public enum Images
    {
        ItemBtnIcon1,
        ItemBtnIcon2,
    }
    public enum GameObjects
    {
    }
    public override void Init()
    {
        base.Init();
        Bind<TextMeshProUGUI>(typeof(Texts));
        Bind<Button>(typeof(Buttons));
    }

    ItemInfo PickRandomItem()
    {
        Dictionary<string, Data.ItemInfo> _itemDict = Managers.Data.ItemInfoDict;
        int rand = Random.Range(0, _pickedNums.Count);
        string[] items = Enum.GetNames(typeof(Define.Item));
        return _itemDict[items[rand]];
    }


}
