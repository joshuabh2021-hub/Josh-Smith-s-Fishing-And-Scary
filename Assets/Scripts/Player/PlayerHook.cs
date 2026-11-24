using UnityEngine;

public class PlayerHook : MonoBehaviour
{

    private Rigidbody2D rb;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float rotataionSpeed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //moveThroughRotation();
        MoveWithMouse();
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
