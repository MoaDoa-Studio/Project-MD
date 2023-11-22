using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class Building : MonoBehaviour
{
    [SerializeField]
    private int ID;

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
        Debug.Log("마우스 다운");
        // 수정모드인지 확인.
        if (GameManager.instance.builderManager.get_BuilderMode() < 2)
            return;

        // 여기서 선택한 건물 세팅해주고.
        GameManager.instance.builderManager.StartMove(ID, this.gameObject);
    }

    private void OnMouseDrag()
    {
        Debug.Log("드래그 중");
        // 수정 모드인지 확인.
        if (GameManager.instance.builderManager.get_BuilderMode() < 2)
            return;

        // 여기서 그리드 맞게 드래그 구현하자.
        GameManager.instance.builderManager.moveItem();
    }

}
