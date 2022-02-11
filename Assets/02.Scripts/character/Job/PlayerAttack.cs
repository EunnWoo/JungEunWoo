using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Animator animator;
    private PlayerInput playerInput;
    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        playerInput = GetComponent<PlayerInput>();
    }
    private void Update()
    {
        if (playerInput.fire)
            OnAttack();
    }
    public virtual void OnAttack()
    {
        Use();
        
    }
    protected virtual IEnumerator Use()
    {

        yield return null;
    }

}
