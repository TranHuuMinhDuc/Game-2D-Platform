using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float jumpSpeed = 8f;
    private float direction = 0f;
    public Text scoreText;
 

    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask groundLayer;
    private bool isTouchingGround;

   
    private Rigidbody2D player;
    private Animator playerAnimation;

    private Vector3 respawnPoint;
    public GameObject fallDetector;
    public HealthBar healthBar;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Rigidbody2D>();
        playerAnimation = GetComponent<Animator>();
        respawnPoint = transform.position;
        scoreText.text = "Score: " + Scoring.totalScore;

        healthBar.OnHealthDepleted += RespawnPlayer;


    }

    // Update is called once per frame
    void Update()
    {
       

        isTouchingGround = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        direction = Input.GetAxis("Horizontal");

        

        if (direction > 0f)
        {
            player.linearVelocity = new Vector2(direction * speed, player.linearVelocity.y);
            transform.localScale = new Vector2(1.9723f, 1.9723f);
        }
        else if (direction < 0f)
        {
            player.linearVelocity = new Vector2(direction * speed, player.linearVelocity.y);
            transform.localScale = new Vector2(-1.9723f, 1.9723f);
        }
        else
        {
            player.linearVelocity = new Vector2(0, player.linearVelocity.y);
        }

        if (Input.GetButtonDown("Jump") && isTouchingGround == true)
        {
            playerAnimation.SetBool("isJumping", true);
            player.linearVelocity = new Vector2(player.linearVelocity.x, jumpSpeed);
        }else if (isTouchingGround)
        {
            playerAnimation.SetBool("isJumping", false);
        }

        playerAnimation.SetFloat("Speed", Mathf.Abs(player.linearVelocity.x));
        playerAnimation.SetBool("OnGround", isTouchingGround);

        fallDetector.transform.position = new Vector2(transform.position.x, fallDetector.transform.position.y);


        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }
       

        
    }

    private void Attack()
    {
        playerAnimation.SetTrigger("Attack");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "FallDetector")
        {
            transform.position = respawnPoint;
        }
        else if(collision.tag == "CheckPoint")
        {
            respawnPoint = transform.position;
        }else if(collision.tag == "NextLevel")
        {
          SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            respawnPoint = transform.position;
        }
        else if(collision.tag == "PreviousLevel")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
            respawnPoint = transform.position;
        }
        else if(collision.tag == "Gem")
        {
            Scoring.totalScore += 1;
            scoreText.text = "Score: " + Scoring.totalScore;
            collision.gameObject.SetActive(false);//Hide object in Scene but still in Scene and can't interact.
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Trap")
        {
            healthBar.Damage(0.002f);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(groundCheck.position, groundCheckRadius);
    }

    private void RespawnPlayer()
    {
        transform.position = respawnPoint; // Move the player to the respawn point
        Health.totalHealth = 1f; // Reset health
        healthBar.setSize(Health.totalHealth); // Update the health bar

    }

    private void OnDestroy()
    {
        // Unsubscribe from the event to avoid memory leaks
        healthBar.OnHealthDepleted -= RespawnPlayer;
    }

}
