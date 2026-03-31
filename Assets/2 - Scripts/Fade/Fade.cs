using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    [Tooltip("Image a fade")]
    [SerializeField] private Image _fadeImage;
    public Image FadeImage => _fadeImage;

    [Tooltip("Durée fondu en secondes")]

    public FadeDatasSO FadeDatasSO => _fadeDatasSO;
    [SerializeField] private FadeDatasSO _fadeDatasSO;

    public void FadeOut(float fadeDuration, float delay = 0f, Action onComplete = null)
    {
        //Color fadeColor = _fadeImage.color;
        //Color newAlpha = new Color(fadeColor.r, fadeColor.g, fadeColor.b, 1);
        //_fadeImage.color = newAlpha;
        StopAllCoroutines();
        StartCoroutine(ProcessFade(1f, fadeDuration, onComplete, delay));
    }

    public void FadeIn(float fadeDuration, float delay = 0f, Action onComplete = null)
    {
        //Color fadeColor = _fadeImage.color;
        //Color newAlpha = new Color(fadeColor.r, fadeColor.g, fadeColor.b, 255);
        //_fadeImage.color = newAlpha;
        StopAllCoroutines();
        StartCoroutine(ProcessFade(0f, fadeDuration, onComplete, delay)); // () => { _fadeImage.gameObject.SetActive(false); onComplete?.Invoke(); })
    }



    private IEnumerator ProcessFade(float targetAlpha, float fadeDuration, Action onComplete = null, float delay = 0)
    {
        yield return new WaitForSeconds(targetAlpha == 0 ? delay : 0);

        _fadeImage.gameObject.SetActive(true);
        float startAlpha = _fadeImage.color.a;
        float time = 0;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            Color tempColor = _fadeImage.color;
            tempColor.a = Mathf.Lerp(startAlpha, targetAlpha, time / fadeDuration);
            _fadeImage.color = tempColor;
            yield return null;
        }

        Color finalColor = _fadeImage.color;
        finalColor.a = targetAlpha;
        _fadeImage.color = finalColor;

        if (targetAlpha == 0f)
            _fadeImage.gameObject.SetActive(false);

        yield return new WaitForSeconds(targetAlpha == 1 ? delay : 0);

        onComplete?.Invoke();
    }
}
