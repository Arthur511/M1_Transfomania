using UnityEngine;

/// <summary>
/// Player animation controller
/// Place on Player GameObject and assign PlayerAnimationSO on it
/// </summary>
public class PlayerAnimator : AnimatorHandler
{
    [Header("Animations Datas")]
    [SerializeField] private PlayerAnimationSO data;


    protected override void Awake()
    {
        base.Awake();

        if (data == null)
        {
            Debug.LogError($"[PlayerAnimator] PlayerAnimationSO is not assigned on {gameObject.name}");
            return;
        }

        data.Initialize();
        _animator.speed = data.globalSpeed;
    }


    /// <summary>Play a player animation with PlayerAnim enumerator</summary>
    public void Play(PlayerAnim anim)
    {
        var entry = data.Get(anim);
        if (entry != null)
            Play(entry);
    }

    /*
    public void PlayIdle() => Play(PlayerAnim.Idle);
    public void PlayWalk() => Play(PlayerAnim.Walk);
    public void PlayRun() => Play(PlayerAnim.Run);
    public void PlayAttack1() => Play(PlayerAnim.Attack1);
    public void PlayInteract() => Play(PlayerAnim.Interact);
    public void PlayHit() => Play(PlayerAnim.Hit);
    */

    public void PlayIdle() => SetBool("isrunning", false);
    public void PlayWalk() => SetBool("isrunning", true);
    public void SetIsHiding(bool hiding) => SetBool("ishidding", hiding);

    /*
    /// <summary>
    /// Play the death animation and after lock the animator
    /// None animation can interupt the death animation after that
    /// </summary>
    public void PlayDeath()
    {
        Play(PlayerAnim.Death);
        Lock();
    }
    */
}
