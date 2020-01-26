using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.UI;

public class IntroSequence : MonoBehaviour {
	public bool ExitedCell = false;
	bool pickedUp;
	public Bars bars;
	public ItemInteract inv;
	public Text notif;
	public GameObject _inv;
	public GameObject camHandler;
	public GameObject door;
	public GameObject door173;
	public GameObject cdoor173;
	public GameObject Sequences;
	public GameObject Ulgrin;
	public GameObject UlgrinObject;
	public GameObject flash;
	public GameObject testsubject1;
	public GameObject testsubject2;
	public GameObject SCP173;
	public GameObject balconyGuard;
	public GameObject _lights;
	public GameObject broken173door;
	public GameObject ventSteam;
	public GameObject franklin;
	public AudioSource alarmAmbiance;
	public AudioSource alarmAmbiance_2;
	public AudioClip IntroMusic;
	public AudioClip light1;
	public AudioClip light2;
	public AudioClip doorsound;
	public AudioClip doorsound2;
	public AudioClip doorsound2close;
	public AudioClip scp173bigdooropen;
	public AudioClip scp173bigdoorclose;
	public AudioClip BeforeDoorOpens;
	public AudioClip ExitCell;
	public AudioClip ExitCellRefuse;
	public AudioClip ExitCellRefuse2;
	public AudioClip CellGas;
	public AudioClip Escort;
	public AudioClip announcement;
	public AudioClip on;
	public AudioClip EscortDone;
	public AudioClip enterchamber;
	public AudioClip approachscp;
	public AudioClip seen173;
	public AudioClip problem;
	public AudioClip Dontlikethis;
	public AudioClip WhatJustHappened;
	public AudioClip horror;
	public AudioClip ohsht;
	public AudioClip _173vent;
	public AudioClip alarm;
	public AudioClip alarm2;

	void Start(){
		broken173door.SetActive (false);
		ventSteam.SetActive (false);
		GetComponent<AudioSource> ().PlayOneShot (light1);
		flash.SetActive (true);
		flash.GetComponent<Animator> ().Play ("flash");
		transform.GetComponentInParent<FirstPersonController> ().enabled = false;
		_inv.SetActive(false);
		inv.enabled = false;
		StartCoroutine (wakingup(9.269f));
		StartCoroutine (talkSequence1 (12.5f));
		Sequences.transform.GetChild(1).GetComponent<AudioSource> ().clip = IntroMusic;
		Sequences.transform.GetChild(1).GetComponent<AudioSource> ().Play();
		Cursor.lockState = CursorLockMode.Locked;
	}

	void OnTriggerEnter(Collider col){
		if (col.tag == "exitcell") {
			ExitedCell = true;
			Ulgrin.GetComponent<AudioSource> ().PlayOneShot (Escort);
			col.gameObject.SetActive(false);
			StartCoroutine(escortPlayer(5.3f));
		}
		if (col.tag == "escortdone") {
			Ulgrin.GetComponent<AudioSource> ().PlayOneShot (EscortDone);
			col.gameObject.SetActive (false);
			door173.GetComponent<Animator> ().Play ("open");
			door173.GetComponent<AudioSource> ().PlayOneShot (doorsound2);
		}
		if (col.tag == "PA") {
			Sequences.transform.GetChild (3).GetChild(0).GetComponent<AudioSource> ().PlayOneShot (on);
			Sequences.transform.GetChild (3).GetComponent<AudioSource> ().PlayOneShot (on);
			StartCoroutine (attention (2.268f));
			col.gameObject.SetActive (false);
		}
		if (col.tag == "scp173chamber") {
			scp173chamber ();
			col.gameObject.SetActive (false);
		}
		if (col.tag == "enterchamber") {
			StartCoroutine (entered (2));
			col.gameObject.SetActive (false);
		}
		if (col.tag == "see173") {
			Sequences.transform.GetChild(1).GetComponent<AudioSource> ().Stop();
			Sequences.transform.GetChild(1).GetComponent<AudioSource> ().volume = 8;
			Sequences.transform.GetChild (1).GetComponent<AudioSource> ().PlayOneShot (seen173);
			col.gameObject.SetActive (false);
		}
	}

	public void scp173chamber(){
		cdoor173.GetComponent<AudioSource> ().PlayOneShot (scp173bigdooropen);
		door173.GetComponent<AudioSource>().PlayOneShot(doorsound2close);
		door173.GetComponent<Animator> ().Play ("close");
		cdoor173.GetComponent<Animator> ().Play ("open");
		StartCoroutine (scientist173 (5));
	}

