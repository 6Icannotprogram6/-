using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Rooltext : MonoBehaviour
{
    public TextMeshProUGUI E_text;
    public Animator animator;
    [SerializeField] private player _player;
    [SerializeField] private AlphaChange alpha;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    public void ShowText( )
    {
        if (Instruct.InInnertexts.Count>Instruct.inInnertext)
        {
            animator.SetTrigger("OnClick");
        E_text.text = Instruct.InInnertexts[Instruct.inInnertext];
        Instruct.inInnertext++;
        }
        else if(Instruct.InInnertexts.Count <= Instruct.inInnertext)
        {
            Instruct.inInnertext = Instruct.InInnertexts.Count;
            alpha.ChangeAlpha(0, 2, null);
            StartCoroutine(InvokTime());
        }
    }
    IEnumerator InvokTime()
    {
        yield return new WaitForSeconds(6f);
        Instruct.raydistance = 10;
        Instruct.inInnertext = 0;
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
