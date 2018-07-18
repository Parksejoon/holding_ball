using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
	// 초기화
	private void Awake()
	{
		new ShopParser();
	}
}
