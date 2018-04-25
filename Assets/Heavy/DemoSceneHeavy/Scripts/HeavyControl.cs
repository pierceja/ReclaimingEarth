using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyController : MonoBehaviour {
	public Animator animator;
	public float time = 1.0f;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();
	}

	// Update is called once per frame
	void Update () {
		Walk ();
		Roar ();
	}

	public void Idle ()
	{
		animator = GetComponent<Animator>();
		animator.SetBool ("Walk", false);
		animator.SetBool ("SprintJump", false);
		animator.SetBool ("PlayerFound", false);
	}

	public void Walk ()
	{
		animator = GetComponent<Animator>();
		animator.SetBool ("Walk", true);
		animator.SetBool ("SprintJump", false);
		animator.SetBool ("PlayerFound", false);
		animator.SetFloat ("TimePassed", time);

	}

	public void Roar()
	{
		animator = GetComponent<Animator>();
		animator.SetBool ("Walk", false);
		animator.SetBool ("SprintJump", true);
		animator.SetBool ("PlayerFound", false);
		animator.SetFloat ("TimePassed", time);

	}

	public void LightMachineGun()
	{
		animator = GetComponent<Animator>();
		animator.SetFloat ("TimePassed", time);

	}
}
