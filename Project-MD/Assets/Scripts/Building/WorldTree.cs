using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WorldTree : Building
{
    [SerializeField]
    private GameObject WorldTree_UI;
    //protected override void OnMouseDown()
    //{
    //    // UI와 클릭하는 오브젝트가 겹치는 경우.
    //    if (EventSystem.current.IsPointerOverGameObject())
    //        return;

    //    if (GameManager.instance.builderManager.get_BuilderMode() == 0) // NO
    //    {
    //        WorldTree_UI.SetActive(true);
    //        return;
    //    }
    //    else if (GameManager.instance.builderManager.get_BuilderMode() == 1) //Default
    //    {
    //        return;
    //    }
    //    else if (GameManager.instance.builderManager.get_BuilderMode() == 2) // Fix
    //    {
    //        // Fix 모드인 경우에만 건물 값 세팅.
    //        if (isFixed == false)
    //            GameManager.instance.builderManager.StartMove(ID, this.gameObject);
    //    }
    //    else // Relocate
    //    {
    //        // 건물 값 세팅.
    //        GameManager.instance.builderManager.StartMove(ID, this.gameObject);
    //    }
    //}

    //protected override void OnMouseDrag()
    //{
    //    // UI와 클릭하는 오브젝트가 겹치는 경우.
    //    if (EventSystem.current.IsPointerOverGameObject())
    //        return;

    //    if (GameManager.instance.builderManager.get_BuilderMode() < 2) // NO, Default
    //        return;

    //    // Fix 모드인 경우에는, 선택한 건물만 움직이도록.
    //    if (GameManager.instance.builderManager.get_BuilderMode() == 2) // Fix
    //    {
    //        if (isFixed == false)
    //            GameManager.instance.builderManager.moveItem();
    //    }
    //    else // Relocate
    //        GameManager.instance.builderManager.moveItem();
    //}
}
