using TMPro;
using Unity.Burst.CompilerServices;
using Unity.Services.Analytics;
using UnityEngine;
using UnityEngine.UI;


//�ȸ��½ű� �൱���¼�ϵͳ�е�ʵ���¼�ί�еĽ���ϵͳ
public class HUD : MonoBehaviour
{



    //
    public Inventory Inventory;//����¼�
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
    private void InvenToryScript_ItemAdded(object sender, InventoryEventArgs e)   //�Զ���ί������InvenToryScript_ItemAdded��+�Զ����� ��e��
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


    //һ���볡������item���ñ�����item�������а�

    public void Refresh()
    {
        int num = 0;
        foreach (Transform t in inventoryPanel.transform)//��������
        {
            Image image = t.Find("Imagess").GetComponent<Image>();
            TextMeshProUGUI textss = t.Find("num").GetComponent<TextMeshProUGUI>();
            ItemDragHandler itemDragHandler = t.Find("Imagess").GetComponent<ItemDragHandler>();
            if (Instruct.ItemList.Count > num)
            {
                foreach (GameObject obj2 in objs)//��������
                {
                    //ˢ���ڱ���λ�ã�����Id
                    IInventoryItem ii = obj2.GetComponent<IInventoryItem>();
                    //����һ�������ڱ�������id��ȵ�����
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
            num++;//����������뱳������Ʒ���һ��
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




