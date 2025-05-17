using System.Collections;
using System.Collections.Generic;

using UnityEngine;



public class RectTransformForText : MonoBehaviour
{
    //通过resource中预制体来对应反射中的内容，这里只需要对应人名

    // Start is called before the first frame update

    void Start()
    {
        
       NPCreflect();

       
        //if (!Instruct.OnDontDes)
        //{
        //    Instruct.OnDontDes = true;

        //    GameObject obj = new GameObject();
        //    obj = this.gameObject;
        //    DontDestroyOnLoad(obj);
        //    obj.AddComponent<UnDes>();
        //    TeskInventory Inventory = GameObject.Find("Canvas").GetComponent<TeskInventory>();

        //    Inventory.TeskAdd(TeskDatabase.Database().GetComponent<TeskAtribute>());

        //    //SceneManager.sceneLoaded += OnLoadScenceSave;
        //}

    }

    //private void OnLoadScenceSave(Scene scene,LoadSceneMode mode)
    //{

    //    TeskInventory Inventory = GameObject.Find("Canvas").GetComponent<TeskInventory>();

    //        Inventory.TeskAdd(TeskDatabase.Database().GetComponent<TeskAtribute>());





    //}
    public void NPCreflect()
    {
        GameObject[] _name = Resources.LoadAll<GameObject>("prefab/NPC");
       
        foreach(GameObject item in _name)
        {
            if (!Instruct.NPC.ContainsKey(item.name))
            {
            Instruct.NPC.Add(item.name, Instruct.InListNode);
                Instruct.InListNode++;
            }

        }

        //foreach(GameObject obj in _obj)
        //{

        //}


          
    }






    // Update is called once per frame
    void Update()
    {
        
    }
}



