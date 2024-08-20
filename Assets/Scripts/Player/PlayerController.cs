using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Physics and Animation")]
    // private static PlayerController instance;
    [SerializeField] private Rigidbody2D playerRb;

    [Header("Player Horizontal Movement")]
    [SerializeField] private float walkSpeed = 5f;
    private float xAxis;

    [Header("Player Vertical Movement")]
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private Transform groundCheckPoint;
    [SerializeField] private float groundCheckY = 0.2f;
    [SerializeField] private float groundCheckX = 0.5f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private bool isWalking = false;

    [Header("Player Audio")]
    [SerializeField] private AudioSource jumpAudio;

    // private void Awake() {
    //     if(instance && instance != this)
    //     {
    //         Destroy(gameObject);
    //         return;
    //     }

    //     instance = this;
    //     DontDestroyOnLoad(gameObject);
    // }

    private void Update()
    {
        if (Globals.isUsing == -1)
        {
            GetInputs();
            Move();
            //PlayAudioWalk();
            Flip();
            Jump();
        }
    }

    private void PlayAudioWalk() {
        if (playerRb.velocity.x != 0 && IsGrounded()) {
            if (!isWalking) {
                isWalking = true;
                //audioPlayer.instance.play_audio_walk();
            } 
        } else {
            if (isWalking) {
                isWalking = false;
                //audioPlayer.instance.stop_audio_walk();
            } 
        }

    }

    private void GetInputs()
    {
        xAxis = Input.GetAxisRaw("Horizontal");
    }

    private void Move()
    {
        playerRb.velocity = new Vector2(walkSpeed * xAxis, playerRb.velocity.y);
    }

    public bool IsGrounded()
    {
        if (Physics2D.Raycast(groundCheckPoint.position, Vector2.down, groundCheckY, groundLayer)
        || Physics2D.Raycast(groundCheckPoint.position + new Vector3(groundCheckX, 0, 0), Vector2.down, groundCheckY, groundLayer)
        || Physics2D.Raycast(groundCheckPoint.position + new Vector3(-groundCheckX, 0, 0), Vector2.down, groundCheckY, groundLayer))
        {
            return true;
        }
        return false;
    }

    private void Flip()
    {
        if (xAxis > 0)
        {
            transform.localScale = new Vector2(-Mathf.Abs(transform.localScale.x), transform.localScale.y);
        }
        else if (xAxis < 0)
        {
            transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x), transform.localScale.y);
        }
    }

    private void Jump()
    {
        // Extra jump control, unused
        // if (Input.GetButtonUp("Jump") && playerRb.velocity.y > 0)
        // {
        //     playerRb.velocity = new Vector2(playerRb.velocity.x, 0);
        // }
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            playerRb.velocity = new Vector2(playerRb.velocity.x, jumpForce);
            jumpAudio.Play();
        }
    }

    public void TogglePlayer()
    {
        playerRb.constraints = RigidbodyConstraints2D.FreezeRotation;
        playerRb.gravityScale = 1f;
        gameObject.SetActive(false);
    }

    public void Stop()
    {
        playerRb.velocity = Vector2.zero;
    }
}
