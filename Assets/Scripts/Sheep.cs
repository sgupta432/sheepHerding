using UnityEngine;
using System.Collections;

public class Sheep : MonoBehaviour
{
	private GameObject Controller;
	private bool inited = false;
	private float minVelocity;
	private float maxVelocity;
	private float randomness;
	private GameObject chasee;
	private Rigidbody rb;

	void Awake()
	{
		rb = GetComponent<Rigidbody> ();
	}
	void Start ()
	{
		
		StartCoroutine ("SheepHerding");
	}

	IEnumerator SheepHerding ()
	{
		while (true)
		{
			if (inited)
			{
				rb.velocity = rb.velocity + Calc () * Time.deltaTime;

				// enforce minimum and maximum speeds for the boids
				float speed = rb.velocity.magnitude;
				if (speed > maxVelocity)
				{
					rb.velocity = rb.velocity.normalized * maxVelocity;
				}
				else if (speed < minVelocity)
				{
					rb.velocity = rb.velocity.normalized * minVelocity;
				}
			}

			float waitTime = Random.Range(0.3f, 0.5f);
			yield return new WaitForSeconds (waitTime);
		}
	}

	private Vector3 Calc ()
	{
		Vector3 randomize = new Vector3 ((Random.value *2) -1, (Random.value * 2) -1, (Random.value * 2) -1);

		randomize.Normalize();
		Herd herdController = Controller.GetComponent<Herd>();
		Vector3 flockCenter = herdController.flockCenter;
		Vector3 flockVelocity = herdController.flockVelocity;
		Vector3 follow = chasee.transform.localPosition;

		flockCenter = flockCenter - transform.localPosition;
		flockVelocity = flockVelocity - rb.velocity;
		follow = follow - transform.localPosition;

		return (flockCenter + flockVelocity + follow * 2 + randomize * randomness);
	}

	public void SetController (GameObject theController)
	{
		Controller = theController;
		Herd herdController = Controller.GetComponent<Herd>();
		minVelocity = herdController.minVelocity;
		maxVelocity = herdController.maxVelocity;
		randomness = herdController.randomness;
		chasee = herdController.chasee;
		inited = true;
	}
}