using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ScenceInformation
{

    // Start is called before the first frame update
   
    public ScenceInformation()
    {
         Camera obj =   Resources.Load<Camera>("prefab/camera");
        GameObject player = Resources.Load<GameObject>("prefab/player");
        Canvas canvas=Resources.Load<Canvas>("prefab/Canvas");

    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
