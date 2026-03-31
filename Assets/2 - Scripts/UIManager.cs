using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Fade Fade;

    [SerializeField] private TextMeshProUGUI _text_LolipopCount;
    [SerializeField] private Button _winScreenButton;
    [SerializeField] private Button _hideButton;
    [SerializeField] private Sprite _hideIcon;
    [SerializeField] private Sprite _unhideIcon;

    private void Awake()
    {
        _winScreenButton.gameObject.SetActive(false);
    }

    public void UpdateLolipopText(int newValue)
    {
        _text_LolipopCount.text = newValue.ToString();
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
