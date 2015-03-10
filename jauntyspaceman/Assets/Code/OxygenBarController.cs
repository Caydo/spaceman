using UnityEngine;
using UnityEngine.UI;

public class OxygenBarController : MonoBehaviour
{
  public Slider OxygenSlider;
  public float OxygenToLoseEachFrame;
	public float defaultO2Loss = 0.0f;
  
	public void LoseDefaultO2() 
  {
		LoseOxygen (defaultO2Loss);
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
    if(OxygenSlider != null)
    {
      LoseOxygen(Time.deltaTime * OxygenToLoseEachFrame);
    }
  }
}
