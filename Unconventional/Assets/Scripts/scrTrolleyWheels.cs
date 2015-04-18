using UnityEngine;
using System.Collections;

public class scrTrolleyWheels : MonoBehaviour
{
	public GameObject[] Wheels;

	void Update ()
	{
		foreach (GameObject wheel in Wheels)
			wheel.transform.rotation = Quaternion.RotateTowards(wheel.transform.rotation, Quaternion.Euler(hingeJoint.axis * hingeJoint.motor.targetVelocity), 180 * Time.deltaTime);
	}
}
