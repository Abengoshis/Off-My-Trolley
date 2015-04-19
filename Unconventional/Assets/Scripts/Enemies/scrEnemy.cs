using UnityEngine;
using System.Collections;

public abstract class scrEnemy : MonoBehaviour
{
	public AudioClip HurtSound;
	public AudioClip DeathSound;

	public GameObject ResourcePrefab;
	protected float resourceExplodeForceMin = 3.0f;
	protected float resourceExplodeForceMax = 6.0f;

	protected float aggroRadius;
	protected float deaggroRadius;

	public float health { get; private set; }
	protected Vector3 targetDirection;
	protected float moveSpeed;
	protected float turnSpeed;

	public void Damage(float amount, Vector3 position)
	{
		if (amount > 0)
		{
			GameObject resource = (GameObject)Instantiate(ResourcePrefab, position, Random.rotation);
			resource.rigidbody.AddExplosionForce(Random.Range (resourceExplodeForceMin, resourceExplodeForceMax), transform.position, transform.localScale.x, 0.0f, ForceMode.Impulse);

			health -= amount;
			if (health <= 0)
				Die();
		}
	}

	void Die()
	{

	}
}
