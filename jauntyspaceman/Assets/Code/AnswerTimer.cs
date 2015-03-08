using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AnswerTimer : MonoBehaviour
{
  public GameObject TimerGO;
  Slider timerSlider;
  float timeLeft;
  float maxTime;
  
  void Start()
  {
    timerSlider = TimerGO.GetComponent<Slider>();
  }

  public void EnableTimer(float maxTime)
  {
    timerSlider.value = 1;
    TimerGO.SetActive(true);
    timeLeft = maxTime;
    this.maxTime = maxTime;
    StartCoroutine(CountdownTime());
  }

  IEnumerator CountdownTime()
  {
    while(timeLeft >= 0)
    {
      yield return new WaitForSeconds(1);
      timerSlider.value = (timeLeft / maxTime);
      timeLeft--;
    }
  }
}
