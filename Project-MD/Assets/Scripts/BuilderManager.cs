using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BuilderManager : MonoBehaviour
{
    [SerializeField]
    private GameObject Builder_UI;
    [SerializeField]
    private GameObject GridLine; // �׸��� ǥ��

    // �ӽ� ���µ�.
    private GameObject Center_UI;    
    BuilderMode builderMode { get; set; }
    enum BuilderMode
    {
        No, // ������ �ƴ�
        Default, // �⺻ ���� ���        
    }

    private void Start()
    {
        Center_UI = Builder_UI.transform.Find("Center_UI").gameObject;        
        builderMode = BuilderMode.No;
    }
    
    // ���� ��� ����.
    public void change_BuilderMode(bool toggle)
    {
        if (toggle)        
            builderMode = BuilderMode.Default;                    
        else
            builderMode = BuilderMode.No;

        GridLine.SetActive(toggle);
    }

    // �ǹ� �׸� ��.
    public void click_Building_Tap(int index)
    {
        for(int i = 0; i < Center_UI.transform.childCount; i++)
        {
            // ���ϴ� ���� ���� ���.
            if (i == index)            
                Center_UI.transform.GetChild(i).gameObject.SetActive(true);
            else
                Center_UI.transform.GetChild(i).gameObject.SetActive(false);
        }
    }
}
