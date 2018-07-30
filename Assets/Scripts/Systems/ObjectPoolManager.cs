using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
	// 인스펙터 노출 변수
	// 일반
	[SerializeField]
	private GameObject				targetObject;							// 타겟 오브젝트

	// 수치
	[SerializeField]
	private int						poolSize;								// 기본 풀 크기
	[SerializeField]
	private string					defualtTag;								// 원래 태그
	[SerializeField]
	private Vector2					defualtPosition;						// 원래 위치


	// 인스펙터 비노출 변수
	// 일반
	private Queue<GameObject>		objectPool = new Queue<GameObject>();	// 오브젝트 풀(탄막)


	// 시작 초기화 
	private void Awake()
	{
		Initialize();
	}

	// 프레임
	private void Update()
	{
		Debug.Log(objectPool.Count);
	}

	// 초기화 
	private void Initialize()
	{
		for (int i = 0; i < poolSize; i++)
		{
			ResetTarget(Instantiate(targetObject, Vector2.zero, Quaternion.identity, transform));
		}
	}

	// 타겟 초기화 
	private void ResetTarget(GameObject target)
	{
		target.SetActive(false);
		target.transform.position	= defualtPosition;
		target.gameObject.tag		= defualtTag; 

		objectPool.Enqueue(target);
	}

	// 오브젝트 가져오기
	public Rigidbody2D PopObjectInPool()
	{
		if (objectPool.Count >= 1)
		{
			GameObject target = objectPool.Dequeue();

			target.SetActive(true);
			return target.GetComponent<Rigidbody2D>();
		}
		else
		{
			return Instantiate(targetObject, Vector2.zero, Quaternion.identity, transform).GetComponent<Rigidbody2D>();
		}
	}

	// 오브젝트 풀에 넣기
	public void PushObjectInPool(Rigidbody2D target)
	{
		// 초기화
		ResetTarget(target.gameObject);
	}
}