	IEnumerator entered(float time){
		yield return new WaitForSeconds (time);
		cdoor173.GetComponent<Animator> ().Play ("close");
		cdoor173.GetComponent<AudioSource> ().PlayOneShot (scp173bigdoorclose);
		StartCoroutine(playSoundWithDelay(approachscp, 2.983f));
		StartCoroutine (chamberdooropens (7.010f));
	}

	IEnumerator chamberdooropens(float time){
		yield return new WaitForSeconds (time);
		testsubject1.GetComponent<ClassD> ().agent.destination = testsubject1.GetComponent<ClassD> ().approach173.position;
		testsubject1.GetComponent<ClassD> ().moveYourLegs ();
		cdoor173.GetComponent<AudioSource> ().PlayOneShot (scp173bigdooropen);
		cdoor173.GetComponent<Animator> ().Play ("open");
		testsubject2.GetComponent<AudioSource> ().PlayOneShot (Dontlikethis);
		StartCoroutine(playSoundWithDelay(problem, 3.5f));
		StartCoroutine (breach (15.4f));
	}

	IEnumerator playSoundWithDelay(AudioClip clip, float delay)
	{
		yield return new WaitForSeconds(delay);
		Sequences.transform.GetChild(6).GetComponent<AudioSource>().PlayOneShot(clip);
	}

	IEnumerator breach(float time){
		yield return new WaitForSeconds (time);
		bars.eye.transform.GetComponent<Animator> ().SetBool ("stayBlink", true);
		camHandler.GetComponent<CameraShake> ().ShakeCamera (4, 5.5f);
		testsubject1.GetComponent<Animator> ().Play ("dead");
		testsubject2.GetComponent<Animator> ().SetBool ("takecover", true);
		SCP173.GetComponent<SCP173> ().enabled = true;
		SCP173.GetComponent<SCP173> ().killSubject1 ();
		StartCoroutine(waitToKill(1.8f));
	}

	IEnumerator waitToKill(float time){
		yield return new WaitForSeconds (time);
		testsubject2.GetComponent<Animator> ().SetBool ("takecover", false);
		balconyGuard.GetComponent<AudioSource> ().PlayOneShot (WhatJustHappened);
		Sequences.transform.GetChild (1).GetComponent<AudioSource> ().PlayOneShot (horror);
		bars.eye.transform.GetComponent<Animator> ().SetBool ("stayBlink", true);
		testsubject2.GetComponent<Animator> ().Play ("dead");
		SCP173.GetComponent<SCP173> ().killSubject2 ();
		StartCoroutine (shakeCam (2f));
	}

	IEnumerator shakeCam(float time){
		yield return new WaitForSeconds (time);
		print ("shakeCam");
		camHandler.GetComponent<CameraShake> ().ShakeCamera (4, 9f);
		StartCoroutine (balconyGuardShoots (1.5f));
	}

	IEnumerator balconyGuardShoots(float time){
		yield return new WaitForSeconds (time);
		bars.eye.transform.GetComponent<Animator> ().SetBool ("stayBlink", true);
		balconyGuard.GetComponent<AudioSource> ().PlayOneShot (ohsht);
		balconyGuard.transform.position = new Vector3 (0.98f, 2.23f, 7.46f);
		balconyGuard.transform.rotation = Quaternion.Euler(0, 273.8899f, 0);
		balconyGuard.GetComponent<Animator> ().SetBool ("shootingstance", true);
		StartCoroutine (shooting (1.3f));
		SCP173.transform.position = Sequences.transform.GetChild (13).transform.position;
		SCP173.transform.LookAt (balconyGuard.transform.position);
		SCP173.GetComponent<SCP173> ().transform.LookAt (balconyGuard.transform.position);
		StartCoroutine (waitForBlackout (3.5f));
	}

	IEnumerator shooting(float time){
		yield return new WaitForSeconds (time);
		balconyGuard.GetComponent<Guard> ().StartCoroutine (balconyGuard.GetComponent<Guard> ().shooting());
	}

