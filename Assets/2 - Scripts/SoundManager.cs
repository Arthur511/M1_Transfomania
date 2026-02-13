
using UnityEngine;
public enum SoundType
{
    COLLECTINGSOUND1,
    COLLECTINGSOUND2,
    COLLECTINGSOUND3,
    CREAMONGROUND,
    THROWCREAM,
    ENEMYDYING,
    CLICK,
    NEWLEVEL,
    PLAYERTAKEDAMAGE1,
    PLAYERTAKEDAMAGE2,
    PLAYERTAKEDAMAGE3,
    PLAYERTAKEDAMAGE4,
    RIBBON,
    SAVEFAIRY,
}

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioClip[] soundlist;
    private static SoundManager Instance;
    private AudioSource AudioSource;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        AudioSource = GetComponent<AudioSource>();
    }

    public static void PlaySound(SoundType sound, float volume = 1)
    {
        if (Instance == null) return;
        Instance.AudioSource.PlayOneShot(Instance.soundlist[(int)sound], volume);
    }

    public static void PlayRandomSound(int startIndex, int endIndex, float volume = 1f)
    {
        if (Instance == null || Instance.soundlist.Length == 0)
            return;

        startIndex = Mathf.Clamp(startIndex, 0, Instance.soundlist.Length - 1);
        endIndex = Mathf.Clamp(endIndex, 0, Instance.soundlist.Length - 1);

        int randomIndex = UnityEngine.Random.Range(startIndex, endIndex + 1);
        AudioClip clip = Instance.soundlist[randomIndex];

        if (clip != null)
            Instance.AudioSource.PlayOneShot(clip, volume);
    }

}

