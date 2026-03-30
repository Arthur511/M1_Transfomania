using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class CameraSwap : MonoBehaviour
{

    public GameObject MainCamera;
    public GameObject Cinematicbrain;
    public float time_before_swap_again;

    public float time_before_swap_material;
    public GameObject thumbnail;
    public Material thumbnailswapped;

    public string[] introtxt;
    public TMP_Text asset;
    public float time_before_next_txt;
    int count = 0;


    void Start()
    {
        MainCamera.SetActive(false);
        Cinematicbrain.SetActive(true);
        StartCoroutine(WaitAndSwapMaterial());
        StartCoroutine(WaitAndSwapAgain());
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
        thumbnail.GetComponent<MeshRenderer>().material = thumbnailswapped;
    }

    IEnumerator WaitAndSwapAgain()
    {
        yield return new WaitForSeconds(time_before_swap_again);
        Cinematicbrain.SetActive(false);
        MainCamera.SetActive(true);
        LevelManager lm = MainGame.Instance.LevelManager;
        lm.StateMachine.SwitchState(new Level_State_PlayerTurn(lm));
    }


}
