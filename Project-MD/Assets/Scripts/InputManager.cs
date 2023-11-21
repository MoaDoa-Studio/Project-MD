using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class InputManager : MonoBehaviour
{
    private Vector3 lastPosition;

    [SerializeField]
    private LayerMask placement_Layermask;        

    // UI를 터치할 때는 인게임에서 반응하지 않도록.
    public bool IsPointerOverUI()
        => EventSystem.current.IsPointerOverGameObject();

    public Vector3 get_MousePosition()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane;
        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit,100,placement_Layermask))
        {
            lastPosition = hit.point;
        }
        return lastPosition;
    }

    public Vector3 get_CenterPosition()
    {
        Vector3 centerPos = new Vector3(Screen.width / 2, Screen.height / 2, 0);
        centerPos.z = Camera.main.nearClipPlane;
        Ray ray = Camera.main.ScreenPointToRay(centerPos);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100, placement_Layermask))
        {
            lastPosition = hit.point;
        }
        return lastPosition;
    }
}