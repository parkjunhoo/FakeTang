using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
    GameObject _player;
    public GameObject Player { get { return _player; } }
    //Dictionary<int, GameObject> _players = new Dictionary<int, GameObject>();
    //Dictionary<int, GameObject> _monsters = new Dictionary<int, GameObject>();
    HashSet<GameObject> _monsters = new HashSet<GameObject>();
    HashSet<GameObject> _items = new HashSet<GameObject>();



    public GameObject Spawn(Define.WorldObject type, string path , Transform parent = null)
    {
        GameObject go = Managers.Resource.Instantiate(path, parent);
        switch (type)
        {
            case Define.WorldObject.Monster:
                _monsters.Add(go);
                break;
            case Define.WorldObject.Player:
                _player = go;
                break;
            case Define.WorldObject.Item:
                _items.Add(go);
                break;
        }

        return go;
    }

    public Define.WorldObject GetWorldObjectType(GameObject go)
    {
        switch (go.layer)
        {
            case 3:
                return Define.WorldObject.Player;
            case 7:
                return Define.WorldObject.Monster;
            case 8:
                return Define.WorldObject.Item;
        }
        return Define.WorldObject.Unknown;
    }

    public void Despawn(GameObject go , float time = 0f)
    {
        Define.WorldObject type = GetWorldObjectType(go);
        switch (type)
        {
            case Define.WorldObject.Monster:
                if (_monsters.Contains(go)) _monsters.Remove(go);
                break;

            case Define.WorldObject.Player:
                if ( _player == go)  _player = null;
                break;

            case Define.WorldObject.Item:
                if (_items.Contains(go)) _items.Remove(go);
                break;
        }

        Managers.Resource.Destroy(go,time);
    }
}
