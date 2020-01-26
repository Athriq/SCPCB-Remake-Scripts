using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class ItemData : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler {
	public Item item;
	public int amount;

	private ItemInteract interact;
	private GameObject canvas;
	private Inventory inv;
	private Title title;

	void Start(){
		interact = GameObject.Find ("pickuprange").GetComponent<ItemInteract> ();
		canvas = GameObject.Find ("MainCanvas");
		inv = GameObject.Find ("Inventory").GetComponent<Inventory> ();
		title = inv.GetComponent<Title> ();
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		title.Activate (item);
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		title.Deactivate ();
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		if (this.item.ID == 0) {
			var obj = Instantiate(Resources.Load<GameObject>("Prefabs/Sprites/docORI"));
			obj.transform.SetParent (canvas.transform, false);
			interact.inventoryOpened = false;
		}
	}
}
