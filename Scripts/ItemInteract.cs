using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class ItemInteract : MonoBehaviour {
	private Interactable intrctble;
	public Inventory inventory;
	public Title title;
	public Canvas canvas;
	public GameObject inv;
	public Item docORI;
	public GameObject events;
	public GameObject grabIcon;
	public bool inInteractRange;
	public AudioClip paperGrab;
	bool grabbing;
	public bool inventoryOpened;
	GameObject curItem;
	public FirstPersonController fpc;
	private Item item;
	new AudioSource audio;
	Camera camera;
	private Vector2 uiOffset;
	RectTransform CanvasRect;

	void Start(){
		CanvasRect = canvas.GetComponent<RectTransform> ();
		audio = GetComponent<AudioSource> ();
		camera = Camera.main;
		inv.SetActive (false);
	}

	void Update(){
		if(Input.GetKeyDown(KeyCode.Tab)){
			inventoryOpened = !inventoryOpened;
		}

		if (inventoryOpened) {
			Activate ();
		} else {
			Deactivate ();
		}

		if (inInteractRange) {
			Vector3 ViewportPosition = camera.WorldToViewportPoint(curItem.transform.position);
			Vector2 WorldObject_ScreenPosition=new Vector2(
				((ViewportPosition.x*CanvasRect.sizeDelta.x)-(CanvasRect.sizeDelta.x*0.5f)),
				((ViewportPosition.y*CanvasRect.sizeDelta.y)-(CanvasRect.sizeDelta.y*0.5f)));

			grabIcon.GetComponent<Image>().rectTransform.anchoredPosition = WorldObject_ScreenPosition;

			if(Input.GetMouseButtonDown(0)){
				if (intrctble.types == Types.Object) {
					grabbing = true;
				}
				if (intrctble.types == Types.Paper) {
					grabbing = true;
					audio.PlayOneShot (paperGrab);
					if (curItem.name == "docORI") {
						events.SendMessage("pickup", SendMessageOptions.DontRequireReceiver);
					}
				}
			}

			if(grabbing){
				curItem.transform.position = Vector3.Lerp(curItem.transform.position, transform.position, 20 * Time.deltaTime);
				StartCoroutine (pickedItem());
			}
		}
	}

	public void Activate(){
		inv.SetActive (true);
		fpc.enabled = false;
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
	}

	public void Deactivate(){
		inv.SetActive (false);
		fpc.enabled = true;
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
		title.Deactivate ();
	}

	IEnumerator pickedItem(){
		yield return new WaitForSeconds (0.1f);
		if (curItem.name == "docORI")
			inventory.AddItem (0);
		
		grabbing = false;
		inInteractRange = false;
		//Destroy (curItem.gameObject);
		grabIcon.SetActive (false);
		Destroy (curItem.gameObject);
	}

	void OnTriggerEnter(Collider col){
		if (col.tag == "interactable") {
			curItem = col.transform.gameObject;
			intrctble = col.gameObject.GetComponent<Interactable> ();
			grabIcon.SetActive (true);
			inInteractRange = true;
		}
	}

	void OnTriggerExit(Collider col){
		if (col.tag == "interactable") {
			curItem = null;
			grabIcon.SetActive (false);
			inInteractRange = false;
		}
	}
}
