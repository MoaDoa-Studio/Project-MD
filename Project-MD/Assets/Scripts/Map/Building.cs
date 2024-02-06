using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class Building : MonoBehaviour
{
    [SerializeField]
    public int ID;
        
    public int currentPollution; // ������.    
    public bool isCollide = false; // �浹 üũ
    public bool isFixed = false;

    private void Start()
    {        
        currentPollution = 0;        
    }

    #region �浹 üũ
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

    #region ���콺 �Է� ����
    protected virtual void OnMouseUp() { }
    protected virtual void OnMouseDrag() { }
    #endregion
}