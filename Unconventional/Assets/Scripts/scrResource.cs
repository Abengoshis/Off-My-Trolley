using UnityEngine;
using System.Collections;

public class scrResource : MonoBehaviour
{
	const float ARC_HEIGHT = 1.0f;

	bool canCollect = false;
	bool collecting = false;
	float collectDelay = 1.0f;
	float collectTimer = 0.0f;
	Vector3 startPosition;
	Vector3 startScale;

	protected int resource;

	protected virtual void Update()
	{
		// Curve into the trolley if being collected.
		if (collecting)
		{
			// Smoothly arc the resource into the trolley.
			float smooth = Mathf.SmoothStep(0.0f, 1.0f, collectTimer / collectDelay);
			Vector3 trolleyPosition = scrGameManager.Instance.Trolley.transform.Find("Trolley").position;
			Vector3 intermediate = Vector3.Lerp (startPosition, trolleyPosition, smooth);
			intermediate.y = ARC_HEIGHT * Mathf.Sin (smooth * Mathf.PI) + Mathf.Lerp (startPosition.y, trolleyPosition.y, smooth);
			transform.position = intermediate;
			transform.localScale = Vector3.Lerp (startScale, Vector3.zero, smooth);

			// If collected, destroy this resource.
			if (collectTimer >= collectDelay)
			{
				scrGameManager.Instance.Trolley.GetComponent<scrTrolleyResources>().Gather(resource);
				Destroy (gameObject);
			}
			else
			{
				collectTimer += Time.deltaTime;
			}
		}
	}

	void OnCollisionEnter(Collision collision)
	{
		// Allow collection after one bounce.
		if (!canCollect)
		{
			if (collision.gameObject.name == "Ground")
			{
				canCollect = true;
			}
		}
	}

	void OnTriggerStay(Collider other)
	{
		if (canCollect)
		{
			if (!collecting)
			{
				if (other.gameObject.name == "InteractionBubble")
				{
					collecting = true;
					startPosition = transform.position;
					startScale = transform.localScale;
					rigidbody.isKinematic = true;
					collider.enabled = false;
				}
			}
		}
	}
}
