using UnityEngine;

public class ParticleRotation : MonoBehaviour
{
	// 인스펙터 노출 변수
	// 수치
	[SerializeField]
	private float			speed;              // 회전 속도


	// 프레임
	private void FixedUpdate()
	{
		transform.Rotate(Vector3.forward * speed);
	}
}
