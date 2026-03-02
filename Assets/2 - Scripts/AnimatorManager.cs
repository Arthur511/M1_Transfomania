using UnityEngine;

public class AnimatorManager : MonoBehaviour
{
    public void PlayAnimation(Animator animator, string animTrigger)
    {
        animator.Play(animTrigger);
    }
}
