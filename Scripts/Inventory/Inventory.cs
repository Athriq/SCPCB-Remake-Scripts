using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour {
	public GameObject inventoryPanel;
	GameObject slotPanel;
	ItemDatabase database;
	public GameObject inventorySlot;
	public GameObject inventoryItem;

	int slotAmount;
	public List<Item> items = new List<Item>();
	public List<GameObject> slots = new List<GameObject>();

	void Start(){
		database = GetComponent<ItemDatabase> ();

		slotAmount = 10;
		slotPanel = inventoryPanel.transform.FindChild ("SlotPanel").gameObject;
		for (int i = 0; i < slotAmount; i++) {
			items.Add (new Item ());
			slots.Add (Instantiate(inventorySlot));
			slots [i].transform.SetParent (slotPanel.transform, false);
		}
	}

	public void AddItem(int id){
		Item itemToAdd = database.FetchItemByID (id);
		for (int i = 0; i < items.Count; i++)
		{
			if (items [i].ID == -1)
			{
				items [i] = itemToAdd;
				GameObject itemObj = Instantiate(inventoryItem);
				itemObj.GetComponent<ItemData> ().item = itemToAdd;
				itemObj.GetComponent<Image> ().sprite = itemToAdd.Sprite;
				itemObj.transform.SetParent(slots[i].transform, false);
				itemObj.name = itemToAdd.Title;
				break;
			}
		}
	}
}
