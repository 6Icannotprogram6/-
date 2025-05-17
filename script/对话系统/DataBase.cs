using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using UnityEngine.EventSystems;


public struct Instruct
{
    public static int InListNode=0;//背包物品及画外音的描述信息下标
    public static int inInnertext = 0;//InInnertext中文本

    public static int raydistance = 10;

    


    [SerializeField] public static List<string> InInnertexts = new List<string>();

    [SerializeField]public static Dictionary<string, int> NPC = new Dictionary<string, int>();
    [SerializeField] public static Dictionary<int, string> TeskDic = new Dictionary<int, string>();

    //3D开关一体
    public static bool openfalse = false;


    public static Dictionary<string,int> ItemName=new Dictionary<string, int>();//存键值
    public static List<string> ItemList = new List<string>();                   //存键
    public static void AddItem(string Key,int value)
    {
        ItemList.Add(Key);
        ItemName.Add(Key, value);
    }

    public static void RemoveItem(string Key)
    {
        ItemList.Remove(Key);
        ItemName.Remove(Key);
    }



    //拖拽事件导致camera旋转，用其限制
    public static bool OnRotate = false;




    public static double playerHP=400;
    public static double playerATK=10;

    public static double bossHP=1000;
    public static double bossATK=10;

   

    public static int voiceoverNum;

    public static int IntroduceNum;


    
    public static int TeskIsFinish = 1;//任务是否完成，完成就更新
  
 

    public static int num = 0;//文本下标号

    public static int UILayer = LayerMask.NameToLayer("UI");


    public static int wepNum = 99;//武器id号

    public  enum weapon
    {
        手 = 99,
        匕首 = 1,
        盾 = 2,
        剑 = 3
    }

    public static bool IsOnUI()
    {
        PointerEventData eventdata = new PointerEventData(EventSystem.current);
        eventdata.position = Input.mousePosition;
        List<RaycastResult> result = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventdata, result);
        if (result.Count > 0)
        {
            for (int i = 0; i < result.Count; i++)
            {
                if (result[i].gameObject.layer == Instruct.UILayer)
                {
                    if (result[i].gameObject.GetComponent<CanvasGroup>()==true)
                    {
                        CanvasGroup grou = result[i].gameObject.GetComponent<CanvasGroup>();
                        if (grou.alpha!=0)
                        {
                            return true;

                        }
                        else
                        {
                            return false;
                        }

                    }

                    return true;


                }
            }
            return false;
        }
        else return false;
    }



    //public static Camera camera = Resources.Load<Camera>("prefab/camera");
    // public static GameObject player = Resources.Load<GameObject>("prefab/player");
    // public static Canvas canvas = Resources.Load<Canvas>("prefab/Canvas");



}




public class DataBase
{
    //public TextMeshProUGUI e_text;
    //单例确保传进来数据安全
    public static DataBase instance = new DataBase();
    public static DataBase Instance => instance;



  


    public int countinNode;//返回的list孩子数量




    private void Start()
    {
       

    }

    public void LoadData(int inListNode,string fileName)
    {
        XmlDocument xml = new XmlDocument();
        xml.Load(fileName);
        XmlNode actor = xml.SelectSingleNode("actor");
        string TextInNode;
            XmlNode talkList = actor.ChildNodes[inListNode];
            Instruct.InInnertexts.Clear();
            for (int i = 0; i < talkList.ChildNodes.Count; i++)
            {
               TextInNode = talkList.ChildNodes[i].InnerText;
            Instruct.InInnertexts.Add(TextInNode);
            }
    }



    //public void SaveDate(object data, string fileName)
    //{
    //    string path = Application.persistentDataPath + "/" + fileName + ".xml";

    //    using (StreamWriter sw = new StreamWriter(path))
    //    {
    //        XmlSerializer s = new XmlSerializer(data.GetType());
    //        s.Serialize(sw, data);
    //    }
    //}

    ////对与已经加载好的文本进行读取。
    ////首先在unity内的类创建xml时传入的是一个object，即class类，加上我们需要自定义的文件名
    //public object LoadDate(Type type, string fileName)
    //{
    //    //声明路径
    //    string path = Application.persistentDataPath + "/" + fileName + ".xml";
    //    if (!File.Exists(path))
    //    {
    //        path = Application.streamingAssetsPath + "/" + fileName + ".xml";
    //        print(Application.persistentDataPath);
    //        if (!File.Exists(path))
    //        {
    //            return null;
    //        }
    //    }
    //    using (StreamReader sr = new StreamReader(path))
    //    {

    //        XmlSerializer serilzer = new XmlSerializer(typeof(actor));
    //        return serilzer.Deserialize(sr);
    //    }

    //}


}
