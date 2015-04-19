using UnityEngine;
using System.Collections;

// Singleton game manager.
public class scrGameManager : MonoBehaviour
{
	public static scrGameManager Instance { get; private set; }

	public GameObject Player;
	public GameObject Trolley;
	public GameObject Tub;

	// Tree of gamestates?

	void Awake()
	{
		Instance = this;
	}

	void Update()
	{

	}
}
