using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class scrForestGenerator : MonoBehaviour
{
	public float Radius;
	public float Sparseness;

	public GameObject[] TreePrefabs;
	public int Trees;

	public GameObject GrubPrefab;
	public int Grubs;

	void Awake()
	{
		ParticleSystem[] ps = GetComponentsInChildren<ParticleSystem>();
		foreach (ParticleSystem p in ps) 
		{
			p.emissionRate *= Radius / 10.0f;
			p.transform.localScale = Vector3.one * Radius;
		}

		StartCoroutine(Build());
	}

	IEnumerator Build()
	{
		// Get evenly spaced positions.
		List<Vector2> positions = new List<Vector2>();
		for (float i = -Radius; i <= Radius; i += Sparseness)
		{
			for (float j = -Radius; j <= Radius; j += Sparseness)
			{
				Vector2 position = new Vector2(i, j);
				if (position.magnitude <= Radius)
				{
					positions.Add(new Vector2(transform.position.x, transform.position.z) + position);
				}
			}
		}
		
		for (int i = 0; i < Trees; ++i)
		{
			int positionIndex = Random.Range (0, positions.Count);
			Vector3 position = new Vector3(positions[positionIndex].x, Random.Range (-0.3f, 0.0f), positions[positionIndex].y);
			Quaternion rotation = Quaternion.Euler(new Vector3(Random.Range (-10.0f, 10.0f), Random.Range (0.0f, 360.0f), Random.Range (-10.0f, 10.0f)));
			GameObject tree = (GameObject)Instantiate(TreePrefabs[Random.Range (0, TreePrefabs.Length)], position, rotation);
			tree.transform.localScale = Vector3.one * Random.Range (2.0f, 10.0f);
			tree.transform.parent = transform;
			positions.RemoveAt (positionIndex);

			yield return new WaitForFixedUpdate();
		}

		for (int i = 0; i < Grubs; ++i)
		{
			int positionIndex = Random.Range (0, positions.Count);
			Vector3 position = new Vector3(positions[positionIndex].x, 0.0f, positions[positionIndex].y);
			Quaternion rotation = Quaternion.Euler(new Vector3(0, Random.Range (0.0f, 360.0f), 0));
			Instantiate(GrubPrefab, position, rotation);
			positions.RemoveAt (positionIndex);

			yield return new WaitForFixedUpdate();
		}
	}
}
