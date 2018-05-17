using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heavy_Control : MonoBehaviour {
	public Animator animator;
	public GameObject Player;
	public Transform player;
	public GameObject Bullets;
	private Vector3 velocity = Vector3.one;
	float moveSpeed = 3; //move speed
	float rotationSpeed = 3; //speed of turning

	private int health;


	// Use this for initialization
	void Start () {
		health = 300;
		player = Player.transform;
		animator = GetComponent<Animator>();
		animator.SetBool ("When hit", false);
		animator.SetBool ("Killed Player", false);
		animator.SetBool ("FoundPlayer", false);
		animator.SetBool ("STOP", false);
	}
	void takeDMG(){
		if (Collision.Equals (player, this)) {
			this.health -= 50;
		}
		if (Collision.Equals (Bullets, this)) {
			this.health -= 20;
		}
	}

	// Update is called once per frame
	void Update () {
		if (this.health <= 0) {
			Destroy (this);
		}
		transform.rotation = Quaternion.Slerp(transform.rotation,
			Quaternion.LookRotation(Player.transform.position - transform.position), rotationSpeed*Time.deltaTime);


		//move towards the player
		transform.position += transform.forward * moveSpeed * Time.deltaTime;

		}
	}
