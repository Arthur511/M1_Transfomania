using UnityEngine;

/// <summary>
/// Enumarator who contains all player animations
/// </summary>
public enum PlayerAnim
{
    Idle = 0,
    Walk = 1,
    Hide = 2,
    Unhide = 3,
}

/// <summary>
/// ScriptableObject who contains all player animations
/// </summary>
[CreateAssetMenu(fileName = "PlayerAnimationData", menuName = "AnimationSO/Player Animation Data", order = 1)]
public class PlayerAnimationSO : AnimationDataSO
{
    public AnimationEntry Get(PlayerAnim anim) => GetAt((int)anim);
}
