using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class listing : MonoBehaviour
{
    public AudioSource source;
    public AudioClip[] clip;
    public int num=0;
    public Slider slid;
    // Start is called before the first frame update
    void Start()
    {
     
        
        source.Play();
    }

    // Update is called once per frame
    void Update()
    {
        source.volume = slid.value;
    }
   

    public void sourceplay()
    {
        source.Play();

    }
    public void sourcestop()
    {
        source.Stop();
    }
    public void sourceConvert()
    {
        if (clip != null)
        {
            num = num + 1;
            source.clip = clip[num];
            source.Play();
        }
        if (num == clip.Length-1)
        {
            num = 0;
        }
    }
}
