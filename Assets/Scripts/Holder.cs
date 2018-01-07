using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Holder : MonoBehaviour
{


    // 움직임 처리
    void FixedUpdate()
    {
        transform.Translate(Vector3.up * Time.deltaTime * GameManager.moveSpeed);
    }
}
