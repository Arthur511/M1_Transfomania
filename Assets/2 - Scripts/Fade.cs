using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    [Tooltip("Image a fade")]
    [SerializeField] private Image _fadeImage;

    [Tooltip("Durée fondu en secondes")]
    [SerializeField] private float _fadeDuration = 1.0f;

    public void FadeOut()
    {
        StopAllCoroutines();
        StartCoroutine(ProcessFade(1f));
    }

    public void FadeIn()
    {
        StopAllCoroutines();
        StartCoroutine(ProcessFade(0f));
    }

    private IEnumerator ProcessFade(float targetAlpha)
    {
        float startAlpha = _fadeImage.color.a;
        float time = 0;

        while (time < _fadeDuration)
        {
            time += Time.deltaTime;

            Color tempColor = _fadeImage.color;
            tempColor.a = Mathf.Lerp(startAlpha, targetAlpha, time / _fadeDuration);
            _fadeImage.color = tempColor;

            yield return null;
        }

        Color finalColor = _fadeImage.color;
        finalColor.a = targetAlpha;
        _fadeImage.color = finalColor;
    }
}
