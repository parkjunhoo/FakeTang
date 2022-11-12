using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static Define;
using static MonsterController;
using static UnityEngine.EventSystems.EventTrigger;
using Newtonsoft.Json;

public class PlayerController : MonoBehaviour
{

    public static PlayerController Instance;

    Dictionary<int, int> _activeSkillTree = new Dictionary<int, int>();
    public Dictionary<int, int> ActiveSkillTree { get { return _activeSkillTree; } }

    
    public Dictionary<string,int> ItemTree = new Dictionary<string, int>();

    PlayerStat _stat;
    Rigidbody2D rigid;
    Animator anim;

    bool isEnergyBoltCool = false;
    bool isUnnamedSkillCool = false;
    bool isSetelliteCool = false;
    public int setelliteCount = 0;

    float damageDelay = 0.3f;
    bool isDamageDelay = false;




    float run_ratio = 0f;
    float damage_ratio = 0f;

    public enum PlayerState
    {
        Idle,
        Moving,
        Die,
    }

    PlayerState _state = PlayerState.Idle;

    public static PlayerState State { get { return Instance._state; } }

    float joyX { get { return Managers.Input.joyX; } }
    float joyY { get { return Managers.Input.joyY; } }


    void Start()
    {
        Instance = this;
        setelliteCount = 0;
        _activeSkillTree.Add((int)Define.ActiveSkill.EnergyBolt, 1);
        _stat = GetComponent<PlayerStat>();
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        Managers.Input.JoyAction -= OnJoystick;
        Managers.Input.JoyAction += OnJoystick;
        Managers.UI.MakeWorldSpaceUI<UI_HPBar>(transform);
    }


    void FixedUpdate()
    {
        switch (_state)
        {
            case PlayerState.Idle:
                Attack();
                run_ratio = Mathf.Lerp(run_ratio, 0, 10.0f * Time.fixedDeltaTime);
                anim.SetFloat("run_ratio", run_ratio);
                anim.Play("IDLE_RUN");
                break;
            case PlayerState.Die:
                anim.Play("DIE");
                break;
            case PlayerState.Moving:
                Attack();
                run_ratio = Mathf.Lerp(run_ratio, 1, 10.0f * Time.fixedDeltaTime);
                anim.SetFloat("run_ratio", run_ratio);
                anim.Play("IDLE_RUN");
                break;
        }
    }

    void OnJoystick()
    {
        if (_state != PlayerState.Die)
        {
            Move();
        }
    }

    void Move()
    {
        if (joyX == 0 && joyY == 0) { _state = PlayerState.Idle; return; }
        _state = PlayerState.Moving;
        float rotationDir = joyX > 0 ? 0 : 180;
        Vector2 inputDir = new Vector2(joyX, joyY).normalized;
        transform.rotation = Quaternion.Euler(new Vector3(0, rotationDir, 0));
        //if (Mathf.Abs(joyX) > 0.3 || Mathf.Abs(joyY) > 0.3) rigid.MovePosition(rigid.position + inputDir * _speed *  Time.deltaTime);
        rigid.MovePosition(rigid.position + inputDir * _stat.MoveSpeed *_stat.ExtraSpeed * Time.fixedDeltaTime);
        
    }

    void Attack()
    {
        foreach (var entry in _activeSkillTree)
        {
            int skillLevel = entry.Value;
            switch (entry.Key)
            {
                case (int)Define.ActiveSkill.EnergyBolt:
                    if (!isEnergyBoltCool)
                    {
                        useSkill(Define.ActiveSkill.EnergyBolt, skillLevel);
                        isEnergyBoltCool = true;
                        float coolTime = Managers.Data.EnergyBoltStatDict[skillLevel].coolTime;
                        StartCoroutine(EnergyBoltDelay(coolTime));
                    }
                    break;

                case (int)Define.ActiveSkill.UnnamedSkill:
                    if (!isUnnamedSkillCool)
                    {
                        useSkill(Define.ActiveSkill.UnnamedSkill, skillLevel);
                        isUnnamedSkillCool = true;
                        float coolTime = Managers.Data.UnnamedSkillStatDict[skillLevel].coolTime;
                        StartCoroutine(UnnamedSkillDelay(coolTime));
                    }
                    break;

                case (int)Define.ActiveSkill.Setellite:
                    if (!isSetelliteCool)
                    {
                        int maxCount = Managers.Data.SetelliteStatDict[skillLevel].maxCount;
                        if (setelliteCount >= maxCount) break;
                        useSkill(Define.ActiveSkill.Setellite, skillLevel);
                        setelliteCount++;
                        isSetelliteCool = true;
                        float coolTime = Managers.Data.SetelliteStatDict[skillLevel].coolTime;
                        StartCoroutine(SetelliteDelay(coolTime));
                    }
                    break;
            }
        }
    }

    public void OnDamaged(int damage)
    {
        if (!isDamageDelay && _state != PlayerState.Die)
        {
            isDamageDelay = true;
            StartCoroutine(DamageDelay());
            damage_ratio = Mathf.Lerp(damage_ratio, 0, 10.0f * Time.fixedDeltaTime);
            anim.SetFloat("damage_ratio", damage_ratio);
            anim.Play("DAMAGE");
            _stat.Hp -= damage;
            if (_stat.Hp <= 0) { _state = PlayerState.Die; GameScene.GameMode = Define.GameMode.GameOver; }
        }
    }


    void useSkill(Define.ActiveSkill skill, int level)
    {
        string skillName = Enum.GetName(typeof(Define.ActiveSkill), skill);
        Managers.Resource.Instantiate($"Skills/{skillName}").GetComponent<Skill>().SetStat(level);
    }

    #region Skill Coroutine
    IEnumerator EnergyBoltDelay(float coolTime)
    {
        yield return new WaitForSeconds(coolTime);
        isEnergyBoltCool = false;

    }
    IEnumerator UnnamedSkillDelay(float coolTime)
    {
        yield return new WaitForSeconds(coolTime);
        isUnnamedSkillCool = false;

    }
    IEnumerator SetelliteDelay(float coolTime)
    {
        yield return new WaitForSeconds(coolTime);
        isSetelliteCool = false;

    }
    #endregion
    IEnumerator DamageDelay()
    {
        yield return new WaitForSeconds(damageDelay);
        isDamageDelay = false;
    }

    public void itemApply()
    {
        foreach (var entry in ItemTree)
        {
            switch (entry.Key)
            {
                case "Shoes":
                    var shoesData = JsonConvert.DeserializeObject<Data.Shoes>(Managers.Data.ItemInfoDict["Shoes"].value);
                    _stat.ExtraSpeed = Mathf.Pow(shoesData.moveSpeed, entry.Value);
                    break;
                case "Sword":
                    var swordData = JsonConvert.DeserializeObject<Data.Sword>(Managers.Data.ItemInfoDict["Sword"].value);
                    _stat.ExtraAttack = swordData.attack * entry.Value;
                    break;
                case "VampireRing":
                    var vampireRingData = JsonConvert.DeserializeObject<Data.VampireRing>(Managers.Data.ItemInfoDict["VampireRing"].value);
                    _stat.Drain = Mathf.Pow(vampireRingData.drain, entry.Value);
                    break;
            }
        }
    }
}
