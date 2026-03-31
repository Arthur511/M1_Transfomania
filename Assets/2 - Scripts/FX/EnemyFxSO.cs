using UnityEngine;


[CreateAssetMenu(fileName = "EnemyFxDatas", menuName = "FX/Enemy FX Data", order = 0)]
public class EnemyFxSO : ScriptableObject
{
    public GameObject DeathFX => _deathFX;
    [SerializeField] private GameObject _deathFX;
}
