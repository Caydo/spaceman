using UnityEngine;
using UnityEngine.UI;

public class OxygenBarController : MonoBehaviour
{
  public Slider OxygenSlider;
  public float OxygenToLoseEachFrame;

  void Awake()
  {
    OxygenSlider = GetComponent<Slider>();
  }

  public void LoseOxygen(float amount)
  {
    OxygenSlider.value -= amount;
  }

  public void GainOxygen(float amount)
  {
    OxygenSlider.value += amount;
  }

  void Update()
  {
    LoseOxygen(OxygenToLoseEachFrame);
  }
}
