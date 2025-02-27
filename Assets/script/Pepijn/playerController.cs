using UnityEngine;

public class playerController : MonoBehaviour
{
    private Rigidbody rb;
    public float speed = 5f; // Adjust the speed as needed
    private Vector3 targetPosition;
    private bool isMoving = false;
    public float stoppingDistance = 0.1f; // Distance at which the player stops
    private GameObject pickedUpKey;

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

        if (pickedUpKey != null)
        {
            // Keep the key above the player's head
            pickedUpKey.transform.position = transform.position + Vector3.up * 2;
        }
    }

    public void HandleClick(Vector3 targetPosition, GameObject clickedObject)
    {
        this.targetPosition = targetPosition;
        isMoving = true;

        if (clickedObject.CompareTag("Key"))
        {
            PickUpKey(clickedObject);
        }
        else if (clickedObject.CompareTag("Door") && pickedUpKey != null)
        {
            RemoveDoor(clickedObject);
        }

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

    private void PickUpKey(GameObject key)
    {
        pickedUpKey = key;
        pickedUpKey.GetComponent<Collider>().enabled = false; // Disable the collider to prevent further interactions
        pickedUpKey.transform.SetParent(transform); // Make the key a child of the player
    }

    private void RemoveDoor(GameObject door)
    {
        Destroy(door);
        Destroy(pickedUpKey);
        pickedUpKey = null;
        Debug.Log("Door and key removed.");
    }

    void OnCollisionEnter(Collision collision)
    {
        // Stop the player when a collision is detected
        rb.linearVelocity = Vector3.zero;
        isMoving = false;
    }
}
