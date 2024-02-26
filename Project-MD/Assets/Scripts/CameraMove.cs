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
        desiredPosition = target.position + offset;
       
        Vector3 TargetDist = transform.position - desiredPosition;
        TargetDist = Vector3.Normalize(TargetDist);

        // 카메라의 대상 위치간의 보간
        Vector3 pos = Vector3.Lerp(transform.position, desiredPosition, smooth);

        target.position = pos; // 카메라 위치 업데이트
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
            transform.position = target.position + new Vector3(offset.x,offset.y,offset.z);
        }
    }
}
