using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class Building : MonoBehaviour
{
    [SerializeField]
    public int ID;
    
    public bool state; // ����.
    public int pollution; // ������.
    public bool isCollide = false; // �浹 üũ

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

    private void OnMouseDown()
    {                
        if (GameManager.instance.builderManager.get_BuilderMode() == 0)
        {
            GameManager.instance.builderManager.set_BuildingInfo(ID, state, pollution);
            
            return;
        }        
        else if (GameManager.instance.builderManager.get_BuilderMode() == 1)
            return;

        // �ǹ� �� ����.        
        GameManager.instance.builderManager.StartMove(ID, this.gameObject);
    }

    private void OnMouseDrag()
    {
     
        // ���� ������� Ȯ��.
        if (GameManager.instance.builderManager.get_BuilderMode() < 2)
            return;

        // �巡��
        GameManager.instance.builderManager.moveItem();
    }

        
}
