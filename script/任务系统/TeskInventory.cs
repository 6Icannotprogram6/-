using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using System;
using System.Timers;


public class TeskInventory : MonoBehaviour
{
    private List<TeskAtribute> teskatributeList = new List<TeskAtribute>();
    public event EventHandler<eventArga> Teskadd;
    public event EventHandler<eventArga> TeskhandIn;
    public event EventHandler<eventArga> Teskatk;
    public event EventHandler<eventArga> Tesktalking;
    public event EventHandler<eventArga> TeskVoiceover;
    public event EventHandler<eventArga> Teskintroduce;










    //完成任务要交互物品，故需要一个gameobject类链表，存储物品，并且要配对，配对作为一个需要加到hud上，并且要在inventory声明的任务
    //当完成任务时，要实现文本交换下一个任务
    //任务类型分为上交物品任务，对话接取任务，怪物波次任务。

    

   public void TeskAdd(TeskAtribute atribute)
    {
        teskatributeList.Add(atribute);
        //获取库里文本


        if (Teskadd != null)
        {
            //通知事件监听
            Teskadd(this, new eventArga(atribute));
        }

    }
    //这里实现对HUD内容和数据库的数据上传
    public void TeskHandIn(TeskAtribute atribute)
    {
       //确定事件的传入，再确定先从哪里传入，再确定要传入的方法。

   
        teskatributeList.Add(atribute);

        


        if (TeskhandIn != null)
        {
            TeskhandIn(this, new eventArga(atribute));
        }

 
    }
    public void TeskTalking(TeskAtribute atribute)
    {
      
        teskatributeList.Add(atribute);

            atribute.OnNPCClick();

        if (Tesktalking != null)
        {
            Tesktalking(this, new eventArga(atribute));
        }

        
    }

    public void TeskAtk(TeskAtribute atribute)
    {
       
        
        teskatributeList.Add(atribute);
        atribute.OnBossDeath();

        if (Teskatk != null)
        {
            Teskatk(this, new eventArga(atribute));
        }
        
        
    }
 
    public void TeskVoiceovr(TeskAtribute atribute)
    {
        teskatributeList.Add(atribute);
       
        if (TeskVoiceover != null)
        {
         
            TeskVoiceover(this, new eventArga(atribute));

        }
    }
    public void TeskIntroduce(TeskAtribute atribute)
    {
        teskatributeList.Add(atribute);

       
            this.Teskintroduce(this, new eventArga(atribute));
        
    }

    
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}






