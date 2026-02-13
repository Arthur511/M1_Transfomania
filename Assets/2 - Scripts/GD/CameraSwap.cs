using System.Collections;
using Unity.Hierarchy;
using UnityEngine;

public class CameraSwap : MonoBehaviour
{

    public GameObject MainCamera;
    public GameObject Cinematicbrain;
    public float time_before_swap_again;
    
    void Start()
    {
        MainCamera.SetActive(false);
        Cinematicbrain.SetActive(true);
        StartCoroutine(WaitAndSwapAgain());
    }

    IEnumerator WaitAndSwapAgain()
    {
        yield return new WaitForSeconds(time_before_swap_again);
        Cinematicbrain.SetActive(false);
        MainCamera.SetActive(true);
        Debug.Log("worked");
    }
}
