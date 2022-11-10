using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_DamageText : UI_Base
{
    enum Texts
    {
        Text,
    }
    private float moveSpeed = 0.5f;
    private float alphaSpeed = 2f;
    private float destroyTime = 0.5f;
    Color alpha;

    public int damage = 1;

    TextMeshProUGUI text;

    public override void Init()
    {
        if (text == null)
        {
            Bind<TextMeshProUGUI>(typeof(Texts));
            text = GetMeshText((int)Texts.Text);
        }
        else
        {
            text.text = damage.ToString();
            alpha = text.color;
        }
    }

    private void OnEnable()
    {
        Init();
        Managers.Resource.Destroy(gameObject, destroyTime);
    }

    private void LateUpdate()
    {
        Transform parent = transform.parent;
        transform.position = parent.position + Vector3.up * (parent.GetComponent<Collider2D>().bounds.size.y);
        text.transform.Translate(new Vector3(0, moveSpeed * Time.fixedDeltaTime, 0));
        alpha.a = Mathf.Lerp(alpha.a, 0, Time.fixedDeltaTime * alphaSpeed); // 텍스트 알파값
        text.color = alpha;
        transform.rotation = Camera.main.transform.rotation;
    }

}
