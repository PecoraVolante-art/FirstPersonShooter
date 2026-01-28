using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    CharacterController CC;
    public float speed = 12F;
    public float gravity = -9.8f;

    public float jumpHeight;
    Vector3 Velocity;

    bool GroundedPlayer;
    void Start()
    {
        CC = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        GroundedPlayer = CC.isGrounded;

        if (GroundedPlayer && Velocity.y < 0)
        {
            Velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move= transform.right * x + transform.forward * z;
        CC.Move(move * speed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && GroundedPlayer)
        {
            Velocity.y = Mathf.Sqrt(jumpHeight * -3.0f * gravity);
        }

        Velocity.y += gravity * Time.deltaTime;
        CC.Move(Velocity*Time.deltaTime);
    }
}
