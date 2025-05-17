using UnityEngine;

public class InventoryItemBase : MonoBehaviour, IInventoryItem, TeskAtribute
{
    public Vector3 offeset=Vector3.zero;
    public Vector3 Itemscale=Vector3.zero;
    public virtual string Name
    {
        get
        {
            return this.gameObject.name;
        }
    }
    public Sprite _Image;
    public Sprite Image
    {
        get
        {
            return _Image;
        }
    }
    public GameObject gameob { get;set;
        //get
        //{
        //    return this.gameObject;
        //}
        //set
        //{
        //    gameob = value;
        //}
    }
    public GameObject obj
    {
        get {

            return gameob;
        }  
    }
    public int Id
    {
        get;set;
    }
    public string backgroundInnertext { get
        {
            return TeskDatabase.Database().backgroundInnertext;
        }
    }
    public int Atk=10;
    public int atk
    {
        get
        {
            return Atk;
        }
    }
    public Transform babatrans;
    public Transform babat
    {
        get
        {
            
                    return babatrans;

                
        }
    }
    Transform canvass
    {
        get
        {
            Transform canv = GameObject.Find("Canvas").transform;
            return canv;
        }
    }
    public TeskHUD huds
    {
        get
        {
            TeskHUD hud=canvass.GetComponent<TeskHUD>();
            return hud;
        }
    }
    Transform IInventoryItem.canvass => canvass;
    public string namess {
        get
        {
            return TeskDatabase.Database().namess;
        }
    }
    public string innertext
    {
        get
        {
            return TeskDatabase.Database().innertext;
        }
    }
    public virtual void OnUse()
    {



       
        Debug.Log(gameob.name);
        
        gameob.transform.position = babat.transform.position;
        babat.transform.eulerAngles =  offeset;
        gameob.transform.localScale = gameob.transform.localScale + Itemscale;
        gameob.transform.parent = babat.transform;
        for(int i = 0; i < babat.childCount; i++)
        {
             GameObject obj= babat.GetChild(i).gameObject;
            obj.SetActive(false);
          
        }
        Instruct.IntroduceNum = huds.IntroduceText.IndexOf(gameob.gameObject);
        //关闭collider或者调整到敌人层级

        gameob.layer = LayerMask.NameToLayer("weapon");
        gameob.SetActive(true);
    }
    public virtual void OnDrop()
    {

        gameob.transform.eulerAngles = new Vector3(0,0,0);

        gameob.transform.parent = null;
        gameob.SetActive(true);
        gameob.transform.position = Input.mousePosition;


        RaycastHit hit = new RaycastHit();
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 1000))
        {
            gameob.transform.position = hit.point;
            gameob.layer = LayerMask.NameToLayer("Default");
        }
    }
    public virtual void OnPickup()
    {

        gameob.SetActive(false);
        
        
        //StartCoroutine("WaitDead", 1);
    }
    public virtual bool addTesk()
    {
        throw new System.NotImplementedException();
    }
    void Update()
    {
        
    }
    public void OnNPCClick()
    {
        throw new System.NotImplementedException();
    }

    public void OnBossDeath()
    {
        throw new System.NotImplementedException();
    }
    //private void OnCollisionEnter(Collision collision)
    //{
    //    gameob.GetComponent<BoxCollider>().enabled = false;
    //}










    //IEnumerator WaitDead(float time)
    //{


    //    while (true)
    //    {
    //        _time += Time.deltaTime;
    //        yield return new WaitForEndOfFrame();
    //        mater.SetFloat("_TriggerDes", (float)_time / time);
    //        if (_time >= time)
    //        {
    //            _time = 1;
    //            mater.SetFloat("_TriggerDes", 1);


    //            Destroy(this.gameObject, 0.5f);


    //            yield break;

    //        }



    //    }



    //}

}
