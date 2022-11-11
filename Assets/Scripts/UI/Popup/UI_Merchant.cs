using Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;
using Image = UnityEngine.UI.Image;
using Random = UnityEngine.Random;

public class UI_Merchant : UI_Popup
{
    HashSet<int> _pickedNums = new HashSet<int>();
    Dictionary<string, Data.ItemInfo> _itemDict = new Dictionary<string, ItemInfo>();
    ItemInfo[] _pickedItems = new ItemInfo[2];

    public ItemInfo SelectItem;

    
    GameObject _player;
    PlayerStat _playerStat;
    PlayerController _playerController;

    

    


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
    GameObject itemBtn1;
    GameObject itemBtn2;

    GameObject ConfirmBtn;
    GameObject CancelBtn;
    public enum Images
    {
        ItemBtnIcon1,
        ItemBtnIcon2,
    }
    public enum GameObjects
    {
        ConfirmBackground
    }
    GameObject confirmPopup;
    public override void Init()
    {
        base.Init();
        Bind<TextMeshProUGUI>(typeof(Texts));
        Bind<Button>(typeof(Buttons));
        Bind<Image>(typeof(Images));
        Bind<GameObject>(typeof(GameObjects));
        _player = GameObject.FindGameObjectWithTag("Player");
        _playerStat = _player.GetComponent<PlayerStat>();
        _playerController = _player.GetComponent<PlayerController>();

        _itemDict = Managers.Data.ItemInfoDict;

        while (_pickedNums.Count < 2)
        {
            string itemName =  PickRandomItem();
            if (itemName == null) break;
        }
        

        int i = 0;
        foreach (int num in _pickedNums)
        {
            _pickedItems[i] = _itemDict[Enum.GetName(typeof(Define.Item), num)];
            i++;
        }



        GetImage((int)Images.ItemBtnIcon1).sprite = Managers.Resource.Load<GameObject>($"Prefabs/Items/{_pickedItems[0].name}").GetComponent<SpriteRenderer>().sprite;
        //GetImage((int)Images.ItemBtnIcon1).sprite = Resources.Load<GameObject>($"Prefabs/Items/{_pickedItems[0].name}").GetComponent<Image>().sprite;
        GetMeshText((int)Texts.ItemBtnPriceText1).text = _pickedItems[0].price.ToString();
        GetImage((int)Images.ItemBtnIcon2).sprite = Managers.Resource.Load<GameObject>($"Prefabs/Items/{_pickedItems[1].name}").GetComponent<SpriteRenderer>().sprite;
        //GetImage((int)Images.ItemBtnIcon2).sprite = Resources.Load<GameObject>($"Prefabs/Items/{_pickedItems[1].name}").GetComponent<Image>().sprite;
        GetMeshText((int)Texts.ItemBtnPriceText2).text = _pickedItems[1].price.ToString();

        confirmPopup = GetObject((int)GameObjects.ConfirmBackground);
        confirmPopup.SetActive(false);
        itemBtn1 = GetButton((int)Buttons.ItemBtn1).gameObject;
        itemBtn2 = GetButton((int)Buttons.ItemBtn2).gameObject;

        ConfirmBtn = GetButton((int)Buttons.ConfirmBtn).gameObject;
        CancelBtn = GetButton((int)Buttons.CancelBtn).gameObject;


        BindEvent(itemBtn1, (PointerEnterEvent) =>
        {
            if (_playerStat.Gold < _pickedItems[0].price) return; // 진동 애니메이션 추가해서 못산다고 전달?
            else
            {
                SelectItem = _pickedItems[0];
                confirmPopup.SetActive(true);
            }
        });
        BindEvent(itemBtn2, (PointerEnterEvent) =>
        {
            if (_playerStat.Gold < _pickedItems[1].price) return; // 진동 애니메이션 추가해서 못산다고 전달?
            else
            {
                SelectItem = _pickedItems[1];
                confirmPopup.SetActive(true);
            }
        });

        BindEvent(ConfirmBtn, (PointerEventData) =>
        {
            int itemCount;
            if(_playerController.ItemTree.TryGetValue(SelectItem.name , out itemCount)) _playerController.ItemTree[SelectItem.name] = itemCount + 1;
            else _playerController.ItemTree.Add(SelectItem.name, 1);
            _playerStat.Gold -= SelectItem.price;
            _playerController.itemApply();
            confirmPopup.SetActive(false);
        });
        BindEvent(CancelBtn, (pointerEventData) =>
        {
            confirmPopup.SetActive(false);
        });

        BindEvent(Get<Button>((int)Buttons.CloseBtn).gameObject, (PointerEventData) =>
        {
            Managers.UI.ClosePopupUI();
        });





    }

    string PickRandomItem()
    {
        if (_itemDict.Count - _pickedNums.Count < 1) return null;

        int rand = Random.Range(0, _itemDict.Count);
        _pickedNums.Add(rand);
        string[] items = Enum.GetNames(typeof(Define.Item));
        return items[rand];
    }


}
