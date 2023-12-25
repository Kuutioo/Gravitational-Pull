using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CelestialBody : MonoBehaviour
{
	private new Rigidbody rigidbody;

	private CelestialBody[] celestialBodies;

	private void Awake()
	{
		rigidbody = GetComponent<Rigidbody>();
		celestialBodies = FindObjectsOfType<CelestialBody>();
	}

	private void Start()
	{
		foreach (CelestialBody celestialBody in celestialBodies)
		{
			if (celestialBody != this)
			{
				InitialVelocity(celestialBody);
			}
		}
	}

	private void FixedUpdate()
	{
		foreach (CelestialBody celestialBody in celestialBodies)
		{
			if (celestialBody != this)
				Attract(celestialBody);
		}
	}

	private void Attract(CelestialBody celestialBody)
	{
		Rigidbody rigidbodyToAttract = celestialBody.rigidbody;

		Vector3 direction = rigidbody.position - rigidbodyToAttract.position;
		float distance = direction.sqrMagnitude;

		float forceMagnitude = Universe.gravitationalConstant * (rigidbody.mass * rigidbodyToAttract.mass) / distance;
		Vector3 force = direction.normalized * forceMagnitude;

		rigidbodyToAttract.AddForce(force);
	}

	private void InitialVelocity(CelestialBody celestialBody)
	{
		Rigidbody rigidbodyToAttract = celestialBody.rigidbody;

		Vector3 direction = rigidbody.position - rigidbodyToAttract.position;
		rigidbodyToAttract.transform.LookAt(this.transform);

		rigidbodyToAttract.velocity += rigidbodyToAttract.transform.right * Mathf.Sqrt((Universe.gravitationalConstant * rigidbody.mass) / direction.magnitude);
	}
}
