using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChase : MonoBehaviour
{
    private Transform ballTransform;            // 공의 트랜스폼


    // 초기화
    void Awake()
    {
        ballTransform = GameObject.Find("Ball").GetComponent<Transform>();
    }

    // 프레임
    void Update()
    {
        if (transform.position != ballTransform.position)
        {
            transform.position = new Vector3(Mathf.Lerp(transform.position.x, ballTransform.position.x, 0.5f), transform.position.y, -10);

        }
    }
}
