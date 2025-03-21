using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    [SerializeField] private Animator m_anim;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PlayerAttack playerAttack;

    private void Awake() 
    {
        playerMovement.Jumping += SetJumpAnimation;    
        playerMovement.Landing += SetLandingAnimation;
        // playerAttack.Attack += SetAttackAnimation;
        // playerAttack.LightAttack += SetLightAttackAnimation;
        // playerAttack.HeavyAttack += SetHeavyAttackAnimation;
        
        playerAttack.LightAttack += SetAttackAnimation;
        playerAttack.HeavyAttack += SetAttackAnimation;
    }


    private void LateUpdate() 
    {
        SetAnimationVariables();
    }
    
    private void SetAnimationVariables()
    {
        var x = Input.GetAxis("Horizontal");
        var y = Input.GetAxis("Vertical");

        var moving = Mathf.Abs(x) + Mathf.Abs(y) > 0.25 ? true : false;

        m_anim.SetFloat("VelocityX", x);
        m_anim.SetFloat("VelocityY", y);

        m_anim.SetBool("Moving", moving);
        m_anim.SetBool("Running", playerMovement.IsSprinting());

        m_anim.SetBool("Falling", playerMovement.isFalling());

        m_anim.SetBool("isGrounded", playerMovement.IsGrounded());

    }

    private void SetJumpAnimation() => m_anim.SetTrigger("JumpUp");

    private void SetLandingAnimation() => m_anim.SetTrigger("Landing");

    private void SetAttackAnimation() => m_anim.SetTrigger("Attack");
    
    private void SetLightAttackAnimation()
    {
        m_anim.SetInteger("LightAttack", 1);
        StartCoroutine(ResetAttackParameter("LightAttack"));
    }
    
    private void SetHeavyAttackAnimation()
    {
        m_anim.SetInteger("HeavyAttack", 1);
        StartCoroutine(ResetAttackParameter("HeavyAttack"));
    }
    
    private IEnumerator ResetAttackParameter(string paramName)
    {
        yield return new WaitForSeconds(0.1f);
        m_anim.SetInteger(paramName, 0);
    }


    #region cleanup

    private void OnDestroy()
    {
        playerMovement.Jumping -= SetJumpAnimation;    
        playerMovement.Landing -= SetLandingAnimation;
        // playerAttack.Attack -= SetAttackAnimation;
        // playerAttack.LightAttack -= SetLightAttackAnimation;  
        // playerAttack.HeavyAttack -= SetHeavyAttackAnimation;
        playerAttack.LightAttack -= SetAttackAnimation;
        playerAttack.HeavyAttack -= SetAttackAnimation;
    }
    #endregion
}
