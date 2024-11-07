using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Playercontroller : MonoBehaviour
{
    public float moveSpeed = 10f;
    public Rigidbody2D rb;

    private float moveX;
    private float topScore=0.0f;
    public Text scoreText;
    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (moveX < 0)
        {
            this.GetComponent<SpriteRenderer>().flipX = false;
        }
        else
        {
            this.GetComponent<SpriteRenderer>().flipX = true;
        }
            if (rb.velocity.y > 0 && transform.position.y > topScore)
        {
            topScore = transform.position.y;
        }
        scoreText.text = "Score:" + Mathf.Round(topScore).ToString();
     }
    private void FixedUpdate()
    {
        moveX = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2 (moveX * moveSpeed, rb.velocity.y);
    }
}
