using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ItemClickHandler : MonoBehaviour
{
    public Inventory _Inventory;
    public player _player;
    public HUD hud;

    [SerializeField]private GameObject image;
    [SerializeField]private TeskInventory teskinventory;

    [SerializeField] private ItemDragHandler itemdrag;



    static bool istrue=false;

    public void OnItemClicked()
    {
      
       
            
        

        if (istrue)
        {
            image.SetActive(istrue);
            istrue = false;
        }
        else if (!istrue)
        {
            image.SetActive(istrue);
            istrue = true;
        }


        if (Instruct.ItemName.Count >= 0)
        {
            IInventoryItem itemss =itemdrag.Items;
        
            
                if (itemss != null)
                {
               

                if ( _player.RightHand.transform.childCount != 0)
                {
                    _player.RightHand.transform.GetChild(0). gameObject.SetActive(false);
                   
                    _player.RightHand.transform.GetChild(0).parent = null;

                }
                if (_player.LeftHand.transform.childCount != 0)
                {
                    _player.LeftHand.transform.GetChild(0).gameObject.SetActive(false);
                    _player.LeftHand.transform.GetChild(0).parent = null;
                }
                    
                    _Inventory.UseItem(itemss);
                InventoryItemBase itembase = (InventoryItemBase)itemss;
                TeskAtribute atribute = itembase.gameob.GetComponent<TeskAtribute>();

                teskinventory.TeskIntroduce(atribute);


                Instruct.wepNum = itemss.atk;

                _player.LeftHand.gameObject.GetComponent<BoxCollider>().enabled = false;
                
                }

            
            //else if (Input.GetMouseButtonDown(0))
            //{
            //    ItemPanel.SetActive(true);

            //}



        }
        


    }


  



    // Start is called before the first frame update
   private void Awake()
    {
        Animator anim = _player.anim;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
