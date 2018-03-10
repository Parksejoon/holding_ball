using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public Rigidbody2D rigidbody2d;

    
    void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();

		rigidbody2d.AddForce(Vector2.up * 100);
	}
}
