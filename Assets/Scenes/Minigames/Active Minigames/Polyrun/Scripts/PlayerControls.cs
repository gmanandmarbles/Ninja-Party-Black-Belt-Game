using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    [Header("Default jumping power")]
    public float jumpPower = 6f;
    [Header("Boolean isGrounded")]
    public bool isGrounded = false;
    float posX = 0.0f;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = transform.GetComponent<Rigidbody2D>();
        posX = transform.position.x;
    }
    // Update is called once per frame
    void Update()
    {

    }
    void FixedUpdate()
    {
        if(Input.GetKey("space") && isGrounded)
        {
            rb.AddForce(Vector3.up * (jumpPower * rb.mass * rb.gravityScale * 20.0f));
        }
        if (transform.position.x < posX)
        {
            GameOver();
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Ground")
        {
            isGrounded = true;
        }
        if (collision.collider.tag == "Enemy")
        {
            GameOver();
        }
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.collider.tag == "Ground")
        {
            isGrounded = false;
        }
    }
    void GameOver()
    {
       GameObject.Find("GameController").GetComponent<GameController>().GameOver();
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Coin")
        {
            Destroy(collision.gameObject);

            GameObject.Find("GameController").GetComponent<GameController>().IncrementScore(); 
        }
    }

}

//                                                                                                                                                                                                                                 09343DC21798DAD949C79A8F7105504E6E28A9574744427F8241E5A1EFCF26B9
