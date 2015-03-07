using UnityEngine;

public class PlayerController : MonoBehaviour
{
  public float JumpSpeed;
  public float MoveSpeed;
  public bool IsGrounded = true;

  void Update()
  {
    if(IsGrounded && Input.GetKey(KeyCode.Space))
    {
      gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * JumpSpeed);
      IsGrounded = false;
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
      IsGrounded = true;
    }
  }
}