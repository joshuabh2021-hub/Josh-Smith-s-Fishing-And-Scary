
using UnityEngine;

public class Fish : MonoBehaviour
{
    private Rigidbody2D rb;
    float timeUntilNextMove = 0;
    float moveTimer = 0;
    Vector2 moveDirection = new Vector2();
    [SerializeField] float movementSpeed;
    float randomMovementSpeed = 0;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.linearVelocity = Vector2.up * GameManager.instance.divingSpeedThisLevel;
        rb.linearVelocity += moveDirection * movementSpeed * randomMovementSpeed;
        moveTimer += Time.deltaTime;
        if(moveTimer > timeUntilNextMove)
        {
            moveTimer = 0;
            timeUntilNextMove = Random.Range(0.5f,3f);
            PickMovementDirection();
        }
    }

    void PickMovementDirection()
    {
        int randomNumber = Random.Range(0,2);
        transform.localScale = new Vector2(randomNumber == 0 ? 1 : -1,1);
        randomMovementSpeed = Random.Range(0.25f, 2);
        moveDirection = randomNumber == 0 ? Vector2.right : Vector2.left;
    }
}
