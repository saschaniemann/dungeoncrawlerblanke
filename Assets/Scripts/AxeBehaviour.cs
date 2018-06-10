using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeBehaviour : MonoBehaviour {

    public float maxDistanceToHit;
    public float damage;
    public InventoryItem axe;
    public float cooldown;
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
        InvokeRepeating("AxeAnimation", 0, 0.1f);
        yield return new WaitForSeconds(0.3f);
        CancelInvoke();
        player.transform.GetChild(3).transform.Rotate(0, 0, -4*40);
        yield return new WaitForSeconds(cooldown);
        readyToHit = true;
    }

    void AxeAnimation()
    {
        player.transform.GetChild(3).transform.Rotate(0,0,40);
    }
}