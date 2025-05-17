using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemTF : MonoBehaviour
{
    private static bool istf=false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.activeSelf == false)
        {
            istf = false;
            this.gameObject.SetActive(istf);
        }
    }
}
