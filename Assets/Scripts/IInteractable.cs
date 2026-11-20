using UnityEngine;

public interface IInteractable //interface allows scripts that inherit from them to have the same function with unique code. 
//this is good for interactables such as chests and doors as we just call the function
{
    void Interact();
}
