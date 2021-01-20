using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{

    public float moveSpeed;
    public float jumpForce;

    public int healthCount;
    public static int coinCount;

    private Rigidbody2D rb;
    private Animator animator;
    private AudioSource audioSource;


    public GameObject coinText;
    public GameObject healthText;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        float hVelocity = 0;
        float vVelocity = 0;

        if(Input.GetKey(KeyCode.LeftArrow))
        {
            hVelocity = -moveSpeed;
            transform.localScale = new Vector3(-1, 1, 1);

            
        }
        else if(Input.GetKey(KeyCode.RightArrow))
        {
            hVelocity = moveSpeed;
            transform.localScale = new Vector3(1, 1, 1);

            
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            vVelocity = jumpForce;
            animator.SetTrigger("JumpTrigger");
        }

        hVelocity = Mathf.Clamp(rb.velocity.x + hVelocity, -5, 5);

        rb.velocity = new Vector2(hVelocity, rb.velocity.y + vVelocity);



        animator.SetFloat("xVelocity", Mathf.Abs(hVelocity));

        animator.SetFloat("xVelocity", 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Mace")
        {
            healthCount -= 10;

            healthText.GetComponent<Text>().text = "Health: " + healthCount;
        }

        if(collision.gameObject.tag == "Coin")
        {
            Destroy(collision.gameObject);

            coinText.GetComponent<Text>().text = "Coin: " + coinCount;
        }
    }
}
