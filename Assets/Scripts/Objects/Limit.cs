using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Limit : MonoBehaviour
{
    // 프레임 ( 삭제 처리 )
    void FixedUpdate()
    {
        if (transform.position.y >= 8)
        {
            Destroy(gameObject);
        }
    }
}
