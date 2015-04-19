using UnityEngine;
using System.Collections;

public class scrCameraController : MonoBehaviour
{
	public enum MovementMode
	{
		Orbit,
		Cutscene,
		Free,
	}
	public MovementMode Mode;

	// Orbit camera.
	public GameObject OrbitFocus;
	private float orbitYaw = 0.0f;
	private float orbitSpeed = 4.0f;
	private float orbitDistance = 4.5f;


	Vector3 targetPosition;
	Quaternion targetRotation;

	void Start ()
	{
	
	}

	void Update ()
	{
		if (Mode == MovementMode.Orbit)
		{
			MoveOrbit();
		}
	}

	void MoveOrbit()
	{
		orbitYaw += Input.GetAxis("Rotate Camera") * orbitSpeed;
		if (orbitYaw >= 360)
			orbitYaw -= 360;
		else if (orbitYaw < 0)
			orbitYaw += 360;

		// Orbit position.
		targetPosition = OrbitFocus.transform.position + new Vector3(orbitDistance * Mathf.Sin (orbitYaw * Mathf.Deg2Rad), 2.7f, orbitDistance * Mathf.Cos(orbitYaw * Mathf.Deg2Rad));
		transform.position = targetPosition;

		// Rotate to face centre.
		//targetRotation.SetLookRotation(new Vector3(OrbitFocus.transform.position.x - transform.position.x, 0.0f, OrbitFocus.transform.position.z - transform.position.z));
		targetRotation.eulerAngles = new Vector3(15.0f, orbitYaw + 180, 0.0f);
		transform.rotation = targetRotation;

	}

	void MoveCutscene()
	{
		//todo:
	}

	void MoveFree()
	{
		//todo:
	}
}
