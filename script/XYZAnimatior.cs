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
        //    Console.WriteLine("绑定报错" + e.Message);
        //    return;
        //}
        //sockettype.Listen(1024);
        //Console.WriteLine("监听完成，等待客户端连入");

        //Socket socketCline = sockettype.Accept();
        //socketCline.Send(Encoding.UTF8.GetBytes("欢迎进入服务端"));
        //byte[] result = new byte[1024];



        //寻路API
        //自动寻路设置目标点
        // agent.SetDestination();
        //点哪里向哪里走
        //停止寻路
        //agent.isStopped = true;
        //关键变量
        //agent.speed;   速度
        //agent.acceleration;  加速度
        //agent.angularSpeed;   旋转速度

        // if (agent.hasPath) { }   当前是否有路径
        //agent.isStopped   是否停止
        //agent.path    当前路径
        //agent.pathStatus    路径状态
        //agent.updatePosition = true;  是否
        //agent.updateRotation = true;   是否
        //代理速度
        //agent.velocity =Vector3    
        //agent.updateUpAxis = true;   是否更新代理速度

        //手动寻路
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

        //设置一个基础值，只要到碰撞检测范围内，就始终是这个基础值，所有攻击操作都大于这个基础值，上一个攻击的值必须小于下一个攻击的值，最后的攻击返还用
        //update会一直连续调用，并不会等到子任务结束后才调用


    }

    static bool isFinishAnim=true;

 



    IEnumerator AtkTStatue()
    {
        //动画状态机特性，getbool获取一次后就不再获取
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
                OnXYZAtk("重击挥剑");
            isFinishAnim = false;
            yield return new WaitForSeconds(1);
            isFinishAnim = true;
        }
        else if (a == 2)
        {
                anim.SetBool("atk2", true);
                OnXYZAtk("挥剑");
            isFinishAnim = false;
            yield return new WaitForSeconds(1);
            isFinishAnim=true;
        }
        else if (a == 3)
        {
               anim.SetBool("atk3", true);
                OnXYZAtk("旋击");
            isFinishAnim = false;
            yield return new WaitForSeconds(1);
            isFinishAnim = true;
        }
        else if (a == 4)
        {
                anim.SetBool("atk4", true);
      
                OnXYZAtk("连招");
            isFinishAnim = false;
            yield return new WaitForSeconds(1);
            isFinishAnim = true;
        }
    }



    private void OnXYZAtk(string audioSound)
    {
        GameObject obj = new GameObject();
        if (audioSound!= "xyz倒地")
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
                OnXYZAtk("xyz倒地");

                TeskAtribute atribute = this.GetComponent<TeskAtribute>();
                _teskinventory.TeskAtk(atribute);

            }

          

        }
    }


  


}
