using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Limit : MonoBehaviour
{
    // 수치
    private float limitY = 20;           // 소멸좌표


    // 프레임 ( 삭제 처리 )
    void FixedUpdate()
    {
        if (transform.position.y >= limitY)
        {
            Destroy(gameObject);
        }
    }
}
