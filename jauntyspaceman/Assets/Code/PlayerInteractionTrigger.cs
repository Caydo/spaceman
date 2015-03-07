using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class PlayerInteractionTrigger : MonoBehaviour
{
  void OnCollisionEnter2D(Collision2D coll)
  {
    if(coll.gameObject.tag == "Player")
    {
      // if the player hit us, do a thing
    }
  }
}