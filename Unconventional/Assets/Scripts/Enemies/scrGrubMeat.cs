using UnityEngine;
using System.Collections;

public class scrGrubMeat : scrResource
{
	Transform splat;

	void Awake()
	{
		resource = Random.Range (5, 10);
		transform.localScale = resource * 0.1f * Vector3.one;
	}
}
