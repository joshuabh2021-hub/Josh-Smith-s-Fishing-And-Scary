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
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 directionToMouse = (mousePosition - (Vector2) transform.position).normalized;
        transform.up = Vector2.MoveTowards(transform.up,-directionToMouse,Time.deltaTime * rotataionSpeed);
        rb.linearVelocity = -transform.up * movementSpeed;
    }
}
