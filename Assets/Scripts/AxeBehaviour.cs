using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeBehaviour : MonoBehaviour {

    public float maxDistanceToHit;
    public float damage;
    public InventoryItem axe;
    private GameObject zombie;
    private GameObject player;
    private Inventory inventory;
    private bool readyToHit;

    void Start () {
        player = GameObject.FindWithTag("Player");
        inventory = player.GetComponent<Inventory>();
        zombie = GameObject.FindWithTag("Zombie");
        readyToHit = true;
    }
	 
	void Update () {
        if (zombie != null)
        {
            if (Input.GetButtonDown("Fire1") && inventory.items.ContainsKey(axe))
            {
                if (Vector3.Distance(zombie.transform.position, player.transform.position) < maxDistanceToHit)
                {
                    RaycastHit hit;
                    Ray ray = new Ray(player.transform.position, player.transform.forward);
                    if (Physics.Raycast(ray, out hit, maxDistanceToHit))
                        if (hit.collider == zombie.GetComponent<Collider>() && readyToHit)
                        {
                            readyToHit = false;
                            StartCoroutine("Hit");
                        }
                }
            }
        }
    }

    IEnumerator Hit()
    {
        zombie.SendMessage("ApplyDamage", damage, SendMessageOptions.DontRequireReceiver);
        yield return new WaitForSeconds(2);
        readyToHit = true;
    }
}