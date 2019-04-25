using UnityEngine;

public class ZoneCollider : MonoBehaviour
{
	public Transform	ballTransform;
	public	Orbit[]		orbits;

	private int			previousOrbit;


	private void Start()
	{
		SetOrbit(0, true);
		previousOrbit = 0;
	}

	private void Update()
	{
		Vector2 position = ballTransform.position;
		float distance;

		distance = Mathf.Sqrt(((position.x - 0) * (ballTransform.position.x - 0)) + ((ballTransform.position.y - 0) * (ballTransform.position.y - 0)));

#if DEBUG
		//Debug.Log(distance);
#endif
		SetOrbit(previousOrbit, false);

		if (distance < 11)
		{
			previousOrbit = 0;
			SetOrbit(0, true);
		}
		else if (distance < 20)
		{
			previousOrbit = 1;
			SetOrbit(1, true);
		}
		else if (distance < 29)
		{
			previousOrbit = 2;
			SetOrbit(2, true);
		}
		else if (distance < 41)
		{
			previousOrbit = 3;
			SetOrbit(3, true);
		}
		else
		{
			previousOrbit = 4;
			SetOrbit(4, true);
		}
	}

	private void SetOrbit(int num, bool enabled)
	{
		orbits[num].SetCollider(enabled);
	}
}
