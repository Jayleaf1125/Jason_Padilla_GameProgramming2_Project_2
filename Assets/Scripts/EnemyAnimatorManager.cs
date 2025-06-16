using UnityEngine;

public class EnemyAnimatorManager : MonoBehaviour
{
    [Header("Movement Animations")]
    public AnimationClip idleAnim;
    public AnimationClip walkAnim;
    public AnimationClip runAnim;

    [Header("Combat Animations")]
    public AnimationClip attackOneAnim;
    public AnimationClip attackTwoAnim;

    [Header("Taking Damage Animations")]
    public AnimationClip takingDamageAnim;
    public AnimationClip deathAnim;

    Animator _animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void PlayIdleAnimation() => _animator.Play(idleAnim.name);
    public void PlayWalkAnimation() => _animator.Play(walkAnim.name);
    public void PlayRunAnimation() => _animator.Play(runAnim.name);
    public void PlayAttackOneAnimation() => _animator.Play(attackOneAnim.name);
    public void PlayAttackTwoAnimation() => _animator.Play(attackTwoAnim.name);
    public void PlayTakingDamageAnimation() => _animator.Play(takingDamageAnim.name);
    public void PlayDeathAnimation() => _animator.Play(deathAnim.name);
    public void SetEnemyHurtingTrue() => _animator.SetBool("isHurting", true);
    public void SetEnemyHurtingFalse() => _animator.SetBool("isHurting", false);
}
