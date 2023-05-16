using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{

    public Item item;

    void PickUp()
    {
        Inventory.Instance.Add(item);
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        PickUp();
    }
}
