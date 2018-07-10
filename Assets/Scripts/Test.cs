using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
	public Material ballAfterEffect;

	public Sprite target;

	private void Start()
	{
		ballAfterEffect.SetTexture("_MainTex", target.texture);
	}
}
