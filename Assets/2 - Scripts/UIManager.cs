using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Fade Fade;

    [SerializeField] private GameObject _lolipopIcon;
    [SerializeField] private TextMeshProUGUI _text_LolipopCount;
    [SerializeField] private Button _winScreenButton;
    [SerializeField] private Button _hideButton;
    [SerializeField] private Sprite _hideIcon;
    [SerializeField] private Sprite _unhideIcon;

    private void Awake()
    {
        _lolipopIcon.SetActive(false);
        _hideButton.gameObject.SetActive(false);
        _winScreenButton.gameObject.SetActive(false);
        //Color fadeColor = Fade.FadeImage.color;
        //Color newAlpha = new Color(fadeColor.r, fadeColor.g, fadeColor.b, 1);
        //Fade.FadeImage.color = newAlpha;
        Fade.FadeImage.gameObject.SetActive(true);
    }

    public void ShowLolipopIcon(int count)
    {
        _lolipopIcon.SetActive(count > 0 ? true : false);
    }

    public void UpdateLolipopText(int newValue)
    {
        _text_LolipopCount.text = newValue > 0 ? newValue.ToString() : "";
    }

    public void ShowHideButton()
    {
        _hideButton.gameObject.SetActive(true);
    }

    public void UpdateHideButton(bool hide)
    {
        _hideButton.GetComponent<Image>().sprite = hide ? _hideIcon : _unhideIcon;
    }

    public void DisplayWinScreen()
    {
        _winScreenButton.gameObject.SetActive(true);
    }

    public void OnClickWinScreen()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
