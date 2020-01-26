using UnityEngine;
using System.Collections;

public class ClassD : MonoBehaviour {

	public Transform goal;
	public Transform approach173;
	public Transform SCP173;
	public AudioClip speaking;
	public NavMeshAgent agent;

	// Use this for initialization
	void Start () {
		agent = GetComponent<NavMeshAgent>();
	}

	public void go() {
		GetComponent<Animator> ().SetBool ("walk", true);
		agent.destination = goal.position;
	}

	public void moveYourLegs(){
		GetComponent<Animator> ().SetBool ("walk", true);
	}

	void Update(){
		if (!agent.pathPending) {
			if (agent.remainingDistance <= agent.stoppingDistance) {
				if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f) {
					GetComponent<Animator> ().SetBool ("walk", false);
					RotateTowards (SCP173);
				}
			}
		}
	}

	private void RotateTowards (Transform target) {
		Vector3 direction = (target.position - transform.position).normalized;
		Quaternion lookRotation = Quaternion.LookRotation(direction);
		transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 2);
	}
}
