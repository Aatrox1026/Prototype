using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using custom_class;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    public float jumpSpeed;
    public float sideDirectionSpeed;
    float init_moveSpeed;

    [Header("Slow")]
    public bool isSlow = false;
    public float slowTime = 5.0f;
    public float slowTimer = 0;

    [Header("Explode")]
    public float explodeForce;

    [Header("Status")]
    public bool isGrounded = false;
    public bool isTouchWall = false;
    public Pair respawnPoint = new Pair(0, 0);

    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        init_moveSpeed = moveSpeed;
        respawnPoint.Set(transform.position.x, transform.position.y + 1);
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isSlow)
        {
            slowTimer += Time.deltaTime;
            if(slowTimer >= slowTime)
            {
                slowTimer = 0;
                moveSpeed = init_moveSpeed;
                isSlow = false;
            }
        }
        rb.velocity = new Vector2(Input.GetAxis("Horizontal") * moveSpeed, rb.velocity.y);
        if (Input.GetKeyDown(KeyCode.Space))
            Jump();
        if (Input.GetKeyUp(KeyCode.Space))
            JumpRelease();
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Wall")
        {
            isGrounded = true;
            if (collision.gameObject.tag == "Wall")
                isTouchWall = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Wall")
        {
            isGrounded = false;
            if (collision.gameObject.tag == "Wall")
                isTouchWall = false;
        }
    }

    void Jump()
    {
        if (isGrounded)
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
    }

    void JumpRelease()
    {
        if (rb.velocity.y < 0)
            return;
        rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
    }

    public void Slow()
    {
        slowTimer = 0;
        if(!isSlow)
        {
            moveSpeed *= 0.6f;
            isSlow = true;
        }
    }

    public void Explode()
    {
        float r = Random.Range(0, Mathf.PI);
        rb.AddForce(new Vector2(explodeForce * Mathf.Cos(r), explodeForce * Mathf.Sin(r)));
    }

    public void Fall()
    {
        Respawn();
    }

    void Respawn()
    {
        transform.position = new Vector3(respawnPoint.first(), respawnPoint.second(), 1);
    }
}
