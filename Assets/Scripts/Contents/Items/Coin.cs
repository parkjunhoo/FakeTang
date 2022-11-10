using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    int _gold = 1;
    bool oneTime = true;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == (int)Define.Layer.Player && oneTime)
        {
            collision.gameObject.GetComponent<PlayerStat>().Gold += _gold;
            oneTime = false;
            Managers.Game.Despawn(gameObject);
        }
    }

    public void setGold(int amount)
    {
        _gold = amount;
    }
}
