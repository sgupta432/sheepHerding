using UnityEngine;
using System.Collections;

public class Herd : MonoBehaviour
{
	public float minVelocity = 1;
	public float maxVelocity = 5;
	public float randomness = 1;
	public int flockSize = 20;
	public GameObject prefab;
	public GameObject chasee;

	public Vector3 flockCenter;
	public Vector3 flockVelocity;

	private GameObject[] sheep;
	private Collider collid;
	void Awake()
	{
		collid = GetComponent<Collider> ();
	}
	void Start()
	{
		sheep = new GameObject[flockSize];
		for (var i=0; i<flockSize; i++)
		{
			Vector3 position = new Vector3 (
				Random.value * collid.bounds.size.x,
				0.0f,
				Random.value * collid.bounds.size.z
			) - collid.bounds.extents;

			GameObject sh = Instantiate(prefab, transform.position, transform.rotation) as GameObject;
			sh.transform.parent = transform;
			sh.transform.localPosition = Vector3.zero;
			sh.GetComponent<Sheep>().SetController (gameObject);
			sheep[i] = sh;
		}
	}

	void Update ()
	{
		Vector3 theCenter = Vector3.zero;
		Vector3 theVelocity = Vector3.zero;

		foreach (GameObject s in sheep)
		{
			theCenter = theCenter + s.transform.localPosition;
			theVelocity = theVelocity + s.GetComponent<Rigidbody>().velocity;
		}

		flockCenter = theCenter/(flockSize);
		flockVelocity = theVelocity/(flockSize);
	}
}