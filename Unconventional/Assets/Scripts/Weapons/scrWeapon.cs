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
}
