using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    // Start is called before the first frame update


    void Start()
    {
        
    }

    // Update is called once per frame
    public void OpenDoor(RaycastHit hit )
    {
        Transform doortrans = hit.collider.transform;
        float angl = 0;
        angl=90;
        doortrans.localEulerAngles=new Vector3(0, 0, angl);
        if (angl==90)
        {
            doortrans.localEulerAngles=new Vector3(0, 0, 0);

        }
    }
    void Update()
    {
        
    }
}
