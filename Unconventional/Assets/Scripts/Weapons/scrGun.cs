using UnityEngine;
using System.Collections;

// Generic hitscan gun.
public class scrGun : scrWeapon
{
	const float MUZZLE_FLASH_DURATION = 0.05f;
	public GameObject MuzzleFlash;
	
	public float FireRate;	// Shots per second.
	private bool firing = false;

	public override void Use ()
	{
		if (!firing)
		{
			StartCoroutine(Fire ());
		}
	}

	IEnumerator Fire()
	{
		// Hitscan.

		// Enable the muzzle flash.
		MuzzleFlash.SetActive(true);
		firing = true;

		// Randomly rotate the flash.
		MuzzleFlash.transform.localEulerAngles = new Vector3(0.0f, 0.0f, Random.Range (0.0f, 360.0f));

		// Randomly scale the flash.
		float scale = Random.Range (0.9f, 1.1f);

		// Set a random flash frame.
		float offset = Random.Range (0, 4) / 4.0f;
		foreach (Renderer r in MuzzleFlash.GetComponentsInChildren<Renderer>())
			r.material.mainTextureOffset = new Vector2(offset, 0.0f);

		float duration = MUZZLE_FLASH_DURATION;
		while (duration > 0)
		{
			duration -= Time.deltaTime;

			MuzzleFlash.transform.localScale = scale * Vector3.one * (0.7f + duration / MUZZLE_FLASH_DURATION * 0.3f);

			yield return new WaitForEndOfFrame();
		}

		// Hide the muzzle flash.
		MuzzleFlash.SetActive(false);

		yield return new WaitForSeconds(1.0f / FireRate - MUZZLE_FLASH_DURATION);

		firing = false;
	}
}
