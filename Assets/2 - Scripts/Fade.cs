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
    [SerializeField] private float _fadeDuration = 1.0f;
    
    public void FadeOut(Action onComplete = null)
    {
        //Color fadeColor = _fadeImage.color;
        //Color newAlpha = new Color(fadeColor.r, fadeColor.g, fadeColor.b, 1);
        //_fadeImage.color = newAlpha;
        StopAllCoroutines();
        StartCoroutine(ProcessFade(1f, onComplete));
    }

    public void FadeIn(float delay = 0f, Action onComplete = null)
    {
        //Color fadeColor = _fadeImage.color;
        //Color newAlpha = new Color(fadeColor.r, fadeColor.g, fadeColor.b, 255);
        //_fadeImage.color = newAlpha;
        StopAllCoroutines();
        StartCoroutine(ProcessFadeIn(delay, onComplete)); // () => { _fadeImage.gameObject.SetActive(false); onComplete?.Invoke(); })
    }

    private IEnumerator ProcessFadeIn(float delay, Action onComplete)
    {
        if (delay > 0f)
            yield return new WaitForSeconds(delay);

        yield return ProcessFade(0f, onComplete);
    }



    private IEnumerator ProcessFade(float targetAlpha, Action onComplete = null)
    {
        _fadeImage.gameObject.SetActive(true);
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

        if (targetAlpha == 0f)
            _fadeImage.gameObject.SetActive(false);

        onComplete?.Invoke();
    }
}
