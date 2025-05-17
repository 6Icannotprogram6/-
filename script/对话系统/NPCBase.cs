using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCBase :MonoBehaviour,TeskAtribute
{
    private player _player;

    public string namess => throw new System.NotImplementedException();

    public string innertext => throw new System.NotImplementedException();

    public string backgroundInnertext {
        get
        {
            return TeskDatabase.Database().BackgroundInnertext;
        }
    }

    public GameObject obj => throw new System.NotImplementedException();

    public void OnBossDeath()
    {
        TeskHUD.HandInList.Add(this.gameObject);
    }


    public virtual void OnNPCClick()
    {
       Collider colid =  this.GetComponent<Collider>();
      
       

        if (Instruct.NPC.ContainsKey(this.gameObject.name)&&!TeskHUD.HandInList.Contains(this.gameObject))
        {
            
            TeskHUD.HandInList.Add(this.gameObject);
        }


    }



}
