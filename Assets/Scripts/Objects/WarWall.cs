using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarWall : MonoBehaviour
{
	// 인스펙터 비노출 변수
	// 일반


	// 매프레임
	void Update()
	{
		transform.Rotate(Vector3.forward * 0.3f);
	}
}
