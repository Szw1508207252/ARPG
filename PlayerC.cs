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
     void move()  //�ƶ�
    {
        Vector3 dir = transform.TransformDirection(new Vector3(inputC.m_Movement.x, 0, inputC.m_Movement.y)); //��ȡ������ x,y,z
        controller.Move(dir * walkSpeed * Time.deltaTime); //�ƶ�
        
    }
}
