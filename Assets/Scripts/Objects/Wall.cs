using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    // 인스펙터 노출 변수
    [SerializeField]
    private int reflexY = 1;                // 물체의 X 반전여부
    [SerializeField]
    private int reflexX = 1;                // 물체의 Y 반전여부


    // 트리거 진입
    private void OnTriggerEnter2D(Collider2D other)
    { 
        // 볼일경우 튕겨냄
        if (other.tag == "Ball")
        {
            Vector2 ballVelocity = other.GetComponent<Rigidbody2D>().velocity;
            
            other.GetComponent<Rigidbody2D>().velocity = new Vector2(ballVelocity.x * reflexX, ballVelocity.y * reflexY);
        }
    }
}
