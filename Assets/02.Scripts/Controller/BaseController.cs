using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public abstract class BaseController : MonoBehaviourPun
{
	public Animator Anim{get;set;}
	
	[SerializeField]
	protected GameObject _lockTarget;
	public bool IsDie { get => CurrentHealth <= 0; }
	public float CurrentHealth;


	private void Start()
	{
		Init();
	}


    private void Update()
    {
		
		UpdateMoving();
		UpdateAttack();
	}



	public abstract void Init();

	protected virtual void UpdateMoving() { }

	protected virtual void UpdateAttack() { }


	//타겟과 거리 계산
	protected Vector3 DestPos(Vector3 targetpoint)
	{
		Vector3 dest = targetpoint - transform.position;

		dest.y = 0;

		return dest;
	}
}
