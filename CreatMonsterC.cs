using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatMonsterC : MonoBehaviour
{
    public GameObject monster_perfab;
    public Transform[] posArray;
    int index = 0;
    float timeCD = 1;
    float timeCD_ = 1;
    int totalNum = 3; //ˢ������
    int curNum = 0;   //��ǰˢ����
    bool makeMonster = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (curNum >= totalNum)
            return;
        if(makeMonster) 
        {
            timeCD -= Time.deltaTime; //ÿ���һ��CD
            if(timeCD < 0)
            {
                timeCD = timeCD_;     //����
                doCreatMonster();
            }
        }      
    }
    private void OnTriggerEnter(Collider other)  //��ˢ�ִ�������tagΪ��Player����������ײ�����á��Ƿ�ˢ�֡�Ϊtrue
    {
        if(other.CompareTag("Player"))
        {
            makeMonster = true;
        }
    }
    void doCreatMonster()   //ˢ��
    {
        Instantiate(monster_perfab,posArray[index++ %posArray.Length].position,Quaternion.identity); //��ˢ�ֵ����ɹ��
        curNum++;
    }
}
