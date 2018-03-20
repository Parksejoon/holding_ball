using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaderManager : MonoBehaviour
{
	// 인스펙터 노출 변수
	// 일반
	[SerializeField]
	private Material backGround;
	[SerializeField]
	private Material ball;
	[SerializeField]
	private Material wall;


	// 색 변경
	private void ChangeColor(float H)
	{
		backGround.color = Color.HSVToRGB(H, 0.6f, 1f);
		ball.color = Color.HSVToRGB(H, 0.6f, 1f);
	}

	// 색 변경 코루틴 ( 무한 반복 )
	public IEnumerator ChangeColorLoop()
	{
		float hue = 0;

		while (true)
		{
			ChangeColor(hue++);

			yield return new WaitForSeconds(0.05f);
		}
	}
}
