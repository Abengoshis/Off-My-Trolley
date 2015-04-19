using UnityEngine;
using System.Collections;

public class scrGrub : scrEnemy
{
	float size;
	GameObject target = null;

	void Awake()
	{
		aggroRadius = 30.0f;
		deaggroRadius = 100.0f;

		size = Random.Range (0.5f, 1.0f);
		transform.localScale = size * Vector3.one;
		moveSpeed = size * 0.2f;
		turnSpeed = size * 2;
	}

	void Update()
	{
		if (Vector3.Distance(scrGameManager.Instance.Tub.transform.position, transform.position) < aggroRadius)
		{
			target = scrGameManager.Instance.Tub;
		}
		else if (Vector3.Distance(scrGameManager.Instance.Player.transform.position, transform.position) < aggroRadius)
		{
			target = scrGameManager.Instance.Player;
		}
		else
		{
			if (target != null && Vector3.Distance(target.transform.position, transform.position) > deaggroRadius)
			{
				target = null;
			}
		}

		if (target != null)
		{
			targetDirection = new Vector3(target.transform.position.x, 0, target.transform.position.z) -
							  new Vector3(transform.position.x, 0, transform.position.z);
			targetDirection.Normalize ();
		}
	}

	void FixedUpdate()
	{
		if (target != null)
		{
			rigidbody.MoveRotation(Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(targetDirection), turnSpeed * Time.fixedDeltaTime));
			rigidbody.velocity = transform.forward * moveSpeed;
		}

	}

}
