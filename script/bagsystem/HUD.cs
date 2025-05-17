using TMPro;
using Unity.Burst.CompilerServices;
using Unity.Services.Analytics;
using UnityEngine;
using UnityEngine.UI;


//热更新脚本 相当于事件系统中的实现事件委托的交互系统
public class HUD : MonoBehaviour
{



    //
    public Inventory Inventory;//添加事件
    public GameObject MessagePanel;
    public Transform inventoryPanel;
    [SerializeField] private player _playe;

    GameObject[] objs;

    //public GameObject butt;





    //Start is called before the first frame update
    void Start()
    {
        Inventory.ItemAdded += InvenToryScript_ItemAdded;
        Inventory.ItemRemoved += Inventory_ItemRemoved;

        objs = GameObject.FindGameObjectsWithTag("wupin");
        _playe.LeftHand.SetActive(true);

        Refresh();
    }
    private void InvenToryScript_ItemAdded(object sender, InventoryEventArgs e)   //自定义委托名（InvenToryScript_ItemAdded）+自定义类 （e）
    {
        int index = Instruct.ItemList.Count;
        Instruct.AddItem(e.Item.Name, index);
        Transform imageTransform = inventoryPanel.GetChild(index);
        Image image = imageTransform.Find("Imagess").GetComponent<Image>();
        TextMeshProUGUI textss = imageTransform.Find("num").GetComponent<TextMeshProUGUI>();
        ItemDragHandler itemDragHandler = imageTransform.Find("Imagess").GetComponent<ItemDragHandler>();
        if (Instruct.ItemList.Count <= Inventory.SLOTS)
        {
            textss.enabled = true;
            textss.text = e.Item.Name;
            image.enabled = true;
            image.sprite = e.Item.Image;
            e.Item.Id = index;
            if (!Instruct.ItemName.ContainsKey(e.Item.Name))
                Instruct.AddItem(e.Item.Name, index);
            itemDragHandler.Items = e.Item;
            Refresh();
            e.Item.OnPickup();
        }
    }
    private void Inventory_ItemRemoved(object sender, InventoryEventArgs e)
    {



        Transform imageTransform = inventoryPanel.GetChild(e.Item.Id);



        Image image = imageTransform.Find("Imagess").GetComponent<Image>();
        TextMeshProUGUI textss = imageTransform.Find("num").GetComponent<TextMeshProUGUI>();
        ItemDragHandler itemDragHandler = imageTransform.Find("Imagess").GetComponent<ItemDragHandler>();

        textss.enabled = false;
        textss.text = null;
        image.enabled = false;
        image.sprite = null;
        itemDragHandler.Items = null;




        Instruct.RemoveItem(e.Item.Name);
        int num = 0;

        foreach (string key in Instruct.ItemList)
        {

            GameObject obj = null;
            foreach(GameObject obj2 in objs)
            {
                if (obj2.name == key)
                {
                    obj = obj2;
                    break;
                }
            }

           IInventoryItem i = obj.GetComponent<IInventoryItem>();
            i.Id=num;
            Instruct.ItemName[key] = num;
            num++;
        }
      
        

        Refresh();

    }


    public void Inventory_ItemConvert()
    {

    }


    //一进入场景根据item表让背包中item跟场景中绑定

    public void Refresh()
    {
        int num = 0;
        foreach (Transform t in inventoryPanel.transform)//遍历背包
        {
            Image image = t.Find("Imagess").GetComponent<Image>();
            TextMeshProUGUI textss = t.Find("num").GetComponent<TextMeshProUGUI>();
            ItemDragHandler itemDragHandler = t.Find("Imagess").GetComponent<ItemDragHandler>();
            if (Instruct.ItemList.Count > num)
            {
                foreach (GameObject obj2 in objs)//遍历场景
                {
                    //刷新在表中位置，根据Id
                    IInventoryItem ii = obj2.GetComponent<IInventoryItem>();
                    //返回一个存在在表中且与id相等的物体
                    if (ii.Id == num)
                    {
                        foreach (string item in Instruct.ItemList)
                        {
                            if (item == ii.Name)
                            {
                            image.sprite = ii.Image;
                            textss.text = ii.Name;
                            image.enabled = true;
                            textss.enabled = true;
                            ii.gameob = obj2;
                            itemDragHandler.Items = ii;
                            }
                        }

                    }
                }
            }
            else
            {
                image.sprite = null;

                textss.text = null;
                image.enabled = false;
                textss.enabled = false;
                itemDragHandler.Items = null;
            }
            num++;//格子数编号与背包中物品编号一样
        }
    }



    public void playtake(string resName)
    {
        GameObject musicObj = new GameObject();
        AudioSource a = musicObj.AddComponent<AudioSource>();
        a.clip = Resources.Load<AudioClip>(resName);
        a.volume = 1;
        a.Play();
        Destroy(musicObj, 0.5f);

    }





    public void OpenMessagePanel(string text)
    {
        MessagePanel.SetActive(true);

    }

    public void CloseMessagePanel()
    {
        MessagePanel.SetActive(false);
    }

    //// Update is called once per frame
    //void Update()
    //{

    //}
}




