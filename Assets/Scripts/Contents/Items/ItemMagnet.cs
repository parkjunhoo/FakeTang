using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemMagnet : MonoBehaviour
{
    bool isTouch = false;
    float _speed = 10.0f;
    GameObject _player;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == (int)Define.Layer.Player)
        {
            _player = collision.gameObject;
            isTouch = true;
        }

    }

    private void FixedUpdate()
    {
        if (!isTouch) return;
        transform.parent.transform.position = Vector3.MoveTowards(transform.position, _player.transform.position, _speed * Time.fixedDeltaTime);
    }
}
