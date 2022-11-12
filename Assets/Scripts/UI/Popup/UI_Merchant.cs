using Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
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
    GameObject SelectBtn;
    
    GameObject _player;
    PlayerStat _playerStat;
    PlayerController _playerController;

    GameObject _merchant;

    

    


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

        StayBtn,
        QuitBtn,
    }
    GameObject itemBtn1;
    GameObject itemBtn2;

    GameObject ConfirmBtn;
    GameObject CancelBtn;

    GameObject CloseBtn;
    GameObject StayBtn;
    GameObject QuitBtn;
    public enum Images
    {
        ItemBtnIcon1,
        ItemBtnIcon2,
    }
    public enum GameObjects
    {
        ConfirmBackground,
        QuitBackground,
    }
    GameObject confirmPopup;
    GameObject quitPopup;
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
        GetMeshText((int)Texts.ItemBtnPriceText1).text = _pickedItems[0].price.ToString()+"G";
        GetImage((int)Images.ItemBtnIcon2).sprite = Managers.Resource.Load<GameObject>($"Prefabs/Items/{_pickedItems[1].name}").GetComponent<SpriteRenderer>().sprite;
        GetMeshText((int)Texts.ItemBtnPriceText2).text = _pickedItems[1].price.ToString()+"G";

        confirmPopup = GetObject((int)GameObjects.ConfirmBackground);
        confirmPopup.SetActive(false);
        quitPopup = GetObject((int)GameObjects.QuitBackground);
        quitPopup.SetActive(false);

        itemBtn1 = GetButton((int)Buttons.ItemBtn1).gameObject;
        itemBtn2 = GetButton((int)Buttons.ItemBtn2).gameObject;

        ConfirmBtn = GetButton((int)Buttons.ConfirmBtn).gameObject;
        CancelBtn = GetButton((int)Buttons.CancelBtn).gameObject;

        CloseBtn = GetButton((int)Buttons.CloseBtn).gameObject;
        StayBtn = GetButton((int)Buttons.StayBtn).gameObject;
        QuitBtn = GetButton((int)Buttons.QuitBtn).gameObject;



        BindEvent(itemBtn1, (PointerEnterEvent) =>
        {
            if (_playerStat.Gold < _pickedItems[0].price)
            {
                itemBtn1.GetComponent<Animator>().Play("VIVE");
                return;
            }
            else
            {
                SelectItem = _pickedItems[0];
                SelectBtn = itemBtn1;
                confirmPopup.SetActive(true);
            }
        });
        BindEvent(itemBtn2, (PointerEnterEvent) =>
        {
            if (_playerStat.Gold < _pickedItems[1].price)
            {
                itemBtn2.GetComponent<Animator>().Play("VIVE");
                return;
            }
            else
            {
                SelectItem = _pickedItems[1];
                SelectBtn = itemBtn2;
                confirmPopup.SetActive(true);
            }
        });

        BindEvent(ConfirmBtn, (PointerEventData) =>
        {
            int itemCount;
            if (_playerController.ItemTree.TryGetValue(SelectItem.name, out itemCount)) _playerController.ItemTree[SelectItem.name] = itemCount + 1;
            else _playerController.ItemTree.Add(SelectItem.name, 1);
            _playerStat.Gold -= SelectItem.price;
            _playerController.itemApply();
            SelectBtn.GetComponent<Button>().interactable = false;
            ChildrenSetFalse(SelectBtn.transform);
            Destroy(SelectBtn.GetComponent<UI_EventHandler>());
            confirmPopup.SetActive(false);
        });


        BindEvent(CancelBtn, (pointerEventData) =>
        {
            confirmPopup.SetActive(false);
        });

        BindEvent(CloseBtn, (PointerEventData) =>
        {
            quitPopup.SetActive(true);
        });
        BindEvent(StayBtn, (pointerEventData) =>
        {
            quitPopup.SetActive(false);
        });
        BindEvent(QuitBtn, (pointerEventData) =>
        {
            quitPopup.SetActive(false);
            Managers.UI.ClosePopupUI();
            Managers.Game.Despawn(_merchant);
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
    
    void ChildrenSetFalse(Transform transform)
    {
        for(int i=0; i<transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    public void SetMerchant(GameObject go)
    {
        _merchant = go;
    }
}
