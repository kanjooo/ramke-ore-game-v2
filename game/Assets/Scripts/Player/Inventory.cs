using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public GameObject PlayerInventory;
    public FirstPersonController first;

    private bool isInventoryActive = false;

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

            // Enable inventory UI or game object
            PlayerInventory.SetActive(true);

            // Show the crosshair

            // Lock and hide the cursor
            Cursor.lockState = CursorLockMode.None;
            
            Cursor.visible = true;

            // Pause the game by setting time scale to 0
            Time.timeScale = 0f;
        }
        else
        {
            // Enable FPSController script to resume movement and rotation
            first.GetComponent<FirstPersonController>().enabled = true;

            // Disable inventory UI or game object
            PlayerInventory.SetActive(false);

            // Hide the crosshair

            // Unlock and show the cursor
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            // Resume the game by setting time scale back to 1
            Time.timeScale = 1f;
        }
    }

}
