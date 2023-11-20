using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BuilderManager : MonoBehaviour
{
    [SerializeField]
    private GameObject Builder_UI;
    [SerializeField]
    private GameObject GridLine; // 그리드 표시

    // 임시 에셋들.
    private GameObject Center_UI;    
    BuilderMode builderMode { get; set; }
    enum BuilderMode
    {
        No, // 건축모드 아님
        Default, // 기본 건축 모드        
    }

    private void Start()
    {
        Center_UI = Builder_UI.transform.Find("Center_UI").gameObject;        
        builderMode = BuilderMode.No;
    }
    
    // 건축 모드 진입.
    public void change_BuilderMode(bool toggle)
    {
        if (toggle)        
            builderMode = BuilderMode.Default;                    
        else
            builderMode = BuilderMode.No;

        GridLine.SetActive(toggle);
    }

    // 건물 테마 탭.
    public void click_Building_Tap(int index)
    {
        for(int i = 0; i < Center_UI.transform.childCount; i++)
        {
            // 원하는 빌딩 탭인 경우.
            if (i == index)            
                Center_UI.transform.GetChild(i).gameObject.SetActive(true);
            else
                Center_UI.transform.GetChild(i).gameObject.SetActive(false);
        }
    }
}
