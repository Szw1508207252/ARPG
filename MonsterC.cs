using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MonsterC : Attribute,FightInterface
{
    public PlayerC playerC;
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
    public Image barhp;
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
        硬直时间_ = 硬直时间;
        发呆时间_ = 发呆时间;
        后摇时间_ = 后摇时间;
        攻击cd_ = 攻击CD;
        前摇时间_ = 前摇时间;
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
            case 怪物状态.硬直:
                硬直();
                break;
            case 怪物状态.死亡:
                死亡();
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
        float distance = Vector3.Distance(this.transform.position, 目标.position);
        if(distance >　失去目标距离)
        {
            return true;
        }
        return false;
    }
    bool 检测是否可攻击()
    {
        float distance = Vector3.Distance(this.transform.position, 目标.position);
        if(distance < 攻击距离)
        {
            return true;
        }
        return false;
    }
    void 针对攻击决策()
    {
        int R = Random.Range(0, 100);
        if(R < 攻击概率 && playerC.hp > 0)
        {
            ani.SetBool("walk", false);
            ani.SetTrigger("beforeAtk");
            
            状态_ = 怪物状态.攻击前摇;
        }
        else
        {
            状态_ = 怪物状态.发呆;
        }
    }
    void 攻击发动()
    {
        攻击cd_ -= Time.deltaTime;
        if(攻击cd_ < 0)
        {
            ani.SetTrigger("afterAtk");
            
            playerC.beHit(攻击力);

            状态_ = 怪物状态.攻击后摇;
             攻击cd_ = 攻击CD;
        }
    }
    void 前摇()
    {
        Vector3 pos = 目标.position;
        pos.y = this.transform.position.y; //防止怪物高度与人物高度不一致，产生倾斜
        this.transform.LookAt(pos);
        前摇时间_ -= Time.deltaTime;
        if(前摇时间_ < 0)
        {
            ani.SetTrigger("atk");
            状态_ = 怪物状态.攻击开始;
            前摇时间_ = 前摇时间;
        }

    }
    void 后摇()
    {
        后摇时间_ -= Time.deltaTime;
        if(后摇时间_ < 0)
        {
            状态_ = 怪物状态.没有目标;
            后摇时间_ = 后摇时间;
        }
    } 
    void 发呆()
    {
        发呆时间_ -= Time.deltaTime;
        if (发呆时间_ < 0)
        {
            状态_ = 怪物状态.没有目标;
            发呆时间_ = 发呆时间; 
        }
    }
    void 硬直()
    {
        硬直时间_ -= Time.deltaTime;
        if (硬直时间_ < 0)
        {
            状态_ = 怪物状态.没有目标;
            硬直时间_ = 硬直时间;
        }
    }
    void 死亡()
    {
        ani.SetBool("die", true);
        Destroy(this.gameObject,3);

        StartCoroutine(处理死亡效果());
    }
    public void beHit(float atk)
    {
        if(状态_== 怪物状态.死亡)
        {
            return;
        }
        硬直时间_ = 硬直时间;
        发呆时间_ = 发呆时间;
        后摇时间_ = 后摇时间;
        攻击cd_ = 攻击CD;
        前摇时间_ = 前摇时间;
        状态_ = 怪物状态.硬直;
        ani.SetTrigger("beHit");
        this.hp -= atk;
        barhp.fillAmount = hp / hpMax;
        if(hp <= 0)
        {
            Time.timeScale = 0.3f;  //慢动画
            状态_ = 怪物状态.死亡;
        }
    }

    IEnumerator 处理死亡效果()
    {
        yield return new WaitForSecondsRealtime(3);
        Time.timeScale = 1;
        Destroy(this.gameObject,1);
    }
}
