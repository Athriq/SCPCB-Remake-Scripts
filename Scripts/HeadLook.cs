using UnityEngine;
using System.Collections;

public class HeadLook : MonoBehaviour {

	public HeadLookController headLook;
	
	// Update is called once per frame
	void Update () {
		headLook.target = transform.position;
	}
}
