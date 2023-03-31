using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rigidBody;
    private float inputX;
    private float speed;
    private Vector2 movement;
    private float jumpAmount;
    private bool onFloor;
    private bool jumping;
    private float jumpTime;
    private float buttonTime;
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        jumpAmount = 10;
        jumping = false;
        jumpTime = 0;
        buttonTime = 0.3f;
        speed = 10;
    }

    // Update is called once per frame
    void Update()
    {
        inputX = Input.GetAxis("Horizontal");

        rigidBody.velocity = new Vector2(inputX * speed, rigidBody.velocity.y);

        if (Input.GetKeyDown(KeyCode.Space) && onFloor)
        {
            jumping = true;
            jumpTime = 0;
        }
        if (jumping)
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpAmount);
            jumpTime += Time.deltaTime;
        }
        if (Input.GetKeyUp(KeyCode.Space) || jumpTime > buttonTime)
        {
            jumping = false;
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        Vector3 contactPoint = col.GetContact(0).point;
        Vector3 center = col.collider.bounds.center;
        Debug.Log("Contact: " + contactPoint.y + ", Center y: " + center.y);
        if (col.gameObject.CompareTag("Floor"))
        {
            onFloor = true;
        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Floor"))
        {
            onFloor = false;
        }
    }
}
