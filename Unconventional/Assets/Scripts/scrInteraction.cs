using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using UnityEngine.UI;

public class scrInteraction : MonoBehaviour
{
	public scrTrolleyWeapons TrolleyWeapons;
	public Text InteractionText;

	private List<Collider> overlappingTriggers = new List<Collider>();

	void Update()
	{
		// Check if there is anything to interact with.
		if (overlappingTriggers.Count == 0)
		{
			InteractionText.text = "";
		}
		else
		{
			for (int i = overlappingTriggers.Count - 1; i >= 0; --i)
				if (overlappingTriggers[i] == null)
					overlappingTriggers.RemoveAt (i);

			// Get the closest trigger.
			Collider closestTrigger = null;
			float closestDistance = float.MaxValue;
			foreach (Collider c in overlappingTriggers)
			{
				float distance = Vector2.Distance(new Vector2(transform.position.x, transform.position.z), new Vector2(c.transform.position.x, c.transform.position.z));
				if (distance < closestDistance || c.gameObject.layer == LayerMask.NameToLayer("Weapon"))
				{
					closestTrigger = c;
					closestDistance = distance;
				}
			}

			if (closestTrigger != null)
			{
				// Set the text to the name.
				InteractionText.text = closestTrigger.transform.root.name;
				InteractionText.transform.position = closestTrigger.transform.root.position + new Vector3(0.0f, 0.6f + 0.1f * Mathf.Sin (Time.time), 0.0f);
				InteractionText.transform.LookAt(Camera.main.transform);

				// Act based on the type of object.
				if (closestTrigger.gameObject.layer == LayerMask.NameToLayer("Weapon"))
				{
					scrWeapon weapon = closestTrigger.transform.root.GetComponent<scrWeapon>();
					
					if (Input.GetButtonDown("Assign Front"))
					{
						TrolleyWeapons.EquipFront(weapon);
					}
					else if (Input.GetButtonDown("Assign Left"))
					{
						TrolleyWeapons.EquipLeft(weapon);
					}
					else if (Input.GetButtonDown("Assign Right"))
					{
						TrolleyWeapons.EquipRight(weapon);
					}
					
				}
			}
		}
	}

	void OnTriggerEnter(Collider other)
	{
		overlappingTriggers.Add (other);
	}

	void OnTriggerExit(Collider other)
	{
		overlappingTriggers.Remove (other);
	}

}
