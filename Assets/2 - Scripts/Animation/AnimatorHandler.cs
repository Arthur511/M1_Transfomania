using System;
using UnityEngine;

/// <summary>
/// Parent of animations controllers
/// Manager reading, CrossFade, parameters (float/bool/int) 
/// </summary>
[RequireComponent(typeof(Animator))]
public abstract class AnimatorHandler : MonoBehaviour
{
    [Header("References")]
    [Tooltip("If null, animator of on GameObject is used")]
    [SerializeField] protected Animator _animator;

    protected int CurrentStateHash { get; private set; }

    /// <summary>True = bloque tout changement d'animation (ex: pendant la mort)</summary>
    public bool IsLocked { get; private set; }


    protected virtual void Awake()
    {
        if (_animator == null)
            _animator = GetComponent<Animator>();

        if (_animator == null)
            Debug.LogError($"[AnimatorHandler] No animator set and {gameObject.name} have not animator");
    }

    /// <summary>
    /// Play an animation with AnimationEntry (CrossFade or Trigger depend on configuraiton)
    /// </summary>
    public void Play(AnimationEntry entry)
    {
        if (!CanPlay(entry)) return;

        switch (entry.parameterType)
        {
            case AnimatorControllerParameterType.Trigger:
                PlayTrigger(entry);
                break;
            case AnimatorControllerParameterType.Bool:
                SetBool(entry.parameterName, entry.boolValue);
                break;
            case AnimatorControllerParameterType.Int:
                SetInt(entry.parameterName, entry.intValue);
                break;
            case AnimatorControllerParameterType.Float:
                SetFloat(entry.parameterName, entry.floatValue);
                break;
            default:
                Debug.LogWarning($"[AnimatorHandler] Type de paramètre non géré : {entry.parameterType}");
                break;
        }
    }

    /// <summary>
    /// Direct CrossFade to a state hash (used for explicitely named states)
    /// </summary>
    public void CrossFade(int stateHash, float duration = 0.1f, int layer = 0)
    {
        if (IsLocked) return;
        CurrentStateHash = stateHash;
        _animator.CrossFadeInFixedTime(stateHash, duration, layer);
    }

    /// <summary> Direct CrossFade by name</summary>
    public void CrossFade(string stateName, float duration = 0.1f, int layer = 0) => CrossFade(Animator.StringToHash(stateName), duration, layer);


    public void SetFloat(string param, float value, float dampTime = 0f, float deltaTime = 0f)
    {
        if (dampTime > 0f)
            _animator.SetFloat(param, value, dampTime, deltaTime > 0f ? deltaTime : Time.deltaTime);
        else
            _animator.SetFloat(param, value);
    }

    public void SetFloat(int hash, float value, float dampTime = 0f, float deltaTime = 0f)
    {
        if (dampTime > 0f)
            _animator.SetFloat(hash, value, dampTime, deltaTime > 0f ? deltaTime : Time.deltaTime);
        else
            _animator.SetFloat(hash, value);
    }

    public void SetBool(string param, bool value) => _animator.SetBool(param, value);
    public void SetBool(int hash, bool value) => _animator.SetBool(hash, value);
    public void SetInt(string param, int value) => _animator.SetInteger(param, value);
    public void SetInt(int hash, int value) => _animator.SetInteger(hash, value);

    public void SetTrigger(string param) => _animator.SetTrigger(param);
    public void SetTrigger(int hash) => _animator.SetTrigger(hash);
    public void ResetTrigger(string param) => _animator.ResetTrigger(param);
    public void ResetTrigger(int hash) => _animator.ResetTrigger(hash);


    /// <summary>Change global speed of animator</summary>
    public void SetSpeed(float speed) => _animator.speed = speed;

    /// <summary>Reset animator global speed to 1</summary>
    public void ResetSpeed() => _animator.speed = 1f;

    /// <summary>Set animation to pause</summary>
    public void Pause() => _animator.speed = 0f;


    /// <summary>
    /// Lock animator (ex: death animation, cinematics, ...)
    /// </summary>
    public void Lock()   => IsLocked = true;

    /// <summary>Unloack animator</summary>
    public void Unlock() => IsLocked = false;


    /// <summary>True if the layer play the state of the new hash</summary>
    public bool IsPlayingState(int stateHash, int layer = 0) => _animator.GetCurrentAnimatorStateInfo(layer).shortNameHash == stateHash;

    /// <summary>True if the layer animation is over (normalizedTime >= 1) </summary>
    public bool IsAnimationComplete(int layer = 0) => _animator.GetCurrentAnimatorStateInfo(layer).normalizedTime >= 1f && !_animator.IsInTransition(layer);

    /// <summary>Normalized time of currently played animation</summary>
    public float GetNormalizedTime(int layer = 0) => _animator.GetCurrentAnimatorStateInfo(layer).normalizedTime;



    private bool CanPlay(AnimationEntry entry)
    {
        if (IsLocked)
        {
            Debug.Log($"[AnimatorHandler] Animation locked (IsLocked=true) : {entry.parameterName}");
            return false;
        }
        if (_animator == null) return false;
        return true;
    }

    private void PlayTrigger(AnimationEntry entry)
    {
        float duration = entry.crossFadeDuration;

        if (duration > 0f)
        {
            // CrossFade to new animation state
            int hash = entry.hash != 0 ? entry.hash : Animator.StringToHash(entry.parameterName);
            CurrentStateHash = hash;
            _animator.CrossFadeInFixedTime(hash, duration, entry.layer);
        }
        else
        {
            _animator.SetTrigger(entry.hash != 0 ? entry.hash : Animator.StringToHash(entry.parameterName));
        }
    }
}
