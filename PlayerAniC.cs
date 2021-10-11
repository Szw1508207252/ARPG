using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAniC : MonoBehaviour
{
    public Animator ani;
    public inputC inputC;
    float ����������Time = 0;
    void Start()
    {
       
    }

    void Update()
    {

        ani.SetFloat("aniTime", Mathf.Repeat(ani.GetCurrentAnimatorStateInfo(0).normalizedTime,1)); //���ò���aniTime���ڣ�0,1���仯

        if(inputC.m_atkTrigger) //�ж������������ִ�й�������
        {
            ����������Time = 2;  //�����ڲ����й��������ö�������
            ani.SetTrigger("atk");
            ani.SetInteger("������", (ani.GetInteger("������") + 1) % 3); //ȡֵ 1��2��0�ж���������
            
         
        }
        // ��ɫ�ƶ�
        ani.SetFloat("ˮƽ�ٶ�",inputC.m_Movement.x);
        ani.SetFloat("��ֱ�ٶ�",inputC.m_Movement.y);
        ����������Time -= Time.deltaTime;  
        if(����������Time < 0)
        {
            ani.SetInteger("������", 0);  //��
        }
    }

    private void LateUpdate() //��Updateִ�����ִ��
    {
        ani.ResetTrigger("atk"); //��ֹ������죬�ڹ������������е�����ִ�к���Ĺ���������
    }
}
