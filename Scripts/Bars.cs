using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Bars : MonoBehaviour {

	public GameObject barsHolder;
	public Image blinkbar;
	public Image eye;
	public float blinkTimer;
	public float blinkTimerMAX = 1;
	public bool begin = true;
	public bool isBlinking;
	public bool stopDecreasing;
	public bool hide;

	private const float coef = 0.05f;
	private bool holdingSpace = false;

	void Start ()
	{
		blinkTimer = blinkTimerMAX;
	}

	void Update(){
		if (begin == true) {
			StartCoroutine ("Decrease");
			begin = false;
		}

		blinkbar.fillAmount = blinkTimer;

		if (blinkTimer < 0) {
			eye.transform.GetComponent<Animator> ().SetBool ("blink", true);
			isBlinking = true;
			blinkTimer = blinkTimerMAX;
		}

		if (blinkTimer > 0) {
			isBlinking = false;
			transform.GetComponent<Camera> ().enabled = true;
		}

		if (Input.GetKey (KeyCode.Space)) {
			holdingSpace = true;
			eye.transform.GetComponent<Animator> ().SetBool ("blink", true);
			isBlinking = true;
			StartCoroutine (wait (0.417f));
		}

		if (Input.GetKeyUp (KeyCode.Space)) {
			eye.transform.GetComponent<Animator> ().SetBool ("blink", false);
		}

		if (Input.GetKeyDown (KeyCode.T)) {
			hide = !hide;
		}

		if (hide)
			barsHolder.GetComponent<Animator> ().SetBool ("hide", true);

		if(!hide)
			barsHolder.GetComponent<Animator> ().SetBool ("hide", false);
	}

	void LateUpdate(){
		eye.transform.GetComponent<Animator> ().SetBool ("blink", false);
	}

	IEnumerator wait(float time){
		yield return new WaitForSeconds (time);
		isBlinking = false;
		blinkTimer = blinkTimerMAX;
	}

	IEnumerator Decrease (){
		while (blinkTimer > 0) {
			blinkTimer -= 0.05f;
			yield return new WaitForSeconds (1);
		}
	}
}
