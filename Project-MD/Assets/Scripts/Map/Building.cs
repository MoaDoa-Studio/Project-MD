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
    public bool isFixed = false;

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
        // UI�� Ŭ���ϴ� ������Ʈ�� ��ġ�� ���.
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (GameManager.instance.builderManager.get_BuilderMode() == 0) // NO
        {
            GameManager.instance.builderManager.set_BuildingInfo(ID, state, pollution);
            return;
        }
        else if (GameManager.instance.builderManager.get_BuilderMode() == 1) //Default
            return;
        else if (GameManager.instance.builderManager.get_BuilderMode() == 2) // Fix
        {
            // Fix ����� ��쿡�� �ǹ� �� ����.
            if (isFixed == false)
                GameManager.instance.builderManager.StartMove(ID, this.gameObject);
        }
        else // Relocate
        {
            // �ǹ� �� ����.
            GameManager.instance.builderManager.StartMove(ID, this.gameObject);
        }
    }

    private void OnMouseDrag()
    {
        // UI�� Ŭ���ϴ� ������Ʈ�� ��ġ�� ���.
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (GameManager.instance.builderManager.get_BuilderMode() < 2) // NO, Default
            return;
        
        // Fix ����� ��쿡��, ������ �ǹ��� �����̵���.
        if (GameManager.instance.builderManager.get_BuilderMode() == 2) // Fix
        {
            if (isFixed == false)
                GameManager.instance.builderManager.moveItem();
        }
        else // Relocate
            GameManager.instance.builderManager.moveItem();
    }        
}
