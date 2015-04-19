using UnityEngine;
using System.Collections;

public class scrTrolleyResources : MonoBehaviour
{
	const float RESOURCES_MAX = 100.0f;
	private float resources = 0;

	public void Gather(float amount)
	{
		resources += amount;
		if (resources > RESOURCES_MAX)
			resources = RESOURCES_MAX;


	}

}
