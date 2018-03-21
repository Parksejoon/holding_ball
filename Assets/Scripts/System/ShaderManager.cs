using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaderManager : MonoBehaviour
{
	// 인스펙터 노출 변수
	// 일반
	[SerializeField]
	private Material	backGround;				// 뒷배경 질감
	[SerializeField]
	private Material	ball;					// 공 질감
	[SerializeField]
	private Material	wall;                   // 벽 질감
	[SerializeField]
	private Material	warWall;                // 위험 벽 질감



	// 시작
	private void Start()
	{
		StartCoroutine(ChangeColorLoop());
	}

	// 색 변경
	private void ChangeColor(float H)
	{
		backGround.SetColor("_TopColor", Color.HSVToRGB(H, 0.3f, 0.9f));
		backGround.SetColor("_BotColor", Color.HSVToRGB(H, 0.8f, 0.2f));
		wall.SetColor("_Color", Color.HSVToRGB(H, 0.45f, 1f));
		ball.SetColor("_Color", Color.HSVToRGB(H, 0.6f, 1f));
	}

	// 색 변경 코루틴 ( 무한 반복 )
	public IEnumerator ChangeColorLoop()
	{
		float hue = 0;

		while (true)
		{
			ChangeColor(hue);
			hue = (hue + 0.01f) % 1f;

			yield return new WaitForSeconds(0.05f);
		}
	}
}
