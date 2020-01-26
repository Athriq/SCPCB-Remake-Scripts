using UnityEngine;
using System.Collections;

public class SCP096 : MonoBehaviour {

	public Transform player;
	public AudioClip screaming;
	public AudioClip triggered;
	new AudioSource audio;
	NavMeshAgent agent;
	public bool chasing;

	void Start(){
		audio = GetComponent<AudioSource> ();
		agent = GetComponent<NavMeshAgent> ();
	}

	public void scream(){
		audio.clip = screaming;
		audio.Play ();
		chasing = true;
	}

	void Update(){
		if (chasing) {
			agent.destination = player.position;
		}
	}
}
