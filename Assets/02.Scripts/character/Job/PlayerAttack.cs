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
    public float range { get; protected set; }
    

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
    public virtual void OnAttack()
    {
     //   Debug.Log("OnAttack입장");
        
            StartCoroutine(Use());
            StopCoroutine(Use());
            
            
      


    }
    protected virtual IEnumerator Use()
    {
        Debug.Log("부모 코루틴");
        yield return null;
    }

}
