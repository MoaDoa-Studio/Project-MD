using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// WayPoint 정보(위치 정보)를 가져온다
public class WayPoint : MonoBehaviour
{

    // WayPoint의 Transform 배열변수 선언
    public static Transform[] points;

    private void Awake()
    {   
        // transform의 자식 오브젝트 갯수를 가져와서 points 배열 방 생성
        points = new Transform[this.transform.childCount];
    
        for(int i = 0; i < points.Length; i++)
        {
            // points 배열 방마다 자식 오브젝트의 transform 초기화
            points[i] = transform.GetChild(i);
        }
    
    }
}
