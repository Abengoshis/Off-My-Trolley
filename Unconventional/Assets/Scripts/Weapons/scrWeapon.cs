using UnityEngine;
using System.Collections;

// Base class for all weapons.
public class scrWeapon : MonoBehaviour
{
	public float ContactDamage;	// Damage to do on contact with a thing.

	public virtual void Use()
	{
		Debug.Log ("Weapon [" + name + "] used.");
	}

	// The weapon can only damage enemies when it is a trigger (which happens when it is attached to the trolley).
	void OnTriggerEnter(Collider other)
	{
		if (ContactDamage != 0 && other.transform.root.GetComponent<scrEnemy>() != null)
		{
			scrEnemy enemy = other.transform.root.GetComponent<scrEnemy>();

			enemy.Damage(ContactDamage, other.ClosestPointOnBounds(transform.position));
		}
	}
}
