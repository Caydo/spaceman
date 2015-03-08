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
  public float RespawnWaitTime;
  public float AmountToDepleteOnRespawn;
  public Transform MostRecentSpawnPoint;
  public bool ShouldRespawn = true;
  public SpriteRenderer FollowerSprite;

  OxygenBarController oxygenController;
  FollowersController followerController;
  Animator animator;
  Rigidbody2D body2D;
  Slider jetFuelMeter;
  bool isGrounded;

  void Awake()
  {
    body2D = gameObject.GetComponent<Rigidbody2D>();
    animator = GetComponent<Animator>();
    followerController = GetComponent<FollowersController>();
  }

  void Start()
  {
    jetFuelMeter = GameObject.FindGameObjectWithTag("JetFuelMeter").GetComponent<Slider>();
    oxygenController = GameObject.FindGameObjectWithTag("OxygenController").GetComponent<OxygenBarController>();
  }
  
  public IEnumerator WaitThenRespawn()
  {
    yield return new WaitForSeconds(RespawnWaitTime);
    followerController.DisableFollower();
    gameObject.GetComponent<PolygonCollider2D>().enabled = true;
    oxygenController.OxygenSlider.value -= AmountToDepleteOnRespawn;
    if(MostRecentSpawnPoint != null)
    {
      transform.position = MostRecentSpawnPoint.position;
      PlayerDead = false;

      jetFuelMeter.value = 1;
    }
  }

  void Update()
  {
    if (oxygenController.OxygenSlider.value <= 0)
    {
      Application.LoadLevel("GameOver");
    }

    // let go of space bar and we're flying so we're falling
    if (Input.GetKeyUp(KeyCode.Space) && !isGrounded)
    {
      //// not grounded, not falling, so we're flying
      animator.SetBool("Jump", false);
      animator.SetTrigger("Fall");
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
        // not grounded, not falling, so we're flying

        animator.SetBool("Jump", true);
      }
    }

    if (!PlayerDead)
    {
      // Move the character
      body2D.velocity = new Vector2(MoveSpeed * Time.deltaTime, body2D.velocity.y);

      if(jetFuelMeter.value <= 0 && !isGrounded)
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
      jetFuelMeter.value = 1;
      // running
      animator.SetBool("Jump", false);
      animator.SetTrigger("Run");
    }
  }
}
