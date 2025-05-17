using System;
using System.Collections;
using System.Collections.Generic;

using TMPro;

using UnityEngine;

public class TeskHUD : MonoBehaviour
{
  

    



    public TeskInventory Inventory;

    public TextMeshProUGUI t1_text;
    public TextMeshProUGUI t2_text;

    public TextMeshProUGUI t3_text;

    public TextMeshProUGUI t4_text;


    public static List<GameObject> HandInList = new List<GameObject>();//任务完成度

    public List<Collider> VoiceoverCollider = new List<Collider>();
    public List<GameObject> IntroduceText = new List<GameObject>();


    [SerializeField] private Gamemanager ison;



    //这个委托是用来装需要让任务执行下去的物品
    //当物品到委托后，调用委托会实现让任务继续下去
    //Action<eventArga> action = delegate (eventArga a) { 
    //    Indel = true; 
    //    t1_text.text = a.teskattribute.names;
    //    t2_text.text = a.teskattribute.Innertext;
    //}; //判断物体是否获得或条件是否达到

   

    private void Start()
    {

        Inventory.Teskadd += _teskAdd;
        Inventory.TeskhandIn += _TeskHandIn;
        Inventory.Teskatk += _TeskAtk;
        Inventory.Tesktalking += _TeskTalking;
        Inventory.TeskVoiceover += _TeskVoiceover;
        Inventory.Teskintroduce += _TeskIntroduce;

       
            Inventory.TeskAdd(base.GetComponent<TeskAtribute>());
        
    }

    private void _TeskIntroduce(object sender, eventArga e)
    {
        Debug.Log(Instruct.IntroduceNum);
        TeskDatabase.Database().TeskLoadData(Instruct.IntroduceNum,2);

        t4_text.text = e.teskattribute.innertext;

    }

   
  
                        //文本转换
    public void _teskAdd(object sender,eventArga a)
    {

        TeskDatabase.Database().TeskLoadData(Instruct.num, 0);
       


        t1_text.text = a.teskattribute.namess;
        t2_text.text = a.teskattribute.innertext + "/" +HandInList.Count;

        
        ison.Teskison();
       
  
     
    }

    
    public void _TeskHandIn(object sender, eventArga a)
    {
        if(HandInList.Contains(a.teskattribute.obj)==false)
        HandInList.Add(a.teskattribute.obj);



        if (HandInList.Count >= 6)
        {
            Instruct.TeskIsFinish = 2;
            Instruct.num++;
            HandInList.Clear();
        }


            Inventory.TeskAdd(TeskDatabase.Database().GetComponent<TeskAtribute>());
            
    }
    private void _TeskTalking(object sender, eventArga a)
    {

        //_player.NPCobj.Contains

       
        
        if (HandInList.Count >= 4)
        {
            Instruct.TeskIsFinish = 3;
            Instruct.num++;
           HandInList.Clear();
        }
            Inventory.TeskAdd(TeskDatabase.Database().GetComponent<TeskAtribute>());

    }




    public void _TeskAtk(object sender,eventArga a)
    {

        t2_text.text = 1 + "/" + HandInList.Count;
        Instruct.num++;
        Inventory.TeskAdd(TeskDatabase.Database().GetComponent<TeskAtribute>());
    }



    private void _TeskVoiceover(object sender, eventArga e)
    {


        TeskDatabase.Database().TeskLoadData(Instruct.voiceoverNum, 1);
       
        t3_text.text = e.teskattribute.backgroundInnertext;

        ison.BackgroundTextIsOn();

    }

    



    // Update is called once per frame
    void Update()
    {
        
    }



    



}
