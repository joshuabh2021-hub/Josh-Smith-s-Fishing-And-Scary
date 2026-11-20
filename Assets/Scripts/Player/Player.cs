using UnityEngine;

public partial class Player : MonoBehaviour //partial class, allows code to be organised into different "scripts" although they are apart of the same system
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InitializeMovement(); // calls the InitializeMovement function. this is found in the "PlayerMovement" script as it is apart of our Player Script
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();// calls the handle movement function. this is found in the "PlayerMovement" script as it is apart of our Player Script
        HandleInteractions(); //calls the handleInteractions function. this is found in the "playerInteractions.cs" script as it is apart of our player script
    }
}
