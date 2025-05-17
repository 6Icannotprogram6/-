using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkingManager : MonoBehaviour
{
    public AlphaChange alpha;
    public Rooltext Rool;

    private static TalkingManager instance;
    public static TalkingManager Instance()
    {
        return instance;
    }

    private void Awake()
    {
        instance = this;
    }

    public void creatrTextTalk()
    {
        if (Instruct.InInnertexts.Count > Instruct.inInnertext)
        {
            Debug.Log(Instruct.InInnertexts.Count);
            Rool.ShowText();
            alpha.ChangeAlpha(1, 2, null);
        }
        else if (Instruct.InInnertexts.Count <= Instruct.inInnertext)
            return;

    }


}
