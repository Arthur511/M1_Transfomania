using System;
using System.Collections;
using TMPro;
using UnityEngine;
using Image = UnityEngine.UI.Image;

public class CameraSwap : MonoBehaviour
{

    public GameObject MainCamera;
    public GameObject Cinematicbrain;
    public float time_before_swap_again;

    public float time_before_swap_material;
    public GameObject thumbnail;
    public GameObject Image;
    public Sprite nouveauSprite;

    public string[] introtxt;
    public TMP_Text asset;
    public float time_before_next_txt;
    int count = 0;


    private void Awake()
    {
        Image.gameObject.SetActive(false);
        asset.gameObject.SetActive(false);
    }

    public void StartIntro(Action onComplete = null)
    {
        Image.gameObject.SetActive(true);
        asset.gameObject.SetActive(true);
        MainCamera.SetActive(false);
        Cinematicbrain.SetActive(true);
        StartCoroutine(WaitAndSwapMaterial());
        StartCoroutine(WaitAndSwapAgain(onComplete));
        asset.GetComponent<TMP_Text>().text = introtxt[0];
        StartCoroutine(WaitAndSwapTxt());

    }

    IEnumerator WaitAndSwapTxt()
    {

        for (int i = 0; i < introtxt.Length; i++)
        {
            yield return new WaitForSeconds(time_before_next_txt);
            asset.GetComponent<TMP_Text>().text = introtxt[count];
            count++;
        }
        yield return new WaitForSeconds(time_before_next_txt+1.592f);
        asset.enabled = false;       
       
    }

    IEnumerator WaitAndSwapMaterial()
    {
        yield return new WaitForSeconds(time_before_swap_material);
        Image.GetComponent<Image>().sprite = nouveauSprite;
    }

    IEnumerator WaitAndSwapAgain(Action onComplete)
    {
        yield return new WaitForSeconds(time_before_swap_again + MainGame.Instance.UIManager.Fade.FadeDatasSO.NextLevel_FadeDuration);
        Cinematicbrain.SetActive(false);
        MainCamera.SetActive(true);
        yield return null;
        LevelManager lm = MainGame.Instance.LevelManager;
        lm.StateMachine.SwitchState(new Level_State_PlayerTurn(lm));

        onComplete?.Invoke();
    }


}
