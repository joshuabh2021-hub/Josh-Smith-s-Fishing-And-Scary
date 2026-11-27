using UnityEngine;

public class PlayerHook : MonoBehaviour
{

    private Rigidbody2D rb;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float rotataionSpeed;

    private Camera fishingCamera;

    float xBound = 10;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        fishingCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        //moveThroughRotation();
        MoveWithMouse();
        
    }
    
    void LateUpdate()
    {
        HandleHookCameraBounds(); // 
    }

    void HandleHookCameraBounds() // Checks if Camera is nearing camera bounds and moves the camera accordingly.
    {
        //This function uses viewport coordinates. (1,1) = top right. (0,0) = bottum left
        if(fishingCamera.WorldToViewportPoint(transform.position).x > 0.7f && fishingCamera.transform.position.x < xBound) // check if the hook is past the specified right boundary of the camera and if the camera is not past its maximum xbound
        {
            PullCameraToHook(0.7f,isVertical:false); // calls the pullCamera to hook function using the specified bound and specifies that it is not involving vertical bounds
        }
        if(fishingCamera.WorldToViewportPoint(transform.position).x < 0.3f && fishingCamera.transform.position.x > -xBound)// checks if the hook is past the specified left boundary of the camera and if the camera is not past its negative x bound
        {
            PullCameraToHook(0.3f,isVertical:false); // calls the pullCameraToHook function using the specified bound and specifies that it is not involving vertical bounds
        }
        if(fishingCamera.WorldToViewportPoint(transform.position).y < 0.3f ) // checks if the hook is below the specified bottum boundary of the camera
        {
            PullCameraToHook(0.3f, isVertical:true); // calls the PullCameraToHook function, using the specified bound and specifies that it involes the Vertical bound
        }
        if(fishingCamera.WorldToViewportPoint(transform.position).y > 0.7f ) // checks if the hook is above the specified top boundary of the camera
        {
            PullCameraToHook(0.7f, isVertical:true); // Calls the pullCameraToHook function using the spcified bound and specifies
        }
    }
    

    void PullCameraToHook(float boundaryCoordinates, bool isVertical) //used to move the camera based on the hooks relative position
    {

        if (isVertical) // checks if we are moving the camera vertically
        {
            float amountToMove = transform.position.y; // sets the amount to move to the y value of the hooks position. this is used for calculations later
            transform.position = new Vector2(transform.position.x,fishingCamera.ViewportToWorldPoint(new Vector2(0,boundaryCoordinates)).y); // moves hook to edge of bounds
            amountToMove -= transform.position.y; // subtracts the previous hook position by the current position to get the amount moved
            GameManager.instance.MoveGameWorldWithArrow(amountToMove); // calls the MoveGameWorldWithArrow function in the game manager while passing the amounttomove
        }
        else // if we are not moving the camera vertically
        {
            float cameraHookDifference = fishingCamera.WorldToViewportPoint(transform.position).x - boundaryCoordinates; // uses worldToViewportPoint to find the hook position on the viewport and subtracts the boundary coordinated from it to get the distance between the hook and the boundry
            Vector3 newCameraPosition = fishingCamera.ViewportToWorldPoint(new Vector2(0.5f + cameraHookDifference ,0.5f)); // calculates the posiiton the camera needs to move to to follow the hook
            fishingCamera.transform.position = new Vector3(newCameraPosition.x, 0, fishingCamera.transform.position.z); // sets the cameras position to the new caluclated position (only on the x axis)
        }
        // line 71
    }//line 72

    void MoveThroughRotation() // needs more testing, this feels clunky as of right now
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 directionToMouse = (mousePosition - (Vector2) transform.position).normalized;
        transform.up = Vector2.MoveTowards(transform.up,-directionToMouse,Time.deltaTime * rotataionSpeed);
        rb.linearVelocity = -transform.up * movementSpeed;
    }
    private void MoveWithMouse()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 directionToMouse = (mousePosition - (Vector2) transform.position).normalized;

        if(fishingCamera.WorldToViewportPoint(transform.position).x > 0.99f && directionToMouse.x > 0 || fishingCamera.WorldToViewportPoint(transform.position).x < 0.01f && directionToMouse.x < 0)
        {
            directionToMouse = new Vector2(0,directionToMouse.y);
        }
        if(fishingCamera.WorldToViewportPoint(transform.position).y > 0.99f && directionToMouse.y > 0 || fishingCamera.WorldToViewportPoint(transform.position).y < 0.01f && directionToMouse.y < 0)
        {
            directionToMouse = new Vector2(directionToMouse.x,0);
        }
        float distanceToMouse = Vector2.Distance(mousePosition,transform.position);
        if(distanceToMouse > 1)
        {
            transform.up = -directionToMouse;
        }
        rb.linearVelocity = directionToMouse * movementSpeed;
    }

    void OnTriggerEnter2D(Collider2D collision) // is called whenever the rigidbody colliders intercect other trigger colliders
    {
        GameObject collisionGameObject = collision.gameObject; // gets the game object of the collided trigger
        if (collisionGameObject.CompareTag("Fish")) // checks if the collided object has the "fish" tag
        {
            GameManager.instance.AddFishToBucket(collisionGameObject.GetComponent<Fish>());
            collisionGameObject.SetActive(false);
        }

    }
}
//end of code