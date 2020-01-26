using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Title : MonoBehaviour {

	private Item item;
	private string data;
	private GameObject title;
	public List<GameObject> slots = new List<GameObject>();

	void Start(){
		title = GameObject.Find ("title");
		title.SetActive (false);
	}

	void Update(){
		if (title.activeSelf) {
			title.transform.position = Input.mousePosition;
		}
	}

	public void Activate(Item item){
		this.item = item;
		ConstructDataString();
		title.SetActive (true);
	}

	public void Deactivate(){
		title.SetActive (false);
	}

	public void ConstructDataString(){
		data = item.Title;
		title.GetComponent<Text> ().text = data;
	}
}
