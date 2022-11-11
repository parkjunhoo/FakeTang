using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    bool oneTime = true;
    string itemName="";
    private void Awake()
    {
        itemName = gameObject.name;
        int index = itemName.IndexOf("(Clone)");
        if (index > 0) itemName = itemName.Substring(0, index);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == (int)Define.Layer.Player && oneTime)
        {
            Dictionary<string, int> itemTree = collision.gameObject.GetComponent<PlayerController>().ItemTree;
            int itemCount;
            if (itemTree.TryGetValue(itemName, out itemCount)) itemTree[itemName] = itemCount + 1;
            else itemTree.Add(itemName, 1);
            collision.gameObject.GetComponent<PlayerController>().itemApply();
            oneTime = false;
            Managers.Game.Despawn(gameObject);
        }
    }
}
