using UnityEngine;
using UnityEngine.UI;

public class OxygenBarController : MonoBehaviour
{
  Slider slider;
  public float OxygenToLoseEachFrame;

  void Awake()
  {
    slider = GetComponent<Slider>();
  }

  public void LoseOxygen(float amount)
  {
    slider.value -= amount;
  }

  public void GainOxygen(float amount)
  {
    slider.value += amount;
  }

  void Update()
  {
    LoseOxygen(OxygenToLoseEachFrame);
  }
}
