using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class Building : MonoBehaviour
{
    [SerializeField]
    private int building_Num;

    public bool isCollide = false; // 충돌 체크    
    public bool isDisplayed = false; // 배치 상태 체크.

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

    private void OnMouseUp()
    {
        Debug.Log("dhsak");
        if (isDisplayed == false)
            return;
        isDisplayed = false;
        GameManager.instance.placementSystem.StartMove(building_Num, this.gameObject);
    }

}
