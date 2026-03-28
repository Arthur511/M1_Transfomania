using UnityEngine;

/// <summary>
///  Enumarator who contains all enemy animations
/// </summary>
public enum AIAnim
{
    Idle = 0,
    Walk = 1,
    PickingUp = 2,
}


/// <summary>
/// ScriptableObject who contains all animations of an enemy 
/// </summary>
[CreateAssetMenu(fileName = "EnemyAnimationData", menuName  = "Animation/Enemy Animation Data", order = 2)]
public class AIAnimationSO : AnimationDataSO
  {
    public AnimationEntry Get(AIAnim anim) => GetAt((int)anim);
}
