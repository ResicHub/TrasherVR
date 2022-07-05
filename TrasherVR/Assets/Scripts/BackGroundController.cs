using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BackGroundController : MonoBehaviour
{
    private Image image;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    public void SceneOff()
    {
        StartCoroutine(BGCoroutine(true));
    }

    public void SceneOn()
    {
        StartCoroutine(BGCoroutine(false));
    }

    private IEnumerator BGCoroutine(bool isGoOn)
    {
        if (isGoOn)
        {
            float t = 0;
            while (t <= 1)
            {
                Color color = image.color;
                color.a = Mathf.Lerp(0f, 1f, t);
                yield return image.color = color;
                t += Time.deltaTime * 2;
            }
        }
        else
        {
            float t = 0;
            while (t <= 1)
            {
                Color color = image.color;
                color.a = Mathf.Lerp(1f, 0f, t);
                yield return image.color = color;
                t += Time.deltaTime * 2;
            }
        }
    }
}
