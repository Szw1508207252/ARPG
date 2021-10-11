using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraC : MonoBehaviour
{
    public inputC inputC;
    public Transform target;
    float xSpeed = 250;
    float ySpeed = 125;

    float x = 0;
    float y = 0;
    float yMinLimit = -10; //最小偏移量
    float yMaxLimit = 70;  //最大偏移量
    float disMinLimit = 2; //最小距离
    float disMaxLimit = 10; //最大距离
    float distance = 4;  //摄像机距离；
    float zoomRate = 80;  //放大系数；

    Vector3 offest = new Vector3(0,1,0); //相机偏移量
    Vector3 rotateOffset = new Vector3(0.1f, 0, -1);//旋转偏移量
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;//锁定鼠标
    }

    
    void Update()
    {
        x += inputC.m_Camera.x * xSpeed * Time.deltaTime;
        y -= inputC.m_Camera.y * ySpeed * Time.deltaTime;
        y = clampAngle(y, yMinLimit, yMaxLimit);
        Quaternion rotation = Quaternion.Euler(y, x, 0); //上下旋转是x，左右旋转是y
        transform.rotation = rotation;  //四元数旋转
        if (inputC.m_Movement.x != 0 || inputC.m_Movement.y != 0)
        {
            target.transform.rotation = Quaternion.Euler(0, x, 0); //当角色移动时，摄像机方向为物体移动方向
        }
        distance -= (inputC.m_Camera.z * Time.deltaTime) * zoomRate * Mathf.Abs(distance); //距离—滚轮
        distance = Mathf.Clamp(distance, disMinLimit, disMaxLimit);
        transform.position = target.position + offest + rotation * (rotateOffset * distance); //摄像机位置：目标位置 + 前后偏移量+ 四元数旋转 * 旋转偏移量*距离
    }

    float clampAngle(float angle ,float min,float max)
    {
        if(angle>360)
        {
            angle -= 360;
        }
        else if(angle < -360)
        {
            angle += 360;
        }
        return Mathf.Clamp(angle, min, max);
    }
}
