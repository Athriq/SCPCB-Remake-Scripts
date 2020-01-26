using UnityEngine;
using System.Collections;

public class DocumentRead : MonoBehaviour {

	void Update(){
		if (Input.GetMouseButtonDown (0)) {
			Destroy (transform.gameObject);
		}
	}
}
