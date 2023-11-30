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
        if (GameManager.instance.builderManager.get_BuilderMode() == 0)
        {
            GameManager.instance.builderManager.set_BuildingInfo(ID, state, pollution);
            
            return;
        }        
        else if (GameManager.instance.builderManager.get_BuilderMode() == 1)
            return;

        // 건물 값 세팅.        
        GameManager.instance.builderManager.StartMove(ID, this.gameObject);
    }

    private void OnMouseDrag()
    {
     
        // 수정 모드인지 확인.
        if (GameManager.instance.builderManager.get_BuilderMode() < 2)
            return;

        // 드래그
        GameManager.instance.builderManager.moveItem();
    }

        
}
