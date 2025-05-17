using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sounceAnim : MonoBehaviour
{
    public AudioSource souncePlay;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void FootSounce()
    {
        souncePlay.Play();
    }
    public void FootStop()
    {
        souncePlay.Stop();
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
