using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    int _gold = 1;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == (int)Define.Layer.Player)
        {
            collision.gameObject.GetComponent<PlayerStat>().Gold += _gold;
        }
    }

    public void setGold(int amount)
    {
        _gold = amount;
    }
}