	IEnumerator waitForBlackout(float time){
		yield return new WaitForSeconds (time);
		_lights.SetActive (false);
		balconyGuard.GetComponent<Guard>().StopCoroutine(balconyGuard.GetComponent<Guard>().shooting());
		balconyGuard.GetComponent<Animator> ().Play ("dead");
		SCP173.GetComponent<SCP173>().audio.PlayOneShot(SCP173.GetComponent<SCP173>().neckSnaps[Random.Range(0,SCP173.GetComponent<SCP173>().neckSnaps.Length)]);
		SCP173.GetComponent<Renderer>().enabled = false;
		Sequences.transform.GetChild (6).GetComponent<AudioSource> ().PlayOneShot (light2);
		Sequences.transform.GetChild(14).GetComponent<AudioSource> ().PlayOneShot (_173vent);
		alarmAmbiance.clip = alarm;
		alarmAmbiance.Play();
		alarmAmbiance_2.clip = alarm2;
		alarmAmbiance_2.PlayDelayed(10);
		door173.SetActive (false);
		franklin.SetActive (false);
		//broken173door.SetActive (true);
		//ventSteam.SetActive (true);
		RenderSettings.ambientIntensity = 0.3f;
		RenderSettings.fog = true;
		Ulgrin.SetActive (false);
		bars.eye.transform.GetComponent<Animator> ().SetBool ("stayBlink", true);
		notif.GetComponent<Animator> ().SetBool ("play", true);
		camHandler.GetComponent<CameraShake> ().ShakeCamera (10, 2);
		notif.text = "Press F5 to save";
	}

	IEnumerator scientist173(float time){
		yield return new WaitForSeconds (time);
		Sequences.transform.GetChild (6).GetComponent<AudioSource> ().PlayOneShot (enterchamber);
		StartCoroutine (movetochamber (5.251f));
	}

	IEnumerator movetochamber(float time){
		yield return new WaitForSeconds (time);
		testsubject1.GetComponent<ClassD> ().go ();
		testsubject2.GetComponent<ClassD> ().go ();
	}

	IEnumerator attention(float time){
		yield return new WaitForSeconds (time);
		Sequences.transform.GetChild (3).GetComponent<AudioSource> ().PlayOneShot (announcement);
		Sequences.transform.GetChild (3).GetChild(0).GetComponent<AudioSource> ().PlayOneShot (announcement);
		notif.GetComponent<Animator> ().SetBool ("play", true);
		notif.text = "Press T to hide User Interface";
	}

	IEnumerator escortPlayer(float time){
		yield return new WaitForSeconds (time);
		UlgrinObject.GetComponent<Ulgrin> ().go();
		Ulgrin.GetComponent<Animator> ().SetBool ("walk", true);
	}

	IEnumerator wakingup(float time){
		yield return new WaitForSeconds (time);
		inv.enabled = true;
		transform.GetComponentInParent<Animator> ().enabled = false;
		transform.GetComponentInParent<FirstPersonController> ().enabled = true;
		notif.GetComponent<Animator> ().SetBool ("play", true);
		notif.text = "Pick up the paper on the desk";
		flash.SetActive (false);
	}

	IEnumerator wait(float time){
		yield return new WaitForSeconds (time);
		Ulgrin.transform.GetComponent<Animator> ().Play ("ExitCell");
		door.transform.GetComponent<Animator> ().Play ("CellDoorOpens");
		door.transform.GetComponent<AudioSource> ().PlayOneShot (doorsound);
		Ulgrin.GetComponent<AudioSource> ().clip = ExitCell;
		Ulgrin.GetComponent<AudioSource> ().PlayDelayed(0.5f);
	}

	IEnumerator talkSequence1(float time){
		yield return new WaitForSeconds (time);
		Ulgrin.GetComponent<AudioSource> ().PlayOneShot (BeforeDoorOpens);
		StartCoroutine (wait (11));
	}

	IEnumerator ifDidntExit3(float time){
		yield return new WaitForSeconds (time);
		Ulgrin.GetComponent<AudioSource> ().PlayOneShot (CellGas);
		StartCoroutine(CellDoorCloses(7));
	}

	IEnumerator CellDoorCloses(float time){
		yield return new WaitForSeconds (time);
		door.transform.GetComponent<Animator> ().Play ("CellDoorClose");
		door.transform.GetComponent<AudioSource> ().PlayOneShot (doorsound);
	}

	public void pickup(){
		pickedUp = true;
		notif.GetComponent<Animator> ().SetBool ("play", true);
		notif.text = "Press TAB to open the inventory";
	}
}

