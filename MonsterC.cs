using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterC : Attribute
{
    protected float hp = 0;
    protected Animator ani;
    protected CharacterController cc;
    protected float 攻击cd_;
    protected float 硬直时间_;
    protected float 发呆时间_;
    protected float 后摇时间_;
    protected float 前摇时间_;
    public 怪物状态 状态_;
    protected Transform 目标;
    protected Transform player;

    Vector3[] 巡逻点array = new Vector3[2];
    int 巡逻点目标index = 0;

    public enum 怪物状态
    {
        没有目标,
        锁定目标,
        死亡,
        发呆,
        追击,
        攻击决策,
        攻击前摇,
        攻击中,
        攻击开始,
        攻击后摇,
        硬直

    }

    private void Awake()
    {
        binbing();
    }

    void binbing() //绑定函数
    {
        cc = this.GetComponent<CharacterController>();
        ani = this.transform.GetChild(0).GetComponent<Animator>();
        hp = hpMax;
    }
    void Start()
    {
        Vector3 出生点pos = this.transform.position;
        巡逻点array[0] = 出生点pos + new Vector3(Random.Range(1, 3), 0, Random.Range(1, 3));
        巡逻点array[1] = 出生点pos + new Vector3(Random.Range(-1, -3), 0, Random.Range(-1, -3));
    }

  
    void Update()
    {
        switch (状态_)
        {
            case 怪物状态.没有目标:
                尝试发现目标();
                break;
            case 怪物状态.锁定目标:
                针对目标决策();
                break;
            case 怪物状态.追击:
                处理追击();
                break;
            case 怪物状态.攻击决策:
                针对攻击决策();
                break;
            case 怪物状态.攻击开始:
                攻击发动();
                break;
            case 怪物状态.攻击前摇:
                前摇();
                break;
            case 怪物状态.攻击后摇:
                后摇();
                break;
            case 怪物状态.发呆:
                发呆();
                break;

        }
        

    }

   void 尝试发现目标()
    {
        if(player == null )
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
       
        float distance = Vector3.Distance(this.transform.position, player.position);       
        巡逻();
       if(distance <= 听觉范围)
        {
            发现目标();
        }
        else if(distance <= 视觉范围 && ToolMethod.获取与目标的夹角(player.position,this.transform)<60)  //目标与怪兽视野中心点之间夹角小于野怪总视野夹角的一半
        {
            发现目标();
        }
        else
        {
            解除目标();
        }
    }
    void 发现目标()
    {
        状态_ = 怪物状态.锁定目标;
        目标 = player;
    }
    void 解除目标()
    {
        状态_ = 怪物状态.没有目标;
        目标 = null;
    }
    void 巡逻()
    {
        this.transform.LookAt(巡逻点array[巡逻点目标index]);
        cc.Move(this.transform.forward * moveSpeed * Time.deltaTime);
        if (Vector3.Distance(this.transform.position, 巡逻点array[巡逻点目标index])< 0.1f)
        {
            巡逻点目标index= ++巡逻点目标index % 巡逻点array.Length;
        }
    }
   

    void 针对目标决策()
    {
        int R = Random.Range(0, 100);
        if(R <= 追击概率)
        {
            状态_ = 怪物状态.追击;
        }
        else
        {
            状态_ = 怪物状态.发呆;
        }
    }
    void 处理追击()
    {
        if(检测是否可攻击())
        {
            状态_ = 怪物状态.攻击决策;
            return;
        }
        if(检测是否失去目标())
        {
            状态_ = 怪物状态.没有目标;
            return;
        }
        Vector3 pos = 目标.position;
        pos.y = this.transform.position.y;
        this.transform.LookAt(pos);
        cc.Move(this.transform.forward * moveSpeed * Time.deltaTime);
    }
    bool 检测是否失去目标()
    {
        return false;
    }
    bool 检测是否可攻击()
    {
        return false;
    }
    void 针对攻击决策()
    {

    }
    void 攻击发动()
    {

    }
    void 前摇()
    {

    }
    void 后摇()
    {

    } 
    void 发呆()
    {

    }


}
