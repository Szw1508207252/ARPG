using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attribute : MonoBehaviour
{
    public string name_;
    protected float hpMax = 30;
    protected float 攻击CD = 0.5f;
    protected float moveSpeed = 1;
    protected float 硬直时间 = 0.3f;
    protected float 攻击概率 = 80;
    protected float 追击概率 = 80;
    protected float 发呆时间 = 1;
    protected float 前摇时间 = 1;
    protected float 普通生效时间 = 0.5f;
    protected float 后摇时间 = 1;
    protected float 视觉范围 = 5;
    protected float 听觉范围 = 6;
    protected float 攻击距离 = 2;
    protected float 失去目标距离 = 8;
    protected bool 是否看向主角 = false;
}
