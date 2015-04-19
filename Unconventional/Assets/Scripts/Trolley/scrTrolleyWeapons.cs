using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class scrTrolleyWeapons : MonoBehaviour
{
	[System.Serializable]
	public class HardPoint
	{
		public GameObject Anchor;
		public scrWeapon attachment { get; private set; }

		public HardPoint()
		{
			Anchor = null;
			attachment = null;
		}

		/// <summary>
		/// Attaches the attachment and resets its position and rotation.
		/// </summary>
		/// <param name="attachment">Attachment.</param>
		public void Attach(scrWeapon attachment)
		{
			// Detach existing weapon.
			if (this.attachment != null)
				Detach ();

			this.attachment = attachment;
			attachment.transform.parent = Anchor.transform;
			attachment.transform.localPosition = new Vector3(0.02f, 0.02f, 0.02f);
			attachment.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, Random.Range (-15.0f, 15.0f));
			attachment.rigidbody.isKinematic = true;
			attachment.collider.isTrigger = true;

			attachment.gameObject.layer = Anchor.layer;
			foreach (Transform t in attachment.gameObject.GetComponentsInChildren<Transform>())
				t.gameObject.layer = Anchor.layer;
		}

		public void Detach()
		{
			Debug.Log ("attachment");
			if (attachment != null)
			{
				attachment.transform.parent = null;
				attachment.rigidbody.isKinematic = false;
				attachment.collider.isTrigger = false;
				attachment.rigidbody.AddForce(Anchor.transform.forward * 3, ForceMode.Impulse);

				LayerMask layer = LayerMask.NameToLayer("Weapon");
				attachment.gameObject.layer = layer;
				foreach (Transform t in attachment.gameObject.GetComponentsInChildren<Transform>())
					t.gameObject.layer = layer;
			}
		}

		public void Use()
		{
			if (attachment != null)
			{
				attachment.Use();
			}
		}
	}

	
	public HardPoint[] FrontHardPoints;
	private int[] frontHardPointIndexes;
	public int frontHardPointsUsed { get; private set; }

	public HardPoint[] LeftHardPoints;
	private int[] leftHardPointIndexes;
	public int leftHardPointsUsed { get; private set; }

	public HardPoint[] RightHardPoints;
	private int[] rightHardPointIndexes;
	public int rightHardPointsUsed { get; private set; }

	void Awake ()
	{
		frontHardPointsUsed = 0;
		frontHardPointIndexes = new int[FrontHardPoints.Length];
		for (int i = 0; i < frontHardPointIndexes.Length; ++i)
			frontHardPointIndexes[i] = i;

		leftHardPointsUsed = 0;
		leftHardPointIndexes = new int[LeftHardPoints.Length];
		for (int i = 0; i < leftHardPointIndexes.Length; ++i)
			leftHardPointIndexes[i] = i;

		rightHardPointsUsed = 0;
		rightHardPointIndexes = new int[RightHardPoints.Length];
		for (int i = 0; i < rightHardPointIndexes.Length; ++i)
			rightHardPointIndexes[i] = i;
	}

	void Update ()
	{
		if (Input.GetAxis("Triggers") < -0.1f)
		{
			foreach (HardPoint hp in FrontHardPoints)
				hp.Use();

			foreach (HardPoint hp in LeftHardPoints)
				hp.Use();

			foreach (HardPoint hp in RightHardPoints)
				hp.Use();
		}
	}

	// Should probably replace these with a single function.

	public void EquipFront(scrWeapon attachment)
	{
		if (frontHardPointsUsed == frontHardPointIndexes.Length)
		{
			// Replace a random hardpoint's attachment.
			FrontHardPoints[Random.Range(0, frontHardPointIndexes.Length)].Attach(attachment);
		}
		else
		{
			// Get a random index that hasnt been used.
			int index = Random.Range (frontHardPointsUsed, frontHardPointIndexes.Length);

			// Get the hardpoint.
			HardPoint hp = FrontHardPoints[frontHardPointIndexes[index]];
			hp.Attach(attachment);

			// Swap the index with the first available one.
			int temp = frontHardPointIndexes[index];
			frontHardPointIndexes[index] = frontHardPointIndexes[frontHardPointsUsed];
			frontHardPointIndexes[frontHardPointsUsed] = temp;

			++frontHardPointsUsed;
		}
	}

	public void EquipLeft(scrWeapon attachment)
	{
		if (leftHardPointsUsed == leftHardPointIndexes.Length)
		{
			// Replace a random hardpoint's attachment.
			LeftHardPoints[Random.Range(0, leftHardPointIndexes.Length)].Attach(attachment);
		}
		else
		{
			// Get a random index that hasnt been used.
			int index = Random.Range (leftHardPointsUsed, leftHardPointIndexes.Length);
			
			// Get the hardpoint.
			HardPoint hp = LeftHardPoints[leftHardPointIndexes[index]];
			hp.Attach(attachment);
			
			// Swap the index with the first available one.
			int temp = leftHardPointIndexes[index];
			leftHardPointIndexes[index] = leftHardPointIndexes[leftHardPointsUsed];
			leftHardPointIndexes[leftHardPointsUsed] = temp;
			
			++leftHardPointsUsed;
		}
	}

	public void EquipRight(scrWeapon attachment)
	{
		if (rightHardPointsUsed == rightHardPointIndexes.Length)
		{
			// Replace a random hardpoint's attachment.
			RightHardPoints[Random.Range(0, rightHardPointIndexes.Length)].Attach(attachment);
		}
		else
		{
			// Get a random index that hasnt been used.
			int index = Random.Range (rightHardPointsUsed, rightHardPointIndexes.Length);
			
			// Get the hardpoint.
			HardPoint hp = RightHardPoints[rightHardPointIndexes[index]];
			hp.Attach(attachment);
			
			// Swap the index with the first available one.
			int temp = rightHardPointIndexes[index];
			rightHardPointIndexes[index] = rightHardPointIndexes[rightHardPointsUsed];
			rightHardPointIndexes[rightHardPointsUsed] = temp;
			
			++rightHardPointsUsed;
		}
	}
}
