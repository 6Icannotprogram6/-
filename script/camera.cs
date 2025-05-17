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
    //�۲����
    public float Distance = 0.4F;
    //��ת�Ƕ�
    private float mx = 0.0F;
    private float my = 0.0F;
    //�Ƕ�����
   public float minlimity = -45;
    public float maxlimity = 45;
    //�Ƿ����ò�ֵ
    public bool isNeedDamping = true;
    //�ٶ�
    public float Damping = 2.5F;
    public float rx = 0;
    public float ry = 0;
    public float speed = 0;
    public Vector3 cun;   //��ǰ��ת�Ƕ�
    // Update is called once per frame
    void LateUpdate()
    {

        //��Χ����
        
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
        //��Χ����
        my = ClampAngle(my,minlimity, maxlimity);

        //���¼���λ�úͽǶ�
        Quaternion mRotation = Quaternion.Euler(my*speed, mx*speed, 0);
            Vector3 mPosition = mRotation * new Vector3(rx, ry, -Distance) + target.position;
            //��������ǶȺ�λ��
            if (isNeedDamping)
        {
            //���β�ֵ
            transform.rotation = Quaternion.Slerp(transform.rotation, mRotation, Time.deltaTime);
            //���Բ�ֵ
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
