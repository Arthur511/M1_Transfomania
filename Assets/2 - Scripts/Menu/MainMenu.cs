using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    [SerializeField] GameObject _mainMenu;
    [SerializeField] GameObject _settings;
    [SerializeField] GameObject _credits;

    public void OnPlay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void OnSetting()
    {
        _mainMenu.SetActive(false);
        _settings.SetActive(true);
    }
    public void OnCredits()
    {
        _mainMenu.SetActive(false);
        _credits.SetActive(true);
    }
    public void OnQuit()
    {
        Application.Quit();
    }
    public void OnBackFromSettings()
    {
        _settings.SetActive(false);
        _mainMenu.SetActive(true);
    }
    public void OnBackFromCredits()
    {
        _credits.SetActive(false);
        _mainMenu.SetActive(true);
    }
}
