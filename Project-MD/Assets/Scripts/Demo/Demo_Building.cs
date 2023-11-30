using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Demo_Building : MonoBehaviour
{
    [SerializeField]
    public int ID;
    [SerializeField]
    public float product;
    [SerializeField]
    public float productivity;
    [SerializeField]
    public GameObject building_State_Canvas;    
   
    public bool state; // ����.
    public int pollution; // ������.
    public bool isCollide = false; // �浹 üũ
    public bool isFixed = false;

    private float curTime = 0.0f;
    private Slider product_Slider;

    private void Start()
    {
        product_Slider = building_State_Canvas.transform.GetChild(ID).Find("Info").Find("Total_Text").Find("Slider").GetComponent<Slider>();
    }

    private void FixedUpdate()
    {
        curTime += Time.deltaTime;

        if (curTime > productivity)
        {
            product_Slider.value += product;
            curTime = 0.0f;
        }                
    }

    #region �浹 üũ
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

    private void OnMouseDown()
    {
        Debug.Log(GameManager.instance.builderManager.get_BuilderMode());

        if (GameManager.instance.builderManager.get_BuilderMode() == 0) // NO
        {
            for(int i = 0; i < 3; i++)
            {
                if (ID == i)
                    building_State_Canvas.transform.GetChild(i).gameObject.SetActive(true);
                else
                    building_State_Canvas.transform.GetChild(i).gameObject.SetActive(false);
            }
            return;
        }
        else if (GameManager.instance.builderManager.get_BuilderMode() == 1) //Default
            return;
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

    private void OnMouseDrag()
    {
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
