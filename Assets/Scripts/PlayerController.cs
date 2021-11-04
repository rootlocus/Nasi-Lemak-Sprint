using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    public int playerSpeed = 10;
    public int playerJumpPower = 1250;
    public int health = 100;
    public int gameSpeed = 6;
    public GameObject scoreBoard;
    public GameObject playerHealthBar;
    public GameObject playerJumpBar;
    public float defaultDebuffTime = 5;

    private float moveX;
    private bool facingRight = false;
    private bool isDead = false;
    private GameObject[] obstacles;
    private Animator playerAnimator;
    private Rigidbody2D playerRB;
    private Score scoreController;
    private HealthBar healthController;
    private JumpBar jumpbarController;
    private bool isGrounded;
    private bool isSlowed;
    private float currentDebuffTime;
    

    // Start is called before the first frame update
    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        playerRB = gameObject.GetComponent<Rigidbody2D>();
        scoreController = scoreBoard.GetComponent<Score>();
        healthController = playerHealthBar.GetComponent<HealthBar>();
        jumpbarController = playerJumpBar.GetComponent<JumpBar>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!this.isDead) {
            if(health > 0) {
                PlayerMove();
            } else {
                PlayerDeath();
            }
        }

        if(isSlowed) {
            PlayerSlowed();
        }
        isGrounded = playerRB.velocity.y == 0;
    }

    void OnTriggerEnter2D (Collider2D collide) {
         if(collide.CompareTag("Obstacle")) {
            if(collide.GetComponent<Obstacle>().obstacleName == "Salad") {
                health -= 20;
                // if (playerJumpPower + 200 >= 1250) {
                //     playerJumpPower = 1250;
                // } else {
                //     playerJumpPower = playerJumpPower + 200;
                // }
                // gameSpeed += collide.GetComponent<Obstacle>().gainSpeed;
            }
            if(collide.GetComponent<Obstacle>().obstacleName == "Nasi Lemak") {
                health -= collide.GetComponent<Obstacle>().damage;
                ResetDebuff();
                scoreController.addScore(20);
                // gameSpeed -= collide.GetComponent<Obstacle>().gainSpeed;
            }
            healthController.setHealth(health);
        }
    }

    void PlayerMove()
    {
        //CONTROLS
        moveX = Input.GetAxis("Horizontal");
        if(Input.GetButtonDown("Jump") && isGrounded) { // check if also grounded
            Jump();
        }
        //ANIMATIONS
        //PLAYER DIRECTION
        // if(moveX < 0.0f && facingRight == false)
        // {
        //     FlipPlayer();
        // } else if( moveX > 0.0f  && facingRight == true)
        // {
        //     FlipPlayer();
        // }
        playerRB.velocity = new Vector2(moveX * playerSpeed, playerRB.velocity.y);
    }

    void FlipPlayer()
    {
        facingRight = !facingRight;
        Vector2 localScale = gameObject.transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    void Jump()
    {
        GetComponent<Rigidbody2D>().AddForce(Vector2.up * playerJumpPower);
    }

    /*
    * turn player gray
    * turn player side ways
    * stop spawner, stop all items in its position TODO
    * 
    */
    void PlayerDeath()
    {
        this.isDead = true;
        GetComponent<SpriteRenderer>().color = Color.gray;
        this.transform.Rotate (Vector3.forward * 90);
        playerAnimator.enabled = false;
        gameSpeed = 0;

        //stop all objects
        obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
        
        foreach(GameObject obstacle in obstacles) {
            obstacle.GetComponent<Obstacle>().speed = 0;
        }
    }

    void ResetDebuff()
    {
        // if(currentDebuffTime == 0) { //initialize
            // playerJumpPower -= 200;
        isSlowed = true;
        currentDebuffTime = defaultDebuffTime;
        playerJumpPower = 1000;

        // }bhv 
    }

    void PlayerSlowed()
    {
        // if(currentDebuffTime == 0) { //initialize
            // playerJumpPower -= 200;
        // }bhv 

        if(currentDebuffTime > 0) { //when theres still time
            currentDebuffTime -= Time.deltaTime;
        }
        if(currentDebuffTime < 0) { // when timer runs out
            isSlowed = false;
            playerJumpPower = 1250;
            currentDebuffTime = 0;
        }
        jumpbarController.setJumpbar(playerJumpPower);

    }
}
