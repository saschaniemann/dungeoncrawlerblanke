using UnityEngine;
using System.Collections;

public class PickableItem : MonoBehaviour {

	public InventoryItem inventoryItem;
	private Inventory inventory;
	private Transform player;
    public bool axe;

	void Start () {

		player = GameObject.FindGameObjectWithTag("Player").transform;
		inventory = player.GetComponent<Inventory>();

	}
	
	void OnMouseDown() 
	{
		if (inventory.AddItem (inventoryItem)) {				
			if (inventoryItem.picSound != null)
				AudioSource.PlayClipAtPoint(inventoryItem.picSound,player.position);
            if (axe)
                player.GetChild(3).GetChild(0).GetComponent<Renderer>().enabled = true;
			Destroy(gameObject);
		}		
	}
}