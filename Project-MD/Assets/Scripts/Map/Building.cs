using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class Building : MonoBehaviour
{
    [SerializeField]
    public int ID;
        
    public int currentPollution; // 오염도.    
    public bool isCollide = false; // 충돌 체크
    public bool isFixed = false;

    private void Start()
    {        
        currentPollution = 0;        
    }

    #region 충돌 체크
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Building")
            isCollide = true;
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Building")
            isCollide = true;
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Building")
            isCollide = false;
    }
    #endregion

    #region 마우스 입력 관련
    protected virtual void OnMouseUp() { }
    protected virtual void OnMouseDrag() { }
    #endregion
}