using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public GameObject bindedHolder;         // 볼이 바인딩되어있는 홀더


    // 움직임 처리
    void FixedUpdate()
    {
        transform.Translate(Vector3.down * Time.deltaTime * (GameManager.moveSpeed / 3));
    }

    // 홀더에 바인딩
    public void BindingHolder(GameObject holder)
    {
        bindedHolder = holder;
        GameManager.moveSpeed = 0.3f;
    }
}
