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

    public GameObject Defaultimage;//ʧ�����

    public GameObject backgroundText;

    [SerializeField] private TeskHUD teskhud;

    public GameObject TestPanel;

  

    [SerializeField] private float sped;

    //�糡��Ҫ����Ķ���
 




    //��������
    public void BackgroundTextIsOn( )
    {
        TextMeshProUGUI _text = teskhud.t3_text;
        Debug.Log(_text.text);
        Animation anima = backgroundText.GetComponent<Animation>();
        AnimationClip clip = anima.GetClip("�����ı���������");
        int num = _text.textInfo.characterCount;
        anima["�����ı���������"].speed=Mathf.Max(anima["�����ı���������"].speed, anima["�����ı���������"].speed*(num%sped));
        anima.Play();
    }
    public void Teskison()
    {
        TestPanel.SetActive(true);
    }


    //�ر��Լ��ĸ���
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

    //���UI button
    //��������Button���ͺ���Text,ֻ���ص�һ����⵽�Ķ���
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

    //�����Լ���һ���Ӽ�
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

    //������תר��
    public void ScenceConvert()
    {
        Button butn = UIEventResultButton();
        if (butn!=null)
        {
            SceneManager.LoadScene(butn.name);
            JsonSaveData.Instance().SaveData();
        }
    }
   //�˳���Ϸ
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
    //UI����һ��ר��
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
    //3D�������UI
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
    public void IsDefault()   //�л�����,����ʼ���档��json�������Ÿ���㡣
    {

        Defaultimage.SetActive(false);
        SceneManager.LoadScene("scence1");
        Time.timeScale = 1;

        JsonSaveData.Instance().SaveData();

        //����Ҫ����json�浵�㣬json�и���㣬�ڸ����֮ǰҪ�����������ϴ������ݵĹ��ܣ���������ã�����ֻ������HP
        //����֮�⣬Ҫ�����˳����߿��پ��Զ�������json�������������á�
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



