using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
  public float JumpSpeed;
  public float MoveSpeed;
  public NPCFollower Follower;
  public float FuelDepletionRateOnActivate;
  public bool PlayerDead = false;

  Animator animator;
  Rigidbody2D body2D;
  Slider jetFuelMeter;
  bool isGrounded;
  bool isFlying;
  float cachedLastX = 0;
  int sameXPosCount = 0;

  void Awake()
  {
    body2D = gameObject.GetComponent<Rigidbody2D>();
    animator = GetComponent<Animator>();
  }

  void Start()
  {
    jetFuelMeter = GameObject.FindGameObjectWithTag("JetFuelMeter").GetComponent<Slider>();
  }


  void Update()
  {
    // let go of space bar and we're flying so we're falling
    if (Input.GetKeyUp(KeyCode.Space) && !isGrounded)
    {
      //// not grounded, not falling, so we're flying
      Debug.Log("Falling");
      animator.SetBool("Jump", false);
      animator.SetTrigger("Fall");
      //isFlying = false;
    }

    else
    {
      // pressing space and grounded, so we're flying and not grounded
      if (Input.GetKey(KeyCode.Space) && jetFuelMeter.value > 0)
      {
        PlayerDead = false;
        body2D.velocity = new Vector2(body2D.velocity.x, JumpSpeed);
        jetFuelMeter.value -= FuelDepletionRateOnActivate;
        isGrounded = false;
        isFlying = true;
        // not grounded, not falling, so we're flying
        
        animator.SetTrigger("Jump");
      }
    }

    if (!PlayerDead)
    {
      // Move the character
      body2D.velocity = new Vector2(MoveSpeed * Time.deltaTime, body2D.velocity.y);

      if(jetFuelMeter.value <= 0)
      {
        animator.SetBool("Jump", false);
        animator.SetTrigger("Fall");
      }

      if (jetFuelMeter.value < 1 && isGrounded)
      {
        jetFuelMeter.value = 1;
      }

      // Unneeded but just commenting out to maybe use later
      //// move left
      //if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
      //{
      //  transform.Translate(transform.right * -MoveSpeed * Time.deltaTime);
      //}

      //// move right
      //if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
      //{
      //  transform.Translate(transform.right * MoveSpeed * Time.deltaTime);
      //}
    }
  }

  void OnCollisionEnter2D(Collision2D coll)
  {
    if(coll.gameObject.tag == "GroundObject")
    {
      isGrounded = true;
      isFlying = false;
      jetFuelMeter.value = 1;
      // running
      animator.SetBool("Jump", false);
      animator.SetTrigger("Run");
    }
  }
}
