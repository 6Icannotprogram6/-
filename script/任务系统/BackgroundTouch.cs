using UnityEngine;

public class BackgroundTouch :NPCBase
{
    [SerializeField]private TeskHUD teskhud;
    [SerializeField]private TeskInventory teskinventory;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "player")
        {
            Collider colid = this.GetComponent<Collider>();
            Instruct.voiceoverNum = teskhud.VoiceoverCollider.IndexOf(colid);
    
            TeskAtribute atribute = this.GetComponent<TeskAtribute>();
            teskinventory.TeskVoiceovr(atribute);
      


        }
    }

    

}
