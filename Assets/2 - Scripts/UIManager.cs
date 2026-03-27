using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text_LolipopCount;

    public void UpdateLolipopText(int newValue)
    {
        text_LolipopCount.text = newValue.ToString();
    }
}
