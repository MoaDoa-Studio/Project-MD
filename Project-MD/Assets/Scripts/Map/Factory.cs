using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Factory : Building
{
    [SerializeField]
    GameObject Builder_Menu;
        
    // 크래프팅 용 변수.
    private BuilderManager builderManager;
    private Vector3 origin_Position;

    // 건물 가변 데이터.
    public int level; // 레벨.
    public bool state; // 운용 상태.
    public int currentProduct; // 현재까지의 생산량.
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

        // UI와 클릭하는 오브젝트가 겹치는 경우.
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (currentBuilderMode == 0) // NO
        {
            builderManager.ViewBuildingInfo(ID, level, currentPollution);
            return;
        }

        // 이미 선택된 건물이 있다면 클릭 불가.
        if (builderManager.mouseIndicator == this.gameObject)
            Builder_Menu.SetActive(true);
    }

    protected override void OnMouseDrag()
    {
        int currentBuilderMode = builderManager.get_BuilderMode();

        // UI와 클릭하는 오브젝트가 겹치는 경우.
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (currentBuilderMode == 0) // NO
            return;
        else if (currentBuilderMode == 1) // Default
        {
            // 먼저 움직일 건물 매니저에 세팅 후 Relocate로 변경.
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
        // 충돌 중인 건물이 있다면?
        if (builderManager.checkCollide() == true)
            return;        

        builderManager.change_BuilderMode("Default");
        isFixed = true;

        Builder_Menu.SetActive(false);
        builderManager.StopMove();
    }

    public void Click_DestroyButton()
    {
        // 선택된 건물을 파괴할 때 사용하는 함수.
        if (builderManager.mouseIndicator != null)
            Destroy(builderManager.mouseIndicator);
        
        builderManager.change_BuilderMode("Default");
        Builder_Menu.SetActive(false);
        builderManager.StopMove();
    }

    public void Click_ReturnButton()
    {
        // 배치를 캔슬하고 원래 위치로 되돌릴 때.        
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
        // 충돌 중인 건물이 있다면?
        if (builderManager.checkCollide() == true)
            return;
                
        builderManager.change_BuilderMode("Default");
        Builder_Menu.SetActive(false);
        builderManager.StopMove();
    }
}
