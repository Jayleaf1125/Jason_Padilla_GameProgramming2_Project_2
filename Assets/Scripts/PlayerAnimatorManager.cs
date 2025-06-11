using UnityEngine;

public class PlayerAnimatorManager : MonoBehaviour
{
    public AnimationClip idleAnim;
    public AnimationClip walkAnim;
    public AnimationClip runAnim;
    public AnimationClip jumpAnim;
    public AnimationClip dashAnim;

    Animator _animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void PlayIdleAnimation() => _animator.Play(idleAnim.name);
    public void PlayWalkAnimation() => _animator.Play(walkAnim.name);
    public void PlayRunAnimation() => _animator.Play(runAnim.name);
    public void PlayJumpAnimation() => _animator.Play(jumpAnim.name);
    public void PlayDashAnimation() => _animator.Play(dashAnim.name);

}
