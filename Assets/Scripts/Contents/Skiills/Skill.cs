using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UIElements;
using Debug = UnityEngine.Debug;

public class Skill : MonoBehaviour
{
    protected int _level;
    protected float _attack;
    protected float _coolTime;
    protected float _moveSpeed;
    protected float _attackRange;
    protected int _damage;
    Vector3 targetPos;

    public int Level { get { return _level; } set { _level = value; } }
    public float Attack { get { return _attack; } set { _attack = value; } }
    public float CoolTime { get { return _coolTime; } set { _coolTime = value; } }
    public float MoveSpeed { get { return _moveSpeed; } set { _moveSpeed = value; } }
    public float AttackRange { get { return _attackRange; } set { _attackRange = value; } }
    public int Damage { get { return _damage; } set { _damage = value; } }
    public GameObject Player { get { return _player; }}

    Rigidbody2D rigid;
    GameObject _player;
    int playerAttack;

    protected virtual void Init()
    {
        SetStat(1);
        rigid = gameObject.GetComponent<Rigidbody2D>();
        _player = GameObject.FindGameObjectWithTag("Player");
        transform.position = Player.transform.position;
        targetPos = Vector3.zero;
        playerAttack = _player.GetComponent<PlayerStat>().Attack;
        _damage = (int)((float)playerAttack * _attack);
    }
    private void Start()
    {
        Init();
        
    }

    public virtual void SetStat(int level)
    {

    }

    protected void TraceEnemy()
    {
        Collider2D[] col = Physics2D.OverlapCircleAll(transform.position, _attackRange, LayerMask.GetMask("Monster"));

        if (targetPos == Vector3.zero)
        {
            if (col.Length > 0)
            {
                targetPos = transform.position + (col[Random.Range(0, col.Length)].transform.position - transform.position).normalized * _attackRange;
            }
            else targetPos = RandPosInRange;
        }
        Move(targetPos);
    }

    private void Move(Vector3 dest)
    {
        if ((dest - transform.position).magnitude < 0.5f) { Managers.Resource.Destroy(gameObject); return; }

        Vector3 destDir = (dest - transform.position).normalized;
        float angle = Mathf.Atan2(dest.y - transform.position.y, dest.x - transform.position.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis((angle+180f) , Vector3.forward);
        transform.position += destDir * _moveSpeed * Time.fixedDeltaTime;
        //rigid.MovePosition(transform.position + destDir * _moveSpeed * Time.fixedDeltaTime);
    }

    private Vector3 RandPosInRange
    {
        get
        {
            float a = transform.position.x;
            float b = transform.position.y;
            float x = Random.Range(-_attackRange + a, _attackRange + a);
            float y_b = Mathf.Sqrt(Mathf.Pow(_attackRange, 2) - Mathf.Pow(x - a, 2));
            y_b *= Random.Range(0, 2) == 0 ? -1 : 1;
            float y = y_b + b;
            return new Vector3(x, y, 0);
        }
    }
}
