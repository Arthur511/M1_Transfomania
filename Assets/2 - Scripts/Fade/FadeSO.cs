using UnityEngine;


[CreateAssetMenu(fileName = "FadeDatas", menuName = "Fade/Fade Data", order = 0)]
public class FadeDatasSO : ScriptableObject
{
    public float NextLevel_FadeDuration => _nextLevel_FadeDuration;
    [Tooltip("Durée du fade au changement de niveau")]
    [SerializeField] private float _nextLevel_FadeDuration;
    public float NextLevel_FullOpacityDuration => _nextLevel_fullOpacityDuration;
    [Tooltip("Durée du maintient du fade à opacitée maximum durant un changement de level")]
    [SerializeField] private float _nextLevel_fullOpacityDuration;


    public float Death_FadeDuration => _death_FadeDuration;
    [Tooltip("Durée du fade à la mort")]
    [SerializeField] private float _death_FadeDuration;

    public float Death_FullOpacityDuration => _death_fullOpacityDuration;
    [Tooltip("Durée du maintient du fade à opacitée maximum durant la mort")]
    [SerializeField] private float _death_fullOpacityDuration;


    public float ToMenu_FadeDuration => _toMenu_FadeDuration;
    [Tooltip("Durée du fade durant le retour au menu")]
    [SerializeField] private float _toMenu_FadeDuration;

    public float ToMenu_FullOpacityDuration => _toMenu_fullOpacityDuration;
    [Tooltip("Durée du maintient du fade lors du retour au menu")]
    [SerializeField] private float _toMenu_fullOpacityDuration;

}
