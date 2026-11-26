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
        if(fishingCamera.WorldToViewportPoint(transform.position).x > 0.7f )
        {
            float cameraHookDifference = fishingCamera.WorldToViewportPoint(transform.position).x - 0.7f;
            fishingCamera.transform.position = fishingCamera.ViewportToWorldPoint(new Vector2(0.5f + cameraHookDifference ,0.5f));
        }
        if(fishingCamera.WorldToViewportPoint(transform.position).x < 0.3f )
        {
            float cameraHookDifference = fishingCamera.WorldToViewportPoint(transform.position).x - 0.3f;
            fishingCamera.transform.position = fishingCamera.ViewportToWorldPoint(new Vector2(0.5f + cameraHookDifference ,0.5f));
        }
    }

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
        float distanceToMouse = Vector2.Distance(mousePosition,transform.position);
        if(distanceToMouse > 1)
        {
            transform.up = -directionToMouse;
        }
        rb.linearVelocity = directionToMouse * movementSpeed;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject collisionGameObject = collision.gameObject;
        if (collisionGameObject.CompareTag("Fish"))
        {
            GameManager.instance.AddFishToBucket(collisionGameObject.GetComponent<Fish>());
            collisionGameObject.SetActive(false);
        }

    }
}
