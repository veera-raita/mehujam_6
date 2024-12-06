using UnityEngine;

public class VelocityRotate : MonoBehaviour
{
    private Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rb.linearVelocity.magnitude > 0.01f)
        {
            float newZRotation = Vector2.SignedAngle(Vector2.up, rb.linearVelocity.normalized);
            transform.eulerAngles = new Vector3(0, 0, newZRotation);
        }
    }
}
