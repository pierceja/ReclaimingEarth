using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunsRandomly : MonoBehaviour {
	public float speedx = 1f; 
	public float speedz = 1f;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		Vector3 pos = transform.position;
		pos.x += speedx * Time.deltaTime * 1000;
		pos.z += speedz * Time.deltaTime * 1000;
		transform.position = pos;
		if (Mathf.Abs(pos.x) > 5) {
			speedx = -speedx; 
		}
		if (Mathf.Abs (pos.z) > 5) {
			speedz = -speedz;
		}
	}

}
