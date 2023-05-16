using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Inventory : MonoBehaviour
{
    public GameObject PlayerInventory;
    public FirstPersonController first;
    public KatanaController katana;
    public static Inventory Instance;
    private bool isInventoryActive = false;
    public List<Item> Items = new List<Item>();

    public Transform ItemContent;
    public GameObject InventoryItem;

    public InventoryItemController[] InventoryItems;
    private void Awake()
    {
        Instance = this;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleInventory();
        }
    }

    private void ToggleInventory()
    {
        isInventoryActive = !isInventoryActive;

        if (isInventoryActive)
        {
            // Disable FPSController script to stop movement and rotation
            first.GetComponent<FirstPersonController>().enabled = false;
            katana.GetComponent<KatanaController>().enabled = false;
            // Enable inventory UI or game object
            PlayerInventory.SetActive(true);
            
            ListItems();

            // Show the crosshair

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            
            // Pause the game by setting time scale to 0
        }
        else
        {
            // Enable FPSController script to resume movement and rotation
            first.GetComponent<FirstPersonController>().enabled = true;
            katana.GetComponent<KatanaController>().enabled = true;
            // Disable inventory UI or game object
            PlayerInventory.SetActive(false);
            CleanItems();
            // Hide the crosshair

            // Unlock and show the cursor
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            // Resume the game by setting time scale back to 1
        }
    }

    public void Add(Item item)
    {
        Items.Add(item);
    }

    public void Remove(Item item)
    {
        Items.Remove(item);
    }

    public void ListItems()
    {
       
        //Populate inventory
        foreach(var item in Items)
        {
            GameObject obj = Instantiate(InventoryItem, ItemContent);
            var itemName = obj.transform.Find("ItemName").GetComponent<Text>();
            var itemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();

            itemName.text = item.itemName;
            itemIcon.sprite = item.icon;
        }
        SetInventoryItems();
    }

    public void CleanItems()
    {
        foreach (Transform item in ItemContent)
        {
            Destroy(item.gameObject);
        }
    }
    public void SetInventoryItems()
    {
        InventoryItems = ItemContent.GetComponentsInChildren<InventoryItemController>();

        for(int i=0; i<Items.Count; i++)
        {
            InventoryItems[i].AddItem(Items[i]);
        }

    }
 
}
