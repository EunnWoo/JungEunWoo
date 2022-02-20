using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    public float del { get; protected set; }
    public float range { get; protected set; }
    public float canAttack { get; protected set; }
    [SerializeField]
    protected Animator animator;
    PlayerMovement playerMovement;


    private void OnEnable()
    {
        Debug.Log("Onenable");
        animator = GetComponentInChildren<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        playerMovement.playerAttack = GetComponent<PlayerAttack>(); // 어택을 상속받아 수정되는 값 다시 받아오기
    }



    public virtual void OnAttack()
    {
 
            StartCoroutine(Use());
            StopCoroutine(Use());


    }
    protected virtual IEnumerator Use()
    {
        
        yield return null;
    }

}
