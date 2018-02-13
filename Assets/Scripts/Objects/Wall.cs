using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    // 트리거 진입
    private void OnTriggerEnter2D(Collider2D other)
    { 
        // 볼일경우 튕겨냄
        if (other.tag == "Ball")
        {
            Vector2 ballVelocity = other.GetComponent<Rigidbody2D>().velocity;

            print(ballVelocity);
            other.GetComponent<Rigidbody2D>().velocity = new Vector2(ballVelocity.x * -1, ballVelocity.y);
        }
    }
}
