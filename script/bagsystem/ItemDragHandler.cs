using System.Collections;
using System.Collections.Generic;
using Unity.IO.Archive;
using Unity.Services.Analytics.Internal;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//事件拥有者
public class ItemDragHandler : MonoBehaviour, IDragHandler, IEndDragHandler //IDragHandler（常用）：鼠标按下拖动时执行（只要鼠标在拖动就一直执行）
{
   
   
    
    public Inventory _Inventory;
    public GameObject bags;
   

    public IInventoryItem Items { get; set; }

   

    private void Start()
    {
        //PointerEventData eventData = new PointerEventData(EventSystem.current);
        //EventTrigger trigger = bags.gameObject.GetComponent<EventTrigger>();
        //EventTrigger.Entry entrys = new EventTrigger.Entry();
        //entrys.eventID = EventTriggerType.PointerExit;
        //entrys.callback.AddListener((data) => OnDrag(eventData));
        //entrys.callback.AddListener((datas) => OnEndDrag(eventData));
        //trigger.triggers.Add(entrys);

    }
    private void Update()
    {

        
      
   
        
    }
    public void OnDrag(PointerEventData eventData)
    {

        transform.position = Input.mousePosition;
        //PointerEventData eventdata = new PointerEventData(EventSystem.current);
        //eventdata.position = Input.mousePosition;
        //eventdata.pointerEnter = bags.gameObject;

        Instruct.OnRotate = true;


        _Inventory.DropItem(Items); 
    
     



    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if(Instruct.ItemName.Count>=0)
        {

           

            transform.localPosition = Vector3.zero;
            Instruct.OnRotate = false;


            _Inventory.RemoveItem(Items); 
              





            //if (Instruct.IsOnUI())
            //{
            //    return;
            //}
            //else
            //{

            //_Inventory.RemoveItem(Items);

            //}



        }
    }

   

    //private void ExitItem(BaseEventData data)
    //{
    //    _Inventory.DropItem(Items);
    //}

    // Start is called before the first frame update

    // Update is called once per frame

}
