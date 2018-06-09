using UnityEngine;

public class Wall : MonoBehaviour
{
	// 인스펙터 노출 변수
	// 수치
	[SerializeField]
	private int		stack = 1;
	

	// 스택 재조정
	public void AddStack(int value)
	{
		stack += value;

		if (stack == 0)
		{
			Destroy(gameObject);
		}
	}
}
