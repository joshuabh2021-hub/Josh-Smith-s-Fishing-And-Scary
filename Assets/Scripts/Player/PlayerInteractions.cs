using UnityEngine;

public partial class Player
{
    void HandleInteractions()
    {
        if (Input.GetKeyDown(KeyCode.E) ) //if the e key has been pressed in this frame
        {
            IInteractable nearbyInteractable = CheckForInteractables(); // creates a temporary IInteractable object and sets it as the result of CheckForInteractables
            if (nearbyInteractable != null) // If our nearby interactable exists
            {
                nearbyInteractable.Interact(); //call the interact function of the interactable
            }
        }
    }
    
    IInteractable CheckForInteractables() // checks to see if there are any interactables near the player. if so, returns the closest, otherwise returns null
    {
        Collider2D interactable = Physics2D.OverlapCircle(transform.position, 5);  // checks for collision objects (trigger is counter) in a 5 unit circle centered around the player
        if (interactable != null) // if our interactable collider exists
        {
            return interactable.GetComponent<IInteractable>(); // returns the IInteractable component
        }
        return null; // returns null
    }
}
