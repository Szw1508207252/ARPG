using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inputC : MonoBehaviour
{
    public Vector2 m_Movement;
    public bool m_atkTrigger ;
    public Vector3 m_Camera;
    void Start()
    {
        
    }

    
    void Update()
    {
        //角色移动，显示数值上
        m_Movement.Set(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        m_atkTrigger = Input.GetMouseButtonDown(0);
        m_Camera.Set(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse ScrollWheel"));
    }
}
