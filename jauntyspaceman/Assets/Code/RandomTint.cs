using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class RandomTint : MonoBehaviour
{
  public Color[] Colors;
  public string GameplaySceneName;
  public float ColorChangeDelay;
  public float ColorChangeTransitionTime;
  public bool ShouldDoCrazy;

  Image image;

  void Start()
  {
    image = GetComponent<Image>();
    StartCoroutine(MakeCrazyColors());
  }

  IEnumerator MakeCrazyColors()
  {
    while (Application.loadedLevelName == GameplaySceneName)
    {
      while(!ShouldDoCrazy)
      {
        yield return null;
      }

      yield return new WaitForSeconds(ColorChangeDelay);
      int getRandomColorNumber = Random.Range(0, Colors.Length - 1);
      Color color = Colors[getRandomColorNumber];

      image.CrossFadeColor(color, ColorChangeTransitionTime, false, false);
    }
  }
}
