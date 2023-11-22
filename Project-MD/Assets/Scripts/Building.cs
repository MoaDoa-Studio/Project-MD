using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class Building : MonoBehaviour
{
    [SerializeField]
    private int ID;

    public bool isCollide = false; // �浹 üũ

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
        Debug.Log("���콺 �ٿ�");
        // ����������� Ȯ��.
        if (GameManager.instance.builderManager.get_BuilderMode() < 2)
            return;

        // ���⼭ ������ �ǹ� �������ְ�.
        GameManager.instance.builderManager.StartMove(ID, this.gameObject);
    }

    private void OnMouseDrag()
    {
        Debug.Log("�巡�� ��");
        // ���� ������� Ȯ��.
        if (GameManager.instance.builderManager.get_BuilderMode() < 2)
            return;

        // ���⼭ �׸��� �°� �巡�� ��������.
        GameManager.instance.builderManager.moveItem();
    }

}
