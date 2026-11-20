using UnityEngine;

public partial class Player
{
    void HandleInteractions()
    {
        if (Input.GetKeyDown(KeyCode.E) ) //if the e key has been pressed in this frame
        {
            IInteractable nearbyInteractable = CheckForInteractables();
            if (nearbyInteractable != null)
            {
                nearbyInteractable.Interact();
            }
        }
    }
    
    IInteractable CheckForInteractables()
    {
        if (Physics2D.OverlapCircle(transform.position, 5))
        {
            return Physics2D.OverlapCircle(transform.position, 5).GetComponent<IInteractable>();
        }
        return null;
    }
}
