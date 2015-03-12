using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
  public float JumpSpeed;
  public float MoveSpeed;
  public NPCFollower Follower;
  public float FuelDepletionRateOnActivate;
  public float FuelDepletionRateOnPress;
  public bool PlayerDead = false;
  public float RespawnWaitTime;
  public float AmountToDepleteOnRespawn;
  public Transform MostRecentSpawnPoint;
  public bool ShouldRespawn;
  public SpriteRenderer FollowerSprite;
  public bool ShouldAllowJump = false;
  public BoxCollider2D ChildCollider;

  OxygenBarController oxygenController;
  FollowersController followerController;
  public Animator animator;
  Rigidbody2D body2D;
  Slider jetFuelMeter;
  bool isGrounded;
  bool stopMoving = false;
  StatTracker statTracker;

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
    statTracker = GameObject.FindGameObjectWithTag("StatTracker").GetComponent<StatTracker>();
  }
  
  public IEnumerator WaitThenRespawn()
  {
    yield return new WaitForSeconds(RespawnWaitTime);

    statTracker.RespawnsStat++;
	  NpcEncounterLoader.FailActiveNpcs(null);
    followerController.DisableFollower();
    gameObject.GetComponent<PolygonCollider2D>().enabled = true;
    oxygenController.OxygenSlider.value -= AmountToDepleteOnRespawn;
    if(MostRecentSpawnPoint != null)
    {
      transform.position = MostRecentSpawnPoint.position;
      PlayerDead = false;

      jetFuelMeter.value = 1;
  	  foreach(GameObject go in GameObject.FindGameObjectsWithTag("Power")) {
	 	go.GetComponent<SpriteRenderer>().enabled = true;
	  }
    }
  }

  void Update()
  {
    if(ShouldAllowJump && Input.GetKeyUp(KeyCode.Space) && jetFuelMeter.value > 0)
    {
      jetFuelMeter.value -= FuelDepletionRateOnPress;
    }

    if (oxygenController.OxygenSlider.value <= 0)
    {
      statTracker.DeathsStat++;
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
      if(ShouldAllowJump && Input.GetKey(KeyCode.Space) && jetFuelMeter.value > 0)
      {
        stopMoving = false;
        PlayerDead = false;
        jetFuelMeter.value -= FuelDepletionRateOnActivate * Time.deltaTime;
        isGrounded = false;
        // not grounded, not falling, so we're flying

        animator.SetBool("Jump", true);
      }
    }

    if (!PlayerDead)
    {
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

  void FixedUpdate()
  {
    if (!PlayerDead)
    {
      if (!stopMoving)
      {
        // Move the character
        body2D.velocity = new Vector2(MoveSpeed * Time.deltaTime, body2D.velocity.y);
      }
    }

    bool falling = Input.GetKeyUp(KeyCode.Space) && !isGrounded;
    
    if(!falling)
    {
      // pressing space and grounded, so we're flying and not grounded
      if(ShouldAllowJump && Input.GetKey(KeyCode.Space) && jetFuelMeter.value > 0)
      {
        body2D.velocity = new Vector2(body2D.velocity.x, JumpSpeed);
      }
    }
  }

  bool isChildCollision = false;
  void OnCollisionEnter2D(Collision2D coll)
  {
    isChildCollision = false;
    foreach(ContactPoint2D contact in coll.contacts)
    {
      if(contact.otherCollider.gameObject.tag == ChildCollider.gameObject.tag)
      {
        isChildCollision = true;
        break;
      }
    }

    if(!isChildCollision)
    {
      if (coll.gameObject.tag == "GroundObject")
      {
        isGrounded = true;
        jetFuelMeter.value = 1;
        // running
        animator.SetBool("Jump", false);
        animator.SetTrigger("Run");
      }
    }
    
    if (coll.gameObject.tag == "KillCollider")
    {
      stopMoving = true;
      body2D.velocity = Vector2.zero;
    }
  }
  
  public void TurnOffColliders()
  {
    gameObject.GetComponent<PolygonCollider2D>().enabled = false;
    ChildCollider.enabled = false;
  }
}
