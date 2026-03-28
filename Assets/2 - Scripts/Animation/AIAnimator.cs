using UnityEngine;


/// <summary>
/// Enemy animation controller
/// Place on Enemy GameObject and assign an EnemyAnimationSO on it (can be different for each enemy)
/// </summary>
public class AIAnimator : AnimatorHandler
{
    [Header("Animation Datas")]
    [SerializeField] private AIAnimationSO data;


    protected override void Awake()
    {
        base.Awake();

        if (data == null)
        {
            Debug.LogError($"[EnemyAnimator] EnemyAnimationSO is not assigned on {gameObject.name}");
            return;
        }

        data.Initialize();
        _animator.speed = data.globalSpeed;
    }

    /// <summary>Play an enemy animation with EnemyAnim enumerator</summary>
    public void Play(AIAnim anim)
    {
        var entry = data.Get(anim);
        if (entry != null)
            Play(entry);
    }


    //public void PlayIdle() => Play(AIAnim.Idle);
    //public void PlayChase() => Play(AIAnim.Chase);
    //public void PlayAttack1() => Play(AIAnim.Attack1);
    //public void PlayHit() => Play(AIAnim.Hit);
    public void PlayIdle()
    {
        SetBool("IsPicking", false);
        SetBool("IsWalking", false);
    }
    public void PlayWalk()
    {
        SetBool("IsPicking", false);
        SetBool("IsWalking", true);
    }
    public void PlayPickingUp()
    {
        SetBool("IsWalking", false);
        SetBool("IsPicking", true);
    }

    /*
    /// <summary>
    /// Play the death animation and after lock the animator
    /// None animation can interupt the death animation after that
    /// </summary>
    public void PlayDeath(bool randomVariant = false)
    {
        Play(AIAnim.Death);
        Lock();
    }
    */

    public void PlayDeath()
    {
        PlayIdle();
        Lock();
    }
}
