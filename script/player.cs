using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using static Unity.VisualScripting.Member;





public class player : MonoBehaviour
{

    public float speed = 0.01f;
    public CharacterController contro;
    public Vector3 veloc = Vector3.zero;  //速度向量
    public float margin = 1;
    public float rotateSpeed;


    public Transform FollowedCamera;
    public Animator anim;

    public Camera camerass;
    public Inventory inventory;//引用全局变量
    public GameObject LeftHand;
    public GameObject RightHand;
    public HUD Hud;
    public TeskHUD teskhud;


    public List<Collider> NPCobj = new List<Collider>();//存npc，改database中list值；
                                                        //让npcobj中的元素号跟文本中的子节点号对应，要重新加载lodatada，关键点是对应序列号并重新加载，要放在一个单






    public TeskInventory teskinventory;

    //失效时间
    public float _OutTime;
    private float OutTime;

    public RectTransform rectXYZ;
    public Vector3 hitoffset;
    [SerializeField] private float atkboxnum;
    [SerializeField] private float atktimenum;

    [SerializeField] private Gamemanager ison;


    void Start()
    {

        contro = GetComponent<CharacterController>();





    }



    void Update()
    {


        OutTime -= Time.deltaTime;
        if (OutTime <= 0)
        {
            num = 0;
            anim.SetInteger("匕首攻击", num);

        }

        AnimatorStateInfo animaterInfo;
        animaterInfo = anim.GetCurrentAnimatorStateInfo(0);

        //移动方向
        Vector3 mdir = Vector3.zero;

        if (contro.isGrounded)
        {
            float v = Input.GetAxis("Vertical");
            float h = Input.GetAxis("Horizontal");
            if (h != 0 || v != 0)
            {
                anim.SetBool("gotake", false);
                anim.SetBool("gowalk", true);
                if (v > 0)
                {
                    //将摄像机前方位移向量作为我们位移X方向向量
                    mdir = new Vector3(FollowedCamera.transform.forward.x * speed, 0, FollowedCamera.transform.forward.z * speed);
                    Quaternion newRotation = Quaternion.LookRotation(mdir);
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, newRotation, rotateSpeed);
                }       // vector3.MoveTowards和Vector3.RotateTowards     
                        //指定旋转：transform.rotation=Quaternion.Slerp(transform.rotation,target.Rotation,0.1f);    自身旋转：transform.rotate(Vector3 eulerAngles);
                if (v < 0)
                {
                    mdir = new Vector3(-FollowedCamera.transform.forward.x * speed, 0, -FollowedCamera.transform.forward.z * speed);
                    Quaternion newRotation = Quaternion.LookRotation(mdir);
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, newRotation, rotateSpeed);
                }
                if (h > 0)
                {
                    mdir = new Vector3(FollowedCamera.transform.right.x * speed, 0, FollowedCamera.transform.right.z * speed);
                    Quaternion newRotation = Quaternion.LookRotation(mdir);
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, newRotation, rotateSpeed);
                }
                if (h < 0)
                {
                    mdir = new Vector3(-FollowedCamera.transform.right.x * speed, 0, -FollowedCamera.transform.right.z * speed);
                    Quaternion newRotation = Quaternion.LookRotation(mdir);
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, newRotation, rotateSpeed);
                }
            }
            else if ((h == 0) && (v == 0))
            {
                anim.SetBool("gowalk", false);
            }
        }
        contro.SimpleMove(mdir * speed);

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);


            OnAtk(ray);

            OnClickNPC(ray);

            hitThis(ray);
            OnPutAllDoor(ray);


        }
        else if (Input.GetMouseButtonDown(1))
        {

            if (Instruct.wepNum == 1)
            {
                num = 4;

                AtkWay();
            }
            else if (Instruct.wepNum == 2)
            {
                num = 5;
                anim.SetTrigger("跳击");
                AtkWay();
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            anim.SetBool("防御", false);
        }

    }



    private void OnMove()
    {
        AnimatorStateInfo animaterInfo;
        animaterInfo = anim.GetCurrentAnimatorStateInfo(0);

        //移动方向
        Vector3 mdir = Vector3.zero;
        //bool isss = IsGrounded();
        if (contro.isGrounded)
        {
            anim.ResetTrigger("gotake");
            float h = Input.GetAxis("Horizontal");

            float v = Input.GetAxis("Vertical");



            if (h != 0 || v != 0)
            {
                //可能Input组件的h，v会一直增大，到移动结束后h，v才会传入信息，有播放延迟






                if (v > 0)
                {
                    //将摄像机前方位移向量作为我们位移X方向向量

                    mdir = new Vector3(FollowedCamera.transform.forward.x * speed, 0, FollowedCamera.transform.forward.z * speed);
                    Quaternion newRotation = Quaternion.LookRotation(mdir);
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, newRotation, rotateSpeed);
                }       // vector3.MoveTowards和Vector3.RotateTowards     
                        //指定旋转：transform.rotation=Quaternion.Slerp(transform.rotation,target.Rotation,0.1f);    自身旋转：transform.rotate(Vector3 eulerAngles);
                if (v < 0)
                {
                    mdir = new Vector3(-FollowedCamera.transform.forward.x * speed, 0, -FollowedCamera.transform.forward.z * speed);
                    Quaternion newRotation = Quaternion.LookRotation(mdir);
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, newRotation, rotateSpeed);
                }

                if (h > 0)
                {
                    mdir = new Vector3(FollowedCamera.transform.right.x * speed, 0, FollowedCamera.transform.right.z * speed);
                    Quaternion newRotation = Quaternion.LookRotation(mdir);
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, newRotation, rotateSpeed);
                }
                if (h < 0)
                {
                    mdir = new Vector3(-FollowedCamera.transform.right.x * speed, 0, -FollowedCamera.transform.right.z * speed);
                    Quaternion newRotation = Quaternion.LookRotation(mdir);
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, newRotation, rotateSpeed);
                }

                anim.SetBool("gowalk", true);

            }
            else if ((h == 0) && (v == 0))
            {

                anim.SetBool("gowalk", false);

            }

        }

        contro.SimpleMove(mdir * speed);
    }


    float angl = 0;
    private void OnPutAllDoor(Ray ray)
    {
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.tag == "door")
            {
                Transform doortrans = hit.collider.GetComponent<Transform>();
                angl += 90;
                Debug.Log("门");
                if (angl >= 180)
                {
                    angl = 0;

                }
                doortrans.localEulerAngles = new Vector3(0, 0, angl);

            }

        }
    }
    private void hitThis(Ray ray)
    {

        RaycastHit hit;


        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject.CompareTag("wupin"))
            {

                IInventoryItem Item = hit.collider.GetComponent<IInventoryItem>();     //事件拥有者，发起任务   拾起物品
                TeskAtribute tesk = hit.collider.GetComponent<TeskAtribute>();

                if (!Instruct.ItemList.Contains(Item.Name))
                {
                    if (teskhud.VoiceoverCollider.Contains(hit.collider))
                    {
                        Instruct.voiceoverNum = teskhud.VoiceoverCollider.IndexOf(hit.collider);
                    }


                    inventory.AddItem(Item);

                    if (Instruct.TeskIsFinish == 1)
                    {

                        teskinventory.TeskHandIn(tesk);

                    }

                    teskinventory.TeskVoiceovr(tesk);
                    anim.SetBool("gotake", true);
                    Hud.playtake("audio/拾取");

                }


            }
        }
    }
    public void OnClickNPC(Ray ray)
    {



        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Instruct.raydistance))
        {
            if (hit.collider.tag == "csm")
            { ison.OpenAndClose3D(); }

            if (hit.collider.tag == "npc")
            {
                ////用到FindIndex方法，用来查npc内数据在第几个元素，并将查到元素序号赋值到InListNode
                //Instruct.InListNode = NPCobj.IndexOf(hit.collider);
                //Debug.Log(Instruct.InListNode);


                if (Instruct.NPC.ContainsKey(hit.collider.gameObject.name))
                {
                    if (Instruct.NPC.TryGetValue(hit.collider.gameObject.name, out int value))
                    {

                        string path = Application.streamingAssetsPath + "/actorLink.xml";

                        DataBase.instance.LoadData(value, path);
                    }

                }


                //string path = Application.streamingAssetsPath + "/actorLink.xml";

                //DataBase.instance.LoadData(Instruct.InListNode, path);

                TalkingManager.Instance().creatrTextTalk();
                TeskAtribute atri = hit.collider.GetComponent<TeskAtribute>();

                if (Instruct.TeskIsFinish == 2)
                    teskinventory.TeskTalking(atri);


                Instruct.raydistance = 0;

            }



        }


    }
    public void OnAtk(Ray ray)
    {
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.name == "XYZ")
            {
                Instruct.weapon wep = new Instruct.weapon();

                wep = (Instruct.weapon)Instruct.wepNum;
                if (LeftHand.transform.childCount > 0)
                {
                    LeftHand.transform.gameObject.GetComponent<BoxCollider>().enabled = false;
                    LeftHand.transform.GetChild(0).gameObject.GetComponent<BoxCollider>().enabled = true;
                }
                else if (RightHand.transform.childCount > 0)
                {
                    //RightHand.transform.gameObject.GetComponent<BoxCollider>().enabled = false;
                    RightHand.transform.GetChild(0).gameObject.GetComponent<BoxCollider>().enabled = true;
                }
                if (wep == Instruct.weapon.匕首)
                {
                    OutTime = _OutTime;
                    num++;
                    OnPlayerAtkSword();

                }
                else if (wep == Instruct.weapon.盾)
                {
                    OnPlayerAtkShield();
                }
                else if (wep == Instruct.weapon.手)
                {
                    LeftHand.gameObject.GetComponent<BoxCollider>().enabled = true;
                    OnHandAtk();
                }



            }
        }




    }
    private void OnAtkSelect(Instruct.weapon weaponType)
    {
        if (weaponType == Instruct.weapon.匕首)
        {

        }
        else if (weaponType == Instruct.weapon.盾)
        {

        }
        else if (weaponType == Instruct.weapon.手)
        {

        }
    }
    static int num = 0;

    private Action<Instruct.weapon> Wepact;
    IEnumerator AtkWay()
    {

        GameObject obj = new GameObject();
        AudioSource source = obj.AddComponent<AudioSource>();


        //if (num == 0)
        //{

        //}
        //else
        if (num == 1)
        {
            source.clip = Resources.Load<AudioClip>("audio/攻击");


        }
        else if (num == 2)
        {

            source.clip = Resources.Load<AudioClip>("audio/匕首1");
        }
        else if (num == 3)
        {

            source.clip = Resources.Load<AudioClip>("audio/匕首3");

        }
        else if (num == 4)
        {
            anim.SetTrigger("匕首跳击");
            source.clip = Resources.Load<AudioClip>("audio/匕首跳击");
        }

        source.clip = Resources.Load<AudioClip>("audio/盾牌跳击");


        source.Play();
        Destroy(obj, atktimenum);
        yield return new WaitForSeconds(0.1f);

    }
    public void OnHandAtk()
    {


        Hud.playtake("audio/攻击");
        anim.SetTrigger("手击");


    }
    public void OnPlayerAtkSword()
    {



        if (num >= 3 || _OutTime == 0)
            num = 0;
        StartCoroutine(AtkWay());
        anim.SetInteger("匕首攻击", num);

    }
    public void OnPlayerAtkShield()
    {
        anim.SetBool("防御", true);

    }
    static int count = 0;
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.name == "剑")
        {
            collision.gameObject.GetComponent<BoxCollider>().enabled = false;
            count++;
            float bloadscale = 1;
            bloadscale = (float)(1 - Instruct.bossATK * count / Instruct.playerHP);

            rectXYZ.localScale = new Vector3(bloadscale, 1, 1);
            if (bloadscale <= 0)
            {
                bloadscale = 0;
                ison.Defaultimage.SetActive(true);
                Hud.playtake("主角倒地");
                anim.SetBool("dath", true);
                if (anim.GetBool("dath"))
                {
                    Time.timeScale = 0;
                }

            }

        }
    }

}
