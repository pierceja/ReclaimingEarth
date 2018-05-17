using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class follwoing:MonoBehaviour{
	private Animator animator;
	public GameObject Player;
	public GameObject Bullet;
	private UnityEngine.AI.NavMeshAgent agent;
	private float moveSpeed = 0.02f; //move speed
	private float rotationSpeed = 3f; //speed of turning
	private int health = 300;


	private void Start(){
			if (agent == null) {
				agent = this.GetComponent<UnityEngine.AI.NavMeshAgent> ();
				agent.enabled = true;
			}
			animator = GetComponent<Animator>();
			animator.SetBool ("When hit", false);
			animator.SetBool ("Killed Player", false);
			animator.SetBool ("FoundPlayer", false);
			animator.SetBool ("STOP", false);
	}

	private void Update(){
		if (this.health <= 0) {
			Destroy (this);
		}
		//agent.SetDestination(Player.transform.position);
		transform.rotation = Quaternion.Slerp(transform.rotation,
			Quaternion.LookRotation(Player.transform.position - transform.position), rotationSpeed*Time.deltaTime);
		//move towards the player
		transform.position += transform.forward * moveSpeed * Time.deltaTime;
		if (Collision.Equals (Player, this)) {
			this.health -= 50;
		}
		if (Collision.Equals (Bullet, this)) {
			this.health -= 30;
		}
	}
}
	