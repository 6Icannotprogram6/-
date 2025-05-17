using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.Services.Analytics.Internal;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class camera : MonoBehaviour
{
    public float sensitivityMouseX = 10f;
    public float sensitivityMouseY = 10f;
    public Transform target;
    //观察距离
    public float Distance = 0.4F;
    //旋转角度
    private float mx = 0.0F;
    private float my = 0.0F;
    //角度限制
   public float minlimity = -45;
    public float maxlimity = 45;
    //是否启用差值
    public bool isNeedDamping = true;
    //速度
    public float Damping = 2.5F;
    public float rx = 0;
    public float ry = 0;
    public float speed = 0;
    public Vector3 cun;   //当前旋转角度
    // Update is called once per frame
    void LateUpdate()
    {

        //范围限制
        
        if (Input.GetMouseButton(0))
        {
            if (Instruct.IsOnUI()||Instruct.OnRotate==true)
            {
                return;
            }
            else
            {
                mx += Input.GetAxis("Mouse X") * sensitivityMouseX * -2F;
            my += Input.GetAxis("Mouse Y") * sensitivityMouseY * -2F;

            }
        }
        //范围限制
        my = ClampAngle(my,minlimity, maxlimity);

        //重新计算位置和角度
        Quaternion mRotation = Quaternion.Euler(my*speed, mx*speed, 0);
            Vector3 mPosition = mRotation * new Vector3(rx, ry, -Distance) + target.position;
            //设置相机角度和位置
            if (isNeedDamping)
        {
            //球形差值
            transform.rotation = Quaternion.Slerp(transform.rotation, mRotation, Time.deltaTime);
            //线性差值
            transform.position = Vector3.Lerp(transform.position, mPosition, Time.deltaTime * Damping);
        }   
        else
        {
            transform.rotation = mRotation;
            transform.position = mPosition;
        }
    }
    private float ClampAngle(float angle,float min,float max)
    {
        if (angle < -360) angle += 360;
        if (angle > 360) angle -= 360;
        return Mathf.Clamp(angle, min, max);
    }



}
