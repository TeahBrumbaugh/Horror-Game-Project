using UnityEngine;

public class TestPlayerMovement : MonoBehaviour
{
    public float speed = 10f;
    private MovementBlocker blocker;

    void Start()
    {
        blocker = GetComponent<MovementBlocker>();
    }

    void Update()
    {
    // Block movement if Answer Sheet is open and you're typing
        if (blocker != null && !blocker.IsMovementAllowed())
            return;
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveX, 0f, moveZ);

        transform.Translate(movement * speed * Time.deltaTime, Space.Self);
    }
}