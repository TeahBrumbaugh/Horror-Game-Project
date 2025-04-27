using UnityEngine;

public class TestPlayerMovement : MonoBehaviour
{
    public float speed = 10f;

    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveX, 0f, moveZ);

        transform.Translate(movement * speed * Time.deltaTime, Space.Self);
    }
}