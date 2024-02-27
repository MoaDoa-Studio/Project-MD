using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    // 카메라가 따라다닐 대상.
    public Transform target;
    public Vector3 offset;
    Vector3 desiredPosition;
    public float smooth = 0.1f;
    public void npc_CameraMove(GameObject _gameObject)
    {
        target = _gameObject.transform;
    }

    private void Update()
    {
        if (target == null) 
        { 
            this.transform.position = new Vector3(0, 4, -3.7f);
            return; 
        }
        else
        {
            desiredPosition = target.position + offset;
            Debug.Log("desiredPosition: " + desiredPosition);
            //transform.position = target.position + new Vector3(offset.x,offset.y,offset.z);
            transform.position = Vector3.Lerp (transform.position, desiredPosition, smooth);
        }
    }
}
