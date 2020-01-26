using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

public class SCP173 : MonoBehaviour {
	public bool isInIntro;
	public Transform Player;
	public GameObject camera;
	public Transform testSubject1;
	public Transform testSubject2;
	public Transform curTarget;
	public AudioClip[] neckSnaps;
	public AudioClip[] rattles;
	public new AudioSource audio;
	public Vector3 target;

	void Start(){
		audio = GetComponent<AudioSource> ();
		curTarget = null;
	}

	void Update(){
		target = new Vector3(curTarget.position.x, this.transform.position.y, curTarget.position.z);
	}

	public void killSubject1(){
		transform.position = testSubject1.localPosition;
		int random = Random.Range(0,neckSnaps.Length);
		audio.PlayOneShot(neckSnaps[Random.Range(0,neckSnaps.Length)]);
		Destroy(testSubject1.GetComponent<ClassD>());
		curTarget = Player;
		transform.LookAt(target);
	}

	public void killSubject2(){
		transform.position = testSubject2.localPosition;
		int random = Random.Range(0,neckSnaps.Length);
		audio.PlayOneShot(neckSnaps[Random.Range(0,neckSnaps.Length)]);
		audio.Play ();
		curTarget = Player;
		Destroy(testSubject2.GetComponent<ClassD>());
		transform.LookAt (target);
	}

	void OnBecameInvisible(){
		if (!isInIntro) {
			print ("wathcout!");
			int random = Random.Range (0, rattles.Length);
			audio.PlayOneShot (rattles [Random.Range (0, rattles.Length)]);
			curTarget = Player;
			transform.LookAt (target);
		}
	}

	void OnTriggerEnter(Collider col){
		if (col.tag == "Player") {
			camera.GetComponent<MotionBlur> ().enabled = true;
		}
	}

	void OnTriggerExit(Collider col){
		if (col.tag == "Player") {
			camera.GetComponent<MotionBlur> ().enabled = false;
		}
	}
}
