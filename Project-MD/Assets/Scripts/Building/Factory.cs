using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// ���� �ǹ�
public class Factory : Building
{
    [SerializeField]
    GameObject Builder_Menu;
        
    // ũ������ �� ����.
    private BuilderManager builderManager;
    private Vector3 origin_Position;

    // �ǹ� ���� ������.
    public int level;
    public bool state;
    public int currentProduct;
    public int currentNpcID;

    public void Start()
    {
        level = 1;
        state = false;
        currentProduct = 0;
        currentNpcID = -1;
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
            builderManager.ViewFactoryInfo(this);
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
                builderManager.ChangeBuilderMode("Relocate");
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

    #region ���� ��ư ����
    public void Click_FixButton()
    {
        // �ǹ� �浹 üũ
        if (builderManager.checkCollide())
            return;        

        isFixed = true;
        ResetBuilderMode();
    }
    public void Click_DestroyButton()
    {
        // ���õ� �ǹ� �ı� �Լ�.
        if (builderManager.mouseIndicator != null)
            Destroy(builderManager.mouseIndicator);

        ResetBuilderMode();
    }
    public void Click_ReturnButton()
    {
        // ��ġ�� ĵ���ϰ� ���� ��ġ�� �ǵ��� ��.
        if (origin_Position != Vector3.zero)
        {
            this.gameObject.transform.position = origin_Position;
            origin_Position = Vector3.zero;
        }

        ResetBuilderMode();
    }
    public void Click_QuitButton()
    {
        // �ǹ� �浹 üũ.
        if (builderManager.checkCollide() == true)
            return;
        ResetBuilderMode();
    }

    void ResetBuilderMode()
    {
        builderManager.ChangeBuilderMode("Default");
        Builder_Menu.SetActive(false);
        builderManager.StopMove();
    }
    #endregion
}
