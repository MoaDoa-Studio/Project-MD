using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Factory : Building
{
    [SerializeField]
    GameObject Builder_Menu;
        
    // ũ������ �� ����.
    private BuilderManager builderManager;
    private Vector3 origin_Position;

    // �ǹ� ���� ������.
    public int level; // ����.
    public bool state; // ��� ����.
    public int currentProduct; // ��������� ���귮.
    public void Start()
    {
        level = 1;
        state = false;
        currentProduct = 0;
        builderManager = GameManager.instance.builderManager;
        origin_Position = Vector3.zero;
    }

    protected override void OnMouseUp()
    {
        int currentBuilderMode = builderManager.get_BuilderMode();

        // UI�� Ŭ���ϴ� ������Ʈ�� ��ġ�� ���.
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (currentBuilderMode == 0) // NO
        {
            builderManager.ViewBuildingInfo(ID, level, currentPollution);
            return;
        }

        // �̹� ���õ� �ǹ��� �ִٸ� Ŭ�� �Ұ�.
        if (builderManager.mouseIndicator == this.gameObject)
            Builder_Menu.SetActive(true);
    }

    protected override void OnMouseDrag()
    {
        int currentBuilderMode = builderManager.get_BuilderMode();

        // UI�� Ŭ���ϴ� ������Ʈ�� ��ġ�� ���.
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (currentBuilderMode == 0) // NO
            return;
        else if (currentBuilderMode == 1) // Default
        {
            // ���� ������ �ǹ� �Ŵ����� ���� �� Relocate�� ����.
            if (builderManager.mouseIndicator == null)
            {
                builderManager.StartMove(ID, this.gameObject);
                origin_Position = gameObject.transform.position;
                builderManager.change_BuilderMode("Relocate");
            }
        }        
        else if (currentBuilderMode == 2) // Fix
        {
            if (isFixed == false)
                builderManager.moveItem();
        }
        else // Relocate
        {
            builderManager.moveItem();
        }
    }

    public void Click_FixButton()
    {
        // �浹 ���� �ǹ��� �ִٸ�?
        if (builderManager.checkCollide() == true)
            return;        

        builderManager.change_BuilderMode("Default");
        isFixed = true;

        Builder_Menu.SetActive(false);
        builderManager.StopMove();
    }

    public void Click_DestroyButton()
    {
        // ���õ� �ǹ��� �ı��� �� ����ϴ� �Լ�.
        if (builderManager.mouseIndicator != null)
            Destroy(builderManager.mouseIndicator);
        
        builderManager.change_BuilderMode("Default");
        Builder_Menu.SetActive(false);
        builderManager.StopMove();
    }

    public void Click_ReturnButton()
    {
        // ��ġ�� ĵ���ϰ� ���� ��ġ�� �ǵ��� ��.        
        if (origin_Position != Vector3.zero)
        {
            this.gameObject.transform.position = origin_Position;
            origin_Position = Vector3.zero;
        }

        builderManager.change_BuilderMode("Default");
        Builder_Menu.SetActive(false);
        builderManager.StopMove();
    }

    public void Click_QuitButton()
    {
        // �浹 ���� �ǹ��� �ִٸ�?
        if (builderManager.checkCollide() == true)
            return;
                
        builderManager.change_BuilderMode("Default");
        Builder_Menu.SetActive(false);
        builderManager.StopMove();
    }
}
