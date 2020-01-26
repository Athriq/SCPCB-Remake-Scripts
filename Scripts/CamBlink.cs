using UnityEngine;
using System.Collections;

public class CamBlink : MonoBehaviour {

	public void DeactivateCam(){
		Camera.main.enabled = false;
	}

	public void ActivateCam(){
		Camera.main.enabled = true;
	}
}
