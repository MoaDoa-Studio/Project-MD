using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class Building : MonoBehaviour
{
    [SerializeField]
    public int ID;
    
    public bool state; // 상태.
    public int pollution; // 오염도.
    public bool isCollide = false; // 충돌 체크
    public bool isFixed = false;

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

    private void OnMouseDown()
    {
        // UI와 클릭하는 오브젝트가 겹치는 경우.
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
            // Fix 모드인 경우에만 건물 값 세팅.
            if (isFixed == false)
                GameManager.instance.builderManager.StartMove(ID, this.gameObject);
        }
        else // Relocate
        {
            // 건물 값 세팅.
            GameManager.instance.builderManager.StartMove(ID, this.gameObject);
        }
    }

    private void OnMouseDrag()
    {
        // UI와 클릭하는 오브젝트가 겹치는 경우.
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (GameManager.instance.builderManager.get_BuilderMode() < 2) // NO, Default
            return;
        
        // Fix 모드인 경우에는, 선택한 건물만 움직이도록.
        if (GameManager.instance.builderManager.get_BuilderMode() == 2) // Fix
        {
            if (isFixed == false)
                GameManager.instance.builderManager.moveItem();
        }
        else // Relocate
            GameManager.instance.builderManager.moveItem();
    }        
}
