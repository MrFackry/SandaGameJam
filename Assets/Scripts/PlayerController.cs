using UnityEngine;

public class PlayerGravityFlip : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool isUpsideDown = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            FlipGravity();
        }
    }

    void FlipGravity()
    {
        // Invertimos la gravedad
        rb.gravityScale *= -1;

        rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * -1*Time.deltaTime);

        // Rotamos el personaje visualmente
        if (!isUpsideDown)
        {
            transform.rotation = Quaternion.Euler(0, 0, 180); // de cabeza
        }
        else
        {
            transform.rotation = Quaternion.identity; // normal
        }

        isUpsideDown = !isUpsideDown;
    }
}