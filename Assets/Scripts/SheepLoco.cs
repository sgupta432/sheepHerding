using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepLoco : MonoBehaviour 
{
	public float walkSpeed = 0.01f;
	public float turnSpeed = 0.01f;
	Animator anim;

	Rigidbody rb;

	
	void Awake()
	{
		anim = GetComponent<Animator> ();
		rb = GetComponent<Rigidbody> ();

	}
	void FixedUpdate () 
	{
		float h = Input.GetAxis ("Horizontal1");
		float v = Input.GetAxis ("Vertical1");
		anim.SetFloat ("Speed", h);
		anim.SetFloat ("Direction", v);
		if (v > 0)
		Debug.Log ("V: " + v);
		transform.Translate (Vector3.forward * h*walkSpeed * Time.deltaTime);
		transform.Rotate (Vector3.up, v *Time.deltaTime);
	}
}
