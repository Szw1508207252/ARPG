using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerC : MonoBehaviour
{
    public inputC inputC;
    public CharacterController controller;
    public float walkSpeed = 2;
   
    void Start()
    {
        
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
}
