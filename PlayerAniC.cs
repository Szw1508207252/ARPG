using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAniC : MonoBehaviour
{
    public Animator ani;
    public inputC inputC;
    float 连击数重置Time = 0;
    void Start()
    {
       
    }

    void Update()
    {

        ani.SetFloat("aniTime", Mathf.Repeat(ani.GetCurrentAnimatorStateInfo(0).normalizedTime,1)); //设置参数aniTime，在（0,1）变化

        if(inputC.m_atkTrigger) //判定鼠标左键点击后执行攻击动画
        {
            连击数重置Time = 2;  //两秒内不进行攻击，重置动画过程
            ani.SetTrigger("atk");
            ani.SetInteger("连击数", (ani.GetInteger("连击数") + 1) % 3); //取值 1、2、0判定攻击动画
            
         
        }
        // 角色移动
        ani.SetFloat("水平速度",inputC.m_Movement.x);
        ani.SetFloat("垂直速度",inputC.m_Movement.y);
        连击数重置Time -= Time.deltaTime;  
        if(连击数重置Time < 0)
        {
            ani.SetInteger("连击数", 0);  
        }
    }

    private void LateUpdate() //在Update执行完才执行
    {
        ani.ResetTrigger("atk"); //防止点击过快，在攻击动画过程中点击便可执行后面的攻击动画。
    }
}
