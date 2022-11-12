using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Merchant : MonoBehaviour
{
    private void Start()
    {
        Managers.Game.Despawn(gameObject, 60f);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Managers.UI.ShowPopupUI<UI_Merchant>().SetMerchant(gameObject);
        }
    }
}
