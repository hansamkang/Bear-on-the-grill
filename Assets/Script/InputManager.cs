using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    Inventory inventory;
    Player player;
    bool enableInput = true;

    // Start is called before the first frame update
    void Start()
    {
        inventory = FindObjectOfType<Inventory>().GetComponent<Inventory>();
        player = FindObjectOfType<Player>().GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enableInput)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                inventory.selectSlot(0);
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                inventory.selectSlot(1);
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                inventory.selectSlot(2);
            }
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                inventory.selectSlot(3);
            }
            if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                inventory.selectSlot(4);
            }
            if (Input.GetKeyDown(KeyCode.Alpha6))
            {
                inventory.selectSlot(5);
            }
            if (Input.GetKeyDown(KeyCode.Alpha7))
            {
                inventory.selectSlot(6);
            }
            if (Input.GetKeyDown(KeyCode.Alpha8))
            {
                inventory.selectSlot(7);
            }
            if (Input.GetKeyDown(KeyCode.Alpha9))
            {
                inventory.selectSlot(8);
            }
            if (Input.GetKeyDown(KeyCode.Alpha0))
            {
                inventory.selectSlot(9);
            }

            if (Input.GetKeyDown(KeyCode.X))
            {
                inventory.useInvenItem();
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                inventory.dropInvenItem();
            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                player.pickUpItem();
            }
        }
    }
}
