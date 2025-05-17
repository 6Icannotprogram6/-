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










    //�������Ҫ������Ʒ������Ҫһ��gameobject�������洢��Ʒ������Ҫ��ԣ������Ϊһ����Ҫ�ӵ�hud�ϣ�����Ҫ��inventory����������
    //���������ʱ��Ҫʵ���ı�������һ������
    //�������ͷ�Ϊ�Ͻ���Ʒ���񣬶Ի���ȡ���񣬹��ﲨ������

    

   public void TeskAdd(TeskAtribute atribute)
    {
        teskatributeList.Add(atribute);
        //��ȡ�����ı�


        if (Teskadd != null)
        {
            //֪ͨ�¼�����
            Teskadd(this, new eventArga(atribute));
        }

    }
    //����ʵ�ֶ�HUD���ݺ����ݿ�������ϴ�
    public void TeskHandIn(TeskAtribute atribute)
    {
       //ȷ���¼��Ĵ��룬��ȷ���ȴ����ﴫ�룬��ȷ��Ҫ����ķ�����

   
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






