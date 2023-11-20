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

    public event Action OnClicked;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            OnClicked?.Invoke();        
    }

    // UI�� ��ġ�� ���� �ΰ��ӿ��� �������� �ʵ���.
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
}