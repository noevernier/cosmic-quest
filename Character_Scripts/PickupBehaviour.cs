using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupBehaviour : MonoBehaviour
{
    [SerializeField]
    private MoveBehaviour playerMovement;

    [SerializeField]
    private Animator playerAnimator;

    [SerializeField]
    private Inventory inventory;

    private Item currentItem;
    public void doPickup(Item item) {

        currentItem = item;
        //Ajoute l'objet passé à l'inventaire du joueur, joue l'animation de ramassage
        playerAnimator.SetTrigger("Pickup");
        playerMovement.canMove = false;
        
    }

    public void addItemToInventory() {
        inventory.addItem(currentItem.itemData);
        Destroy(currentItem.gameObject);

        currentItem = null;
    }

    public void activePlayerMovement() {
        playerMovement.canMove = true;
    }
}
