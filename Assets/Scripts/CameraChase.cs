using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChase : MonoBehaviour
{
    // 인스펙터 노출 변수
    [SerializeField]
    private float     speed = 0.05f;               // 카메라 이동 속도
    [SerializeField]
    private float     fixX;                        // 고정 x축 좌표
    [SerializeField]
    private float     fixY;                        // 고정 y축 좌표
    [SerializeField]
    private Transform chaseObject;                 // 쫒아갈 오브젝트    
    

    // 프레임
    void FixedUpdate()
    {
        transform.position = new Vector3(Mathf.Lerp(transform.position.x, chaseObject.position.x - fixX, speed), Mathf.Lerp(transform.position.y, chaseObject.position.y - fixY, speed), -10);
    }
}
