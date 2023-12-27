using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Factory : Building
{
    public bool state; // ��� ����.
    public int currentProduct; // ��������� ���귮.

    public void Start()
    {
        state = false;
        currentProduct = 0;
    }

    protected override void OnMouseDown()
    {
        // UI�� Ŭ���ϴ� ������Ʈ�� ��ġ�� ���.
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (GameManager.instance.builderManager.get_BuilderMode() == 0) // NO
        {
            GameManager.instance.builderManager.set_BuildingInfo(ID, state, currentPollution);
            return;
        }
        else if (GameManager.instance.builderManager.get_BuilderMode() == 1) //Default
        {
            return;
        }
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

    protected override void OnMouseDrag()
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
