using UnityEngine;

public class CameraPlayerFollower : MonoBehaviour
{
  public GameObject PlayerObject;

  void Update()
  {
    transform.position = new Vector3(PlayerObject.transform.position.x, transform.position.y, transform.position.z);
  }
}
