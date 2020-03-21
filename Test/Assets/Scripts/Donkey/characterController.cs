using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class characterController : MonoBehaviour
{
    public float maxSpeed; //szybkość ruchu bohatera

    //skakanie
    bool grounded = false;
    float groundCheckRadius = 0.2f;
    public LayerMask groundLayer; //czyli utworzony groundlayer 
    public Transform groundCheck;
    public float jumpHeight;
    private float climbSpeed;//dr

    float inputVertical, inputHorizontal;//

    public float moveSpeed;//
    public LayerMask ladderMask;//

    public enum detectionModes
    {
        layerMask, tag
    }

    public detectionModes detectionMode;
    private bool isClimbing = false;

    Rigidbody2D RB; //komponent postaci
    Animator Animacja; //manipulacja animatorem
    bool facingRight;



    void Start()
    {
        RB = GetComponent<Rigidbody2D>(); //szuka assetów
        Animacja = GetComponent<Animator>();//-||-
        facingRight = true;

    }

    //Update jest do skoku i strzelania
    void Update()
    {

        if (grounded && Input.GetAxis("Jump") > 0) //czy wcisniety button
        {
            grounded = false;
            Animacja.SetBool("isGrounded", grounded);
            RB.AddForce(new Vector2(0, jumpHeight)); //mozliwosc ruchu w powietrzu
        }
        inputHorizontal = Input.GetAxis("Horizontal");//
        inputVertical = Input.GetAxis("Vertical");//
        RB.velocity = new Vector2(inputHorizontal * moveSpeed, 0f); //
        if(isClimbing)//
        {
            RB.gravityScale = 0;//
            RB.velocity = new Vector2(inputHorizontal * moveSpeed, inputVertical * moveSpeed);//
        }
        else//
        {
            RB.gravityScale = 5;//
        }
        if(detectionMode == detectionModes.layerMask)
        {
            if(RB.IsTouchingLayers(ladderMask))
            {
                isClimbing = true;
            }
            else
            {
                isClimbing = false;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (detectionMode == detectionModes.tag)
        {
            if (collision.tag == ("Ladder"))
            {
                isClimbing = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (detectionMode == detectionModes.tag)
        {
            if (collision.tag == ("Ladder"))
            {
                isClimbing = false;
            }
        }
    }



    void OnTriggerEnter2D(Collider2D col) //KOD DO RESTARTU SCENY PO KOLIZJI
    {
        if(col.tag=="beczka")
        {
            Scene scene;
            scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
    }

    // Update is called once per frame
    void FixedUpdate() //fizyka zawsze przez FIXED
    {
        //start skoku, if grounded(zaznaczyc warstwy i collider), jak nie = fall
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        Animacja.SetBool("isGrounded", grounded);

        Animacja.SetFloat("verticalSpeed", RB.velocity.y); //klatka po klatce

        //koniec skoku

        float move = Input.GetAxis("Horizontal"); // edit->project settings->input
                                                  //czyli WSAD;
        float move2 = Input.GetAxis("Vertical");

        Animacja.SetFloat("speed", Mathf.Abs(move));//absolut value

        RB.velocity = new Vector2(move * maxSpeed, RB.velocity.y); //jak nie naciskasz to 0;

        if (move > 0 && !facingRight)
        {
            flip();
        }
        else if (move < 0 && facingRight)
        {
            flip();
        }


        void flip()
        {
            facingRight = !facingRight;
            Vector3 theScale = transform.localScale;
            theScale.x *= -1; //x pomnoz razy -1, czyli odwroc;
            transform.localScale = theScale;
        }


    }
}
