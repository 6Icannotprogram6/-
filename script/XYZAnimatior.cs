using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public enum XYZStatude
{
    Idle,
    Running,
    Atk
}

public class XYZAnimatior : NPCBase
{
    public NavMeshAgent agent;
    public GameObject target;
    public Animator anim;




    public TeskInventory _teskinventory;

  


    public RectTransform recplayer;

    public int AtkDistance;
    public int IdleDis;


    public GameObject hand;

    [SerializeField] private float atkboxnum;
    [SerializeField] private float atktimenum;

    [SerializeField] private float WaitTime;

    public XYZStatude statude = XYZStatude.Idle;
    // Start is called before the first frame update
    void Start()
    {

   



        //Socket sockettype = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        //try
        //{
        //    IPEndPoint endpoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8080);
        //    sockettype.Bind(endpoint);
        //}
        //catch(Exception e)
        //{
        //    Console.WriteLine("�󶨱���" + e.Message);
        //    return;
        //}
        //sockettype.Listen(1024);
        //Console.WriteLine("������ɣ��ȴ��ͻ�������");

        //Socket socketCline = sockettype.Accept();
        //socketCline.Send(Encoding.UTF8.GetBytes("��ӭ��������"));
        //byte[] result = new byte[1024];



        //Ѱ·API
        //�Զ�Ѱ·����Ŀ���
        // agent.SetDestination();
        //��������������
        //ֹͣѰ·
        //agent.isStopped = true;
        //�ؼ�����
        //agent.speed;   �ٶ�
        //agent.acceleration;  ���ٶ�
        //agent.angularSpeed;   ��ת�ٶ�

        // if (agent.hasPath) { }   ��ǰ�Ƿ���·��
        //agent.isStopped   �Ƿ�ֹͣ
        //agent.path    ��ǰ·��
        //agent.pathStatus    ·��״̬
        //agent.updatePosition = true;  �Ƿ�
        //agent.updateRotation = true;   �Ƿ�
        //�����ٶ�
        //agent.velocity =Vector3    
        //agent.updateUpAxis = true;   �Ƿ���´����ٶ�

        //�ֶ�Ѱ·
        //NavMeshPath path = new NavMeshPath();
        //if (agent.CalculatePath(Vector3.zero, path))
        //{

        //}
        //agent.SetPath(path);
        //agent.ResetPath();
        //agent.Warp(Vector3.zero);


    }
  

    // Update is called once per frame
    void Update()
    {

        Vector3 agentvec = agent.transform.position;
        Vector3 targetvec = target.transform.position;
        float enmlength = Vector3.Distance(agentvec, targetvec);


        switch (statude)
        {
            case XYZStatude.Idle:
                agent.isStopped = true;
               

                    anim.SetBool("walk", false);
                    anim.SetBool("atkstatue", false);
                
                
                if(enmlength > AtkDistance&&enmlength<=IdleDis)
                {
                    statude = XYZStatude.Running;
                }
                    break;
            case XYZStatude.Running:
                agent.isStopped = false;
                agent.SetDestination(target.transform.position);
                    anim.SetBool("atkstatue", false);
                    anim.SetBool("walk", true);

                if (enmlength > IdleDis)
                {
                    statude = XYZStatude.Idle;
                }
                if(enmlength < AtkDistance)
                {
                    statude = XYZStatude.Atk;
                }
           
                break;

            case XYZStatude.Atk:

               
                agent.isStopped = true;
                anim.SetBool("walk", false);
                anim.SetBool("atkstatue", true);


                if(isFinishAnim)
                StartCoroutine(AtkTStatue());

                if (enmlength >= AtkDistance)
                {
                    statude = XYZStatude.Running;
                }
                    break;

        }

        //����һ������ֵ��ֻҪ����ײ��ⷶΧ�ڣ���ʼ�����������ֵ�����й��������������������ֵ����һ��������ֵ����С����һ��������ֵ�����Ĺ���������
        //update��һֱ�������ã�������ȵ������������ŵ���


    }

    static bool isFinishAnim=true;

 



    IEnumerator AtkTStatue()
    {
        //����״̬�����ԣ�getbool��ȡһ�κ�Ͳ��ٻ�ȡ
        if (anim.GetBool("atk1") || anim.GetBool("atk2") || anim.GetBool("atk3") || anim.GetBool("atk4"))
        {
            anim.SetBool("atk1", false);
            anim.SetBool("atk2", false);
            anim.SetBool("atk3", false);
            anim.SetBool("atk4", false);
            statude = XYZStatude.Idle;
            yield return new WaitForSeconds(1);
            statude = XYZStatude.Atk;
        }
       int a = UnityEngine.Random.Range(1, 5);
        if (a == 1)
        {
                anim.SetBool("atk1",true);
                OnXYZAtk("�ػ��ӽ�");
            isFinishAnim = false;
            yield return new WaitForSeconds(1);
            isFinishAnim = true;
        }
        else if (a == 2)
        {
                anim.SetBool("atk2", true);
                OnXYZAtk("�ӽ�");
            isFinishAnim = false;
            yield return new WaitForSeconds(1);
            isFinishAnim=true;
        }
        else if (a == 3)
        {
               anim.SetBool("atk3", true);
                OnXYZAtk("����");
            isFinishAnim = false;
            yield return new WaitForSeconds(1);
            isFinishAnim = true;
        }
        else if (a == 4)
        {
                anim.SetBool("atk4", true);
      
                OnXYZAtk("����");
            isFinishAnim = false;
            yield return new WaitForSeconds(1);
            isFinishAnim = true;
        }
    }



    private void OnXYZAtk(string audioSound)
    {
        GameObject obj = new GameObject();
        if (audioSound!= "xyz����")
        {
            hand.gameObject.GetComponent<BoxCollider>().enabled = true;


            obj.transform.parent = hand.transform;
        obj.transform.position = hand.transform.position;
        obj.transform.rotation = hand.transform.rotation;

        }
       

        AudioSource source = obj.AddComponent<AudioSource>();
        source.clip = Resources.Load<AudioClip>("audio/"+audioSound);
        source.volume = 1;
        source.Play();
        
       

            
    }




    static int xyzcount = 0;


   
    public void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "wupin")
        {
            collision.gameObject.GetComponent<BoxCollider>().enabled = false;
            xyzcount++;
            float bloodslider = 1;
            bloodslider = (float)(1 - Instruct.playerATK * xyzcount / Instruct.bossHP);
            recplayer.localScale = new Vector3(bloodslider, 1, 1);
            if (bloodslider <=0)
            {
                bloodslider = 0;
                recplayer.gameObject.SetActive(false);
                agent.isStopped = true;
                anim.SetBool("death", true);
                OnXYZAtk("xyz����");

                TeskAtribute atribute = this.GetComponent<TeskAtribute>();
                _teskinventory.TeskAtk(atribute);

            }

          

        }
    }


  


}
