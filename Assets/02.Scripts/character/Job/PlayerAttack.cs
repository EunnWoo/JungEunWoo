using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private PlayerInput playerInput;
    public float del { get; protected set; }

    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        playerInput = GetComponent<PlayerInput>();
    }
    //private void Update()
    //{
    //    if (playerInput.fire)
    //    OnAttack();

    //}
    protected virtual void OnAttack()
    {
        if (playerInput.fire)
        {

            StartCoroutine(Use());
            StopCoroutine(Use());
        }


    }
    protected virtual IEnumerator Use()
    {
        Debug.Log("�θ� �ڷ�ƾ");
        yield return null;
    }

}
