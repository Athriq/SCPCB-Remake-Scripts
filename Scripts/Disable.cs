using UnityEngine;
using System.Collections;

public class Disable : MonoBehaviour {

	public void disableNotif(){
		GetComponent<Animator> ().SetBool ("play", false);
	}

	public void disableStayBlink(){
		GetComponent<Animator>().SetBool("stayBlink", false);
	}
}
