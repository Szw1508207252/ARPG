using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerC : MonoBehaviour
{
    public inputC inputC;
    public CharacterController controller;
    public float walkSpeed = 2;  
    public BoxCollider 攻击碰撞器;
    public Image harhp;
    public float hp;   //当前血量      
    private float hpMax = 100;   //最大血量
    RaycastHit[] raycastHit = new RaycastHit[10];
    public Animator ani;
    public GameObject EndPlane;
    void Start()
    {
        this.hp = hpMax;
    }

   
    void Update()
    {
        move();
    }
     void move()  //移动
    {
        Vector3 dir = transform.TransformDirection(new Vector3(inputC.m_Movement.x, 0, inputC.m_Movement.y)); //获取自身方向 x,y,z
        controller.Move(dir * walkSpeed * Time.deltaTime); //移动
        
    }
    public void 打开攻击碰撞器()
    {
        攻击碰撞器.enabled = true;
    }

    public void 关闭攻击碰撞器()
    {
        攻击碰撞器.enabled = false;
    }

    public void 攻击伤害检测()
    {
        if(Physics.SphereCastNonAlloc(this.transform.position,2,this.transform.forward,raycastHit,1)>0)
        {
            for(int i = 0; i< raycastHit.Length; i++)
            {
                if(raycastHit[i].collider == null)
                {
                    break;
                }
                if(ToolMethod.获取与目标的夹角(raycastHit[i].collider.transform.position,this.transform) <90)
                {
                    FightInterface fi = raycastHit[i].collider.GetComponent<FightInterface>();
                    if (fi != null)
                    {
                        fi.beHit(10);
                    }
                }
            }
        }
    }

    public void beHit(float atk)
    {
        this.hp -= atk;
        harhp.fillAmount = hp / hpMax;
        ani.SetTrigger("beHit");
        if (hp <= 0)
        {
            死亡();
            Time.timeScale = 0.3f;
        }
    }

    IEnumerator 处理死亡效果()
    {
        yield return new WaitForSecondsRealtime(3);
        Time.timeScale = 1;
        EndPlane.SetActive(true);
    }
    void 死亡()
    {
        ani.SetBool("die", true);
      

        StartCoroutine(处理死亡效果());
    }
}
