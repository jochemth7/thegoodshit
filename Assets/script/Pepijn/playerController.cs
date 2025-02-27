using UnityEngine;

public class playerController : MonoBehaviour
{
    private Rigidbody rb;
    public float speed = 5f; // Adjust the speed as needed
    private Vector3 targetPosition;
    private bool isMoving = false;
    public float stoppingDistance = 0.1f; // Distance at which the player stops

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (rb == null)
        {
            Debug.LogError("Rigidbody component is missing on the player.");
        }

        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
    }

    void Update()
    {
        if (isMoving)
        {
            MoveToPosition(targetPosition);
        }
    }

    public void HandleClick(Vector3 targetPosition, GameObject clickedObject)
    {
        this.targetPosition = targetPosition;
        isMoving = true;

        // Handle any additional logic based on the clicked object
        Debug.Log("Clicked on: " + clickedObject.name);
    }

    private void MoveToPosition(Vector3 targetPosition)
    {
        Vector3 direction = (targetPosition - transform.position).normalized;
        float distance = Vector3.Distance(transform.position, targetPosition);

        if (distance > stoppingDistance)
        {
            rb.linearVelocity = direction * speed;
        }
        else
        {
            rb.linearVelocity = Vector3.zero;
            isMoving = false;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // Stop the player when a collision is detected
        rb.linearVelocity = Vector3.zero;
        isMoving = false;
    }
}
