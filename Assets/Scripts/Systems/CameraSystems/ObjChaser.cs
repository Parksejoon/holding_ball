using UnityEngine;

public class ObjChaser : MonoBehaviour
{
	// 인스펙터 노출 변수
	// 수치
	[SerializeField]
	private float		speed = 0.05f;                  // 카메라 이동 속도
	[SerializeField]
	private Vector3		focusPos;						// 고정 위치
	[SerializeField]
	private Transform	chaseObject;                    // 쫒아갈 오브젝트    

	public  float		limitX = 6f;					// 최대 X
	public  float		limitY = 2.15f;					// 최대 Y

	// 인스펙터 비노출 변수
	// 일반
	private Vector3		temp;							// 계산용


	// 프레임
	private void FixedUpdate()
	{
		temp = new Vector3(Mathf.Lerp(transform.position.x, chaseObject.position.x - focusPos.x, speed),
			Mathf.Lerp(transform.position.y, chaseObject.position.y - focusPos.y, speed), focusPos.z);

		if (temp.x < limitX && temp.x > -limitX)
		{
			transform.position = new Vector3(temp.x, transform.position.y, focusPos.z);
		}

		if (temp.y < limitY && temp.y > -limitY)
		{
			transform.position = new Vector3(transform.position.x, temp.y, focusPos.z);
		}
	}
}
