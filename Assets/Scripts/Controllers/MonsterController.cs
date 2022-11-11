using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerController;

public class MonsterController : MonoBehaviour
{
    MonsterStat _stat;
    Rigidbody2D rigid;
    GameObject target;
    Animator anim;

    bool DyingMessage = false;

    public enum MonsterState
    {
        Idle,
        Die,
        Moving,
        Attack,
    }

    MonsterState _state = MonsterState.Moving;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        _stat = GetComponent<MonsterStat>();
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        //Managers.UI.MakeWorldSpaceUI<UI_HPBar>(transform);
    }

    private void OnEnable()
    {
        _state = MonsterState.Moving;
        gameObject.layer = (int)Define.Layer.Monster;
        DyingMessage = false;

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.gameObject.tag == "Player" && _state != MonsterState.Die)
        {
            collision.collider.gameObject.GetComponent<PlayerController>().OnDamaged(_stat.Attack);
        }
    }
    void FixedUpdate()
    {
        switch (_state)
        {
            case MonsterState.Moving:

                anim.Play("IDLE");
                Move();
                break;
            case MonsterState.Die:
                anim.Play("DIE");
                gameObject.layer = 0;
                if (!DyingMessage) { DyingMessage = true; Invoke("OnDie", 0.5f); }
                //Managers.Game.Despawn(gameObject,0.5f);
                //Managers.Resource.Destroy(gameObject,0.5f);
                break;
            case MonsterState.Attack:
                break;
        }
    }

    void Move()
    {
        if (target == null) target = GameObject.FindGameObjectWithTag("Player");
        else if (_state != MonsterState.Die)
        {
            Vector2 inputDir = (target.transform.position - transform.position).normalized;

            float rotationDir = inputDir.x > 0 ? 0 : 180;
            transform.rotation = Quaternion.Euler(new Vector3(0, rotationDir, 0));
            rigid.MovePosition(rigid.position + inputDir * _stat.MoveSpeed * Time.fixedDeltaTime);
        }
    }

    public void OnDamaged(int damage)
    {
        _stat.Hp -= damage;
        Managers.UI.MakeWorldSpaceUI<UI_DamageText>(transform).damage = damage;
        if (_stat.Hp <= 0) {  _state = MonsterState.Die;  }
    }

    void OnDie()
    {
        MonsterSpawner.MonsterCount--;
        target.GetComponent<PlayerStat>().KillingCount++;
        target.GetComponent<PlayerStat>().Exp += _stat.Exp;
        Managers.Game.Despawn(gameObject);
        if (Utill.RandomInHundred(10))
        {
            GameObject go = Managers.Game.Spawn(Define.WorldObject.Item, "Items/Coin");
            go.GetComponent<Coin>().setGold(_stat.Gold);
            go.transform.position = transform.position;
        }
        if (Utill.RandomInHundred(50))
        {
            GameObject go = Managers.Game.Spawn(Define.WorldObject.Item, "Items/Shoes");
            go.transform.position = transform.position;
        }

    }
}
