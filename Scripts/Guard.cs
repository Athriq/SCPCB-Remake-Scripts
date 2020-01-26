using UnityEngine;
using System.Collections;

public class Guard : MonoBehaviour {

	public Renderer muzzleFlash;
	public Light muzzleLight;
	public AudioClip gunshot;
	public int Ammo = 0;
	new AudioSource audio;
	bool muzzle = false;

	void Start(){
		muzzleFlash.enabled = false;
		muzzleLight.enabled = false;
		audio = GetComponent<AudioSource> ();
	}

	public IEnumerator shooting(){
		while (Ammo < 14) {
			audio.PlayOneShot (gunshot);
			Ammo++;
			TurnOnMuzzle ();
			yield return new WaitForSeconds (0.14f);
		}
	}

	void TurnOnMuzzle(){
		muzzleFlash.enabled = true;
		muzzleFlash.transform.Rotate (0, 0, Random.Range (0, 90));
		muzzleLight.enabled = true;
		muzzle = true;
	}

	void TurnOffMuzzle(){
		if (muzzle) {
			muzzleFlash.enabled = false;
			muzzleLight.enabled = false;
			muzzle = false;
		}
	}

	void Update(){
		TurnOffMuzzle ();
		if (Ammo > 14) {
			StopCoroutine (shooting ());
		}
	}
}
