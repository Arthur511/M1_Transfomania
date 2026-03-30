using UnityEngine;

public class PlaySoundClic : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

       



    }

    public void PLAYSOUND()
    {
        SoundManager.PlaySound(SoundType.CLIC, 50f);
    }


}
