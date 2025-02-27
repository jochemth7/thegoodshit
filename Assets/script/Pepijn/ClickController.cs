using UnityEngine;

public class ClickController : MonoBehaviour
{
    public playerController playerController; // Reference to the PlayerController script

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Detect left mouse button click
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                playerController.HandleClick(hit.point, hit.collider.gameObject);
            }
        }
    }
}

