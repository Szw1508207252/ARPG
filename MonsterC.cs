using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterC : Attribute
{
    protected float hp = 0;
    protected Animator ani;
    protected CharacterController cc;
    protected float ����cd_;
    protected float Ӳֱʱ��_;
    protected float ����ʱ��_;
    protected float ��ҡʱ��_;
    protected float ǰҡʱ��_;
    public ����״̬ ״̬_;
    protected Transform Ŀ��;
    protected Transform player;

    Vector3[] Ѳ�ߵ�array = new Vector3[2];
    int Ѳ�ߵ�Ŀ��index = 0;

    public enum ����״̬
    {
        û��Ŀ��,
        ����Ŀ��,
        ����,
        ����,
        ׷��,
        ��������,
        ����ǰҡ,
        ������,
        ������ʼ,
        ������ҡ,
        Ӳֱ

    }

    private void Awake()
    {
        binbing();
    }

    void binbing() //�󶨺���
    {
        cc = this.GetComponent<CharacterController>();
        ani = this.transform.GetChild(0).GetComponent<Animator>();
        hp = hpMax;
    }
    void Start()
    {
        Vector3 ������pos = this.transform.position;
        Ѳ�ߵ�array[0] = ������pos + new Vector3(Random.Range(1, 3), 0, Random.Range(1, 3));
        Ѳ�ߵ�array[1] = ������pos + new Vector3(Random.Range(-1, -3), 0, Random.Range(-1, -3));
    }

  
    void Update()
    {
        switch (״̬_)
        {
            case ����״̬.û��Ŀ��:
                ���Է���Ŀ��();
                break;
            case ����״̬.����Ŀ��:
                ���Ŀ�����();
                break;
            case ����״̬.׷��:
                ����׷��();
                break;
            case ����״̬.��������:
                ��Թ�������();
                break;
            case ����״̬.������ʼ:
                ��������();
                break;
            case ����״̬.����ǰҡ:
                ǰҡ();
                break;
            case ����״̬.������ҡ:
                ��ҡ();
                break;
            case ����״̬.����:
                ����();
                break;

        }
        

    }

   void ���Է���Ŀ��()
    {
        if(player == null )
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
       
        float distance = Vector3.Distance(this.transform.position, player.position);       
        Ѳ��();
       if(distance <= ������Χ)
        {
            ����Ŀ��();
        }
        else if(distance <= �Ӿ���Χ && ToolMethod.��ȡ��Ŀ��ļн�(player.position,this.transform)<60)  //Ŀ���������Ұ���ĵ�֮��н�С��Ұ������Ұ�нǵ�һ��
        {
            ����Ŀ��();
        }
        else
        {
            ���Ŀ��();
        }
    }
    void ����Ŀ��()
    {
        ״̬_ = ����״̬.����Ŀ��;
        Ŀ�� = player;
    }
    void ���Ŀ��()
    {
        ״̬_ = ����״̬.û��Ŀ��;
        Ŀ�� = null;
    }
    void Ѳ��()
    {
        this.transform.LookAt(Ѳ�ߵ�array[Ѳ�ߵ�Ŀ��index]);
        cc.Move(this.transform.forward * moveSpeed * Time.deltaTime);
        if (Vector3.Distance(this.transform.position, Ѳ�ߵ�array[Ѳ�ߵ�Ŀ��index])< 0.1f)
        {
            Ѳ�ߵ�Ŀ��index= ++Ѳ�ߵ�Ŀ��index % Ѳ�ߵ�array.Length;
        }
    }
   

    void ���Ŀ�����()
    {
        int R = Random.Range(0, 100);
        if(R <= ׷������)
        {
            ״̬_ = ����״̬.׷��;
        }
        else
        {
            ״̬_ = ����״̬.����;
        }
    }
    void ����׷��()
    {
        if(����Ƿ�ɹ���())
        {
            ״̬_ = ����״̬.��������;
            return;
        }
        if(����Ƿ�ʧȥĿ��())
        {
            ״̬_ = ����״̬.û��Ŀ��;
            return;
        }
        Vector3 pos = Ŀ��.position;
        pos.y = this.transform.position.y;
        this.transform.LookAt(pos);
        cc.Move(this.transform.forward * moveSpeed * Time.deltaTime);
    }
    bool ����Ƿ�ʧȥĿ��()
    {
        return false;
    }
    bool ����Ƿ�ɹ���()
    {
        return false;
    }
    void ��Թ�������()
    {

    }
    void ��������()
    {

    }
    void ǰҡ()
    {

    }
    void ��ҡ()
    {

    } 
    void ����()
    {

    }


}
