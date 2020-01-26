using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class console : MonoBehaviour {

	public InputField consoleInput;
	public string command;
	public GameObject sequences;
	public IntroSequence sequencescript;
	public bool isConsole;
	public bool cursorlock;

	void Start(){
		consoleInput.gameObject.SetActive (false);
		cursorlock = false;
	}

	void Update(){
		command = consoleInput.text;

		if (Input.GetKeyDown (KeyCode.Escape)) {
			cursorlock = !cursorlock;
		}
		if (Input.GetKeyDown (KeyCode.Tab)) {
			isConsole = !isConsole;
		}

		if (cursorlock) {
			Cursor.lockState = CursorLockMode.Locked;
			print ("lol");
		}
		if(!cursorlock){
			Cursor.lockState = CursorLockMode.None;
			print ("lolo");
		}

		if (isConsole) {
			consoleInput.gameObject.SetActive (true);
			GetComponent<FlyCam> ().enabled = false;
		} else {
			consoleInput.gameObject.SetActive (false);
			GetComponent<FlyCam> ().enabled = true;
		}

		if (Input.GetKeyDown(KeyCode.Return)) {
			isConsole = false;
			if (command == "play scene 1") {
				sequencescript.scp173chamber ();
			}
		}
	}
}
