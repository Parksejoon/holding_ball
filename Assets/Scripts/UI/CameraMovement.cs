using UnityEngine;

public class CameraMovement : MonoBehaviour
{
	// 인스펙터 노출 변수
	// 일반
	[SerializeField]
	private new Camera 	camera;				// 대상 카메라
		
	// 수치
	[SerializeField]
	private Vector2 	limitPos;			// 최대치
		
	// 인스펙터 비노출 변수
	// 수치
	private float 		moveDis;			// 움직임 정도
	private Vector2 	newPos;				// 새로운 좌표
		
		
	// 프레임
	private void FixedUpdate()
	{
		newPos = -Camera.main.ScreenToWorldPoint(Input.mousePosition) * 0.1f;

		if (newPos.x < limitPos.x && newPos.x > -limitPos.x)
		{
			camera.transform.position = new Vector3(camera.transform.position.x, newPos.y, UIManager.instance.cameraZpos);
		}
			
		if (newPos.y < limitPos.y && newPos.y > -limitPos.y)
		{
			camera.transform.position = new Vector3(newPos.x, camera.transform.position.y, UIManager.instance.cameraZpos);	
		}
	}
}
