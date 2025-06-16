using UnityEngine;
using System.Collections.Generic;

public class PlayerAnimatorManager : MonoBehaviour
{
    [Header("Movement Animations")]
    public AnimationClip idleAnim;
    public AnimationClip walkAnim;
    public AnimationClip runAnim;
    public AnimationClip jumpAnim;
    public AnimationClip dashAnim;

    [Header("Combat Animations")]
    public AnimationClip attackOneAnim;
    public AnimationClip attackTwoAnim;
    public AnimationClip attackThreeAnim;
    public AnimationClip defendAnim;
    public AnimationClip defendSuccessfulAnim;

    [Header("Healing Animations")]
    public AnimationClip healingAnim;

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
    public void PlayJumpAnimation() => _animator.Play(jumpAnim.name);
    public void PlayDashAnimation() => _animator.Play(dashAnim.name);
    

    public void PlayAttackOneAnimation() => _animator.Play(attackOneAnim.name);
    public void PlayAttackTwoAnimation() => _animator.Play(attackTwoAnim.name);
    public void PlayAttackThreeAnimation() => _animator.Play(attackThreeAnim.name);
    public void PlayDefendAnimation() => _animator.Play(defendAnim.name);
    public void PlayDefendSuccessfulAnimation() => _animator.Play(defendSuccessfulAnim.name);
    public void PlayHealingAnimation() => _animator.Play(healingAnim.name);
    public void PlayTakingDamageAnimation() => _animator.Play(takingDamageAnim.name);
    public void PlayDeathAnimation() => _animator.Play(deathAnim.name);

    public void SetPlayerHealingTrue() => _animator.SetBool("isHealing", true);
    public void SetPlayerHealingFalse() => _animator.SetBool("isHealing", false);
    public void SetPlayerHurtingTrue() => _animator.SetBool("isHurting", true);
    public void SetPlayerHurtingFalse() => _animator.SetBool("isHurting", false);
}
