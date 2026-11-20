using UnityEngine;

public partial class Player : MonoBehaviour //partial class, allows code to be organised into different "scripts" although they are apart of the same system
{
    private Rigidbody2D rb; // rigidbody of player
    [SerializeField] private float movementSpeed = 5; // can be changed in editor. default is 5
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); //gets the rigidbody2d component from the player. only works if the script and the rigidybody2d are connected to the same gameobject
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 inputVector = Vector2.right * Input.GetAxisRaw("Horizontal"); // gets the players horizontal input (d = 1, a = -1)
        inputVector += Vector2.up * Input.GetAxisRaw("Vertical"); // gets the players vertical input (w = 1,s = -1)
        if(inputVector.x != 0 && inputVector.y != 0)
        {
            inputVector /= 1.5f; //dampens diagonal movement so that it is not faster than movement in any one direction
        }
        rb.linearVelocity = inputVector * movementSpeed; // sets the players velocity to the input vector multiplied by the movement speed

        HandleInteractions(); //calls the handleInteractions function. this is found in the "playerInteractions.cs" script as it is apart of our player script
    }
}
