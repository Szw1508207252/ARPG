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
    float yMinLimit = -10; //��Сƫ����
    float yMaxLimit = 70;  //���ƫ����
    float disMinLimit = 2; //��С����
    float disMaxLimit = 10; //������
    float distance = 4;  //��������룻
    float zoomRate = 80;  //�Ŵ�ϵ����

    Vector3 offest = new Vector3(0,1,0); //���ƫ����
    Vector3 rotateOffset = new Vector3(0.1f, 0, -1);//��תƫ����
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;//�������
    }

    
    void Update()
    {
        x += inputC.m_Camera.x * xSpeed * Time.deltaTime;
        y -= inputC.m_Camera.y * ySpeed * Time.deltaTime;
        y = clampAngle(y, yMinLimit, yMaxLimit);
        Quaternion rotation = Quaternion.Euler(y, x, 0); //������ת��x��������ת��y
        transform.rotation = rotation;  //��Ԫ����ת
        if (inputC.m_Movement.x != 0 || inputC.m_Movement.y != 0)
        {
            target.transform.rotation = Quaternion.Euler(0, x, 0); //����ɫ�ƶ�ʱ�����������Ϊ�����ƶ�����
        }
        distance -= (inputC.m_Camera.z * Time.deltaTime) * zoomRate * Mathf.Abs(distance); //���롪����
        distance = Mathf.Clamp(distance, disMinLimit, disMaxLimit);
        transform.position = target.position + offest + rotation * (rotateOffset * distance); //�����λ�ã�Ŀ��λ�� + ǰ��ƫ����+ ��Ԫ����ת * ��תƫ����*����
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
