using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChase : MonoBehaviour
{
    // 일반 변수
    public  Transform chaseObject;              // 쫒아갈 오브젝트

    // 수치
    public  float     speed = 0.05f;            // 카메라 이동 속도
    public  float     fixX;                     // 고정 x축 좌표
    public  float     fixY;                     // 고정 y축 좌표


    // 프레임
    void Update()
    {
        transform.position = new Vector3(Mathf.Lerp(transform.position.x, chaseObject.position.x - fixX, speed), Mathf.Lerp(transform.position.y, chaseObject.position.y - fixY, speed), -10);
    }
}
