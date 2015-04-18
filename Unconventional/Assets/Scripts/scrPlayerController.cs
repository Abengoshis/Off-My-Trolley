using UnityEngine;
using System.Collections;

public class scrPlayerController : MonoBehaviour
{

	private Vector3 targetDirection;
	private float moveSpeed = 5.0f;
	private float rotateSpeed = 1040.0f;

	public GameObject Trolley;

	void Awake ()
	{

	}

	void Update ()
	{


		ProcessInput ();
	}

	void FixedUpdate()
	{
		Move ();
	}

	void ProcessInput()
	{
		targetDirection = Input.GetAxis("Horizontal") * new Vector3(Camera.main.transform.right.x, 0.0f, Camera.main.transform.right.z) + 
						  Input.GetAxis("Vertical") * new Vector3(Camera.main.transform.forward.x, 0.0f, Camera.main.transform.forward.z);
	}

	void Move()
	{
		HingeJoint joint = Trolley.hingeJoint;
		JointMotor motor = Trolley.hingeJoint.motor;

		// Rotate to face the new direction.
		if (targetDirection.magnitude != 0)
		{
			rigidbody.MoveRotation(Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(targetDirection), rotateSpeed * Time.fixedDeltaTime));
			rigidbody.MovePosition(transform.position + targetDirection * moveSpeed * targetDirection.magnitude * Time.fixedDeltaTime);

			// Add influence to trolley.
			motor.force = Vector2.Angle(new Vector2(transform.forward.x, transform.forward.z), new Vector2(Trolley.transform.forward.x, Trolley.transform.forward.z)) * 10;
			motor.targetVelocity = Vector2.Dot(new Vector2(transform.forward.x, transform.forward.z), new Vector2(Trolley.transform.right.x, Trolley.transform.right.z)) * motor.force;
			joint.motor = motor;
		}
		else
		{
			motor.targetVelocity *= 0.95f;
			joint.motor = motor;
		}
	}
}
