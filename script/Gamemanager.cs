using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Gamemanager : MonoBehaviour
{
    private static Gamemanager instance;
    public static Gamemanager Instance()
    {
        return instance;
    }

    protected void Start()
    {
        if (Instruct.ItemList == null)
            Instruct.wepNum = 99;

  
    }
    protected void Awake()
    {

        instance=this;
    }

    public HUD hud;

    public GameObject csmimg;

    public GameObject Defaultimage;//失败面板

    public GameObject backgroundText;

    [SerializeField] private TeskHUD teskhud;

    public GameObject TestPanel;

  

    [SerializeField] private float sped;

    //跨场景要保存的东西
 




    //显隐动画
    public void BackgroundTextIsOn( )
    {
        TextMeshProUGUI _text = teskhud.t3_text;
        Debug.Log(_text.text);
        Animation anima = backgroundText.GetComponent<Animation>();
        AnimationClip clip = anima.GetClip("背景文本显隐动画");
        int num = _text.textInfo.characterCount;
        anima["背景文本显隐动画"].speed=Mathf.Max(anima["背景文本显隐动画"].speed, anima["背景文本显隐动画"].speed*(num%sped));
        anima.Play();
    }
    public void Teskison()
    {
        TestPanel.SetActive(true);
    }


    //关闭自己的父级
    public void Xbutton()
    {
        Button btn = UIEventResultButton();
                if (btn!=null)
                {
        Transform transParient = btn.gameObject.GetComponent<Transform>().parent;
            transParient.SetAsFirstSibling();
        transParient.gameObject.SetActive(false);

                }  
    }

    //检测UI button
    //如果检测有Button，就忽略Text,只返回第一个检测到的对象。
    public Button UIEventResultButton()
    {
        PointerEventData eventdata = new PointerEventData(EventSystem.current);
        eventdata.position=Input.mousePosition;
        List<RaycastResult> result = new List<RaycastResult>();
        GraphicRaycaster gr = this.GetComponent<GraphicRaycaster>();
        gr.Raycast(eventdata, result);
            Button btn=null;
        if (result.Count>0)
        {
      
           
            foreach (RaycastResult r in result)
            {
            if (r.gameObject.GetComponent<Button>()!=null)
                {
                    Debug.Log(r.gameObject.name);
               btn = r.gameObject.GetComponent<Button>();
                    break;
                }
            else if (r.gameObject.GetComponent<Image>()!=null&&r.gameObject.GetComponent<Button>()==null)
                {
                    break;
                }
           
            }
            //else if()

         

        }

        return btn;
    }

    //开启自己第一个子级
    public void _TrueButton()
    {
                Button butn = UIEventResultButton();
                if (butn!=null)
                {

                    Transform trans =butn.gameObject. GetComponent<Transform>();
                trans.SetAsLastSibling();
                if(trans.GetChild(0).GetChild(0).gameObject!=null)
                trans.GetChild(0).GetChild(0).gameObject.SetActive(true);
                }
    }

    //场景跳转专用
    public void ScenceConvert()
    {
        Button butn = UIEventResultButton();
        if (butn!=null)
        {
            SceneManager.LoadScene(butn.name);
            JsonSaveData.Instance().SaveData();
        }
    }
   //退出游戏
   public void ExitGame()
    {
#if UNITY_EDITOR
        JsonSaveData.Instance().SaveData();

        UnityEditor.EditorApplication.isPlaying=false;
#else
        JsonSaveData.Instance().SaveData();
        
        Application.Quit();
#endif
    }
    //UI开关一体专用
    public void UIOpenAndClose()
    {
        Button butn = UIEventResultButton();
      if(butn!=null)
      {
        GameObject obj = butn.transform.GetChild(0).GetChild(0).gameObject;
        if (obj.activeSelf==true)
        {
            obj.SetActive(false);
        }
        else if (obj.activeSelf==false)
        {
           obj.SetActive(true);
        }
      }
    }
    //3D物体控制UI
    public void OpenAndClose3D()
    {
        if (Instruct.openfalse==true)
        {
            csmimg.SetActive(true);
            Instruct.openfalse = false;
        }
        else if (Instruct.openfalse ==false)
        {
            csmimg.SetActive(false);
            Instruct.openfalse = true;
        }
    }
    public void IsDefault()   //切换场景,到初始界面。用json到传送门复活点。
    {

        Defaultimage.SetActive(false);
        SceneManager.LoadScene("scence1");
        Time.timeScale = 1;

        JsonSaveData.Instance().SaveData();

        //这里要调用json存档点，json有复活点，在复活点之前要制作保留新上传的数据的功能，在这里调用，这里只是重置HP
        //除此之外，要制作退出或者卡顿就自动保留的json，在设置面板调用。
    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += dataInstance;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= dataInstance;
    }
    public void dataInstance(UnityEngine.SceneManagement.Scene scene,LoadSceneMode mode)
    {

        JsonSaveData.Instance().SaveData();

        JsonSaveData.Instance().LoadData();
    }
}



