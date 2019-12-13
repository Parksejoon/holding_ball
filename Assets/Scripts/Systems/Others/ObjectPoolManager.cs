using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct ObjectData
{
	public GameObject prefab;
	public Transform parent;
}

public class ObjectPoolManager : MonoBehaviour
{
	// 오브젝트 풀
	public static Dictionary<string, Stack<GameObject>> objectPools;		// = new Dictionary<string, Stack<GameObject>>();

	// 오브젝트 리스트
	private static Dictionary<string, ObjectData> objectList;				// = new Dictionary<string, ObjectData>();

	// 수치
	public static int extraCapacity = 50;

	
	// map 초기화
	public static void Init()
	{
		objectPools = new Dictionary<string, Stack<GameObject>>();
		objectList = new Dictionary<string, ObjectData>();
	}

	// 오브젝트 등록
	public static void AddObject(string name, GameObject prefab, Transform parent)
	{
		if (objectList.ContainsKey(name))
		{
			Debug.Log("ASD");
			return;
		}

		ObjectData data = new ObjectData();

		data.prefab = prefab;
		data.parent = parent;

		objectList.Add(name, data);
	}

	// 오브젝트 생성
	public static void Create(string name, int size)
	{
		GameObject prefab = objectList[name].prefab;

		if (objectPools.ContainsKey(name))
		{
			for (int i = 0; i < size; i++)
			{
				GameObject gameObj = Instantiate(prefab, Vector3.zero, Quaternion.identity, objectList[name].parent) as GameObject;

				gameObj.SetActive(false);

				objectPools[name].Push(gameObj);
			}
		}
		else
		{
			Stack<GameObject> objects;

			objects = new Stack<GameObject>(size);

			for (int i = 0; i < size; i++)
			{
				GameObject gameObj = Instantiate(prefab, Vector3.zero, Quaternion.identity, objectList[name].parent) as GameObject;

				gameObj.SetActive(false);

				objects.Push(gameObj);
			}

			objectPools.Add(name, objects);
		}
	}

	// 오브젝트 가져오기 (기본)
	public static GameObject GetGameObject(string name)
	{
		Stack<GameObject> objects = objectPools[name];


		if (objects.Count <= 0)
		{
			Create(name, extraCapacity);
		}

		GameObject gameObj = objects.Pop();

		gameObj.SetActive(true);


		return gameObj;
	}

	// 오브젝트 가져오기 (위치)
	public static GameObject GetGameObject(string name, Vector2 position)
	{
		Stack<GameObject> objects = objectPools[name];


		if (objects.Count <= 0)
		{
			Create(name, extraCapacity);
		}

		GameObject gameObj = objects.Pop();

		gameObj.transform.position = position;
		gameObj.SetActive(true);

		return gameObj;
	}

	// 오브젝트 가져오기 (위치, 회전값)
	public static GameObject GetGameObject(string name, Vector2 position, Quaternion rotation)
	{
		Stack<GameObject> objects = objectPools[name];
		

		if (objects.Count <= 0)
		{
			Create(name, extraCapacity);
		}

		GameObject gameObj = objects.Pop();

		gameObj.transform.position = position;
		gameObj.transform.rotation = rotation;
		gameObj.SetActive(true);

		return gameObj;
	}

	// 오브젝트 해제
	public static void Release(string name, GameObject gameObj)
	{
#if DEBUG
		//Debug.Log(objectPools[name].Count);
#endif

		gameObj.SetActive(false);

		objectPools[name].Push(gameObj);
	}
}

