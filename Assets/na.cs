using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class na:MonoBehaviour{
	public GameObject Player;
	private UnityEngine.AI.NavMeshAgent agent;


	private void Start(){
		if (agent = null) {
			agent = this.GetComponent<UnityEngine.AI.NavMeshAgent> ();
		}
	}

	private void Update(){
		agent.SetDestination(Player.transform.position);
	}
}