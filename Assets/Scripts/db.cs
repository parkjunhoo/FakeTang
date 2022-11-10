using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class db : MonoBehaviour
{

    public class SkillData
    {
        int _level;
        float _attack;
        float attackspeed;
        float _moveSpeed;
        float _attackRange;
    }

    void Start()
    {
        StartCoroutine(Get());
    }

    IEnumerator Get()
    {
        string apikey = "";
        string url = "http://naver.com";
        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();

        if(www.error == null)
        {

            SkillData skillData = JsonUtility.FromJson<SkillData>(www.downloadHandler.text);
            Debug.Log(www.downloadHandler.text);
            Debug.Log(skillData);
        }
        else
        {
            Debug.Log("error");
        }
    }
}
