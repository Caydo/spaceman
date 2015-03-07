using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
  public float JumpSpeed;
  public float MoveSpeed;
  public bool CanBoost = true;
  Rigidbody2D body2D;
  Slider jetFuelMeter;
  bool isGrounded;

  void Awake()
  {
    body2D = gameObject.GetComponent<Rigidbody2D>();
  }

  void Start()
  {
    jetFuelMeter = GameObject.FindGameObjectWithTag("JetFuelMeter").GetComponent<Slider>();
  }

  void Update()
  {
    if(Input.GetKey(KeyCode.Space) && jetFuelMeter.value > 0)
    {
      body2D.velocity += new Vector2(body2D.velocity.x, JumpSpeed);
      jetFuelMeter.value -= 0.05f;
      isGrounded = false;
    }
    else
    {
      if(isGrounded && jetFuelMeter.value <= 0)
      {
        jetFuelMeter.value = 1;
      }
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

  void OnCollisionEnter2D(Collision2D coll)
  {
    if(coll.gameObject.tag == "GroundObject")
    {
      isGrounded = true;
    }
  }
}
