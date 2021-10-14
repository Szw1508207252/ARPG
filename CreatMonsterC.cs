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
    int totalNum = 3; //刷怪总数
    int curNum = 0;   //当前刷怪数
    bool makeMonster = false;

    public PlayerC playerC;
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
            timeCD -= Time.deltaTime; //每秒减一：刷怪CD
            if(timeCD < 0)
            {
                timeCD = timeCD_;     //重置
                doCreatMonster();
            }
        }      
    }
    private void OnTriggerEnter(Collider other)  //当刷怪触发器与tag为“Player”的物体碰撞后，设置“是否刷怪”为true
    {
        if(other.CompareTag("Player"))
        {
            makeMonster = true;
        }
    }
    void doCreatMonster()   //刷怪
    {
        GameObject go = Instantiate(monster_perfab,posArray[index++ %posArray.Length].position,Quaternion.identity); //在刷怪点生成怪物；
        go.GetComponent<MonsterC>().playerC = playerC;     //预制体不能直接绑定在场景里，需要代码赋值。
        curNum++;
    }
}
