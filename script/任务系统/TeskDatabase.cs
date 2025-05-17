using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Xml;
using System.Text.RegularExpressions;

//任务接口
public interface TeskAtribute
{
    public string namess { get; }

    public string innertext { get; }
    public string backgroundInnertext { get; }
 

    public GameObject obj { get; }

    public void OnNPCClick();
    public void OnBossDeath();

    
}


public class eventArga :EventArgs 
{
    public TeskAtribute teskattribute;
    public eventArga(TeskAtribute e)
    {
        teskattribute = e;
    }
}



public class TeskDatabase:MonoBehaviour,TeskAtribute   //发送任务事件的拥有者,任务触发器：
{
    private static TeskDatabase database = new TeskDatabase();
    public static TeskDatabase Database()
    {
        return database;


    }

    protected void Awake()
    {
        database = this;
    }





    public string Names;
   public string innerText;

    public string BackgroundInnertext;
   

 




  



    // Start is called before the first frame update

    //在这里要单纯传输Tesk内数据
    public void TeskLoadData(int num,int tags)
    {
        XmlDocument xml = new XmlDocument();
        string path = Application.streamingAssetsPath + "/Tesk.xml";
        xml.Load(path);
        XmlNode Tesks = xml.SelectSingleNode("Tesks");


         
        if (tags == 0)
        {

            XmlNode Tesk = Tesks.SelectSingleNode("TeskAttribute");
            XmlNodeList Tesk1 = Tesk.SelectNodes("Tesk1");
            if (num < Tesk1.Count)
            {

                Names = Tesk1[num].Attributes["string"].Value;
                innerText = Tesk1[num].Attributes["num"].Value;
       

            }
            else
            {
                Names = null;
                innerText = "已完成所有任务";
            }
        }
        else if (tags == 1)
        {
            XmlNode backTesk = Tesks.SelectSingleNode("backgroundsounce");
            XmlNodeList backgroundsounce = backTesk.SelectNodes("bgs");
           
            if (num < backgroundsounce.Count)
            {
                Debug.Log(backgroundsounce[num].InnerText);

                BackgroundInnertext = backgroundsounce[num].InnerText;
            }
        }
        else if (tags == 2)
        {
            XmlNode TextTesk = Tesks.SelectSingleNode("ItemInnerText");
            XmlNodeList TextTesks = TextTesk.SelectNodes("text");
            if (num < TextTesks.Count)
            {
              
                innerText = TextTesks[num].Attributes["string"].Value;
            }
        }
      
       
       
    }

    
    private void Start()
    {
        //TeskAtribute atribute = this.GetComponent<TeskAtribute>();

        //inventory.TeskAdd(this);
    }

    public void OnNPCClick()
    {
        throw new NotImplementedException();
    }

    public void OnBossDeath()
    {
        throw new NotImplementedException();
    }


    public string namess
    {
        get
        {
            return Names;
        }
    }
    public string innertext {
        get
        {
            return innerText;
        }
    }

    public string backgroundInnertext
    {
        get
        {
            Debug.Log(BackgroundInnertext);
             return BackgroundInnertext;
        }

    }

 

    public GameObject obj => throw new NotImplementedException();

  
}


