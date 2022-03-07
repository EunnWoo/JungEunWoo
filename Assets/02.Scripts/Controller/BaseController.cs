using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseController : MonoBehaviour
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
	}



	public abstract void Init();

	protected virtual void UpdateMoving() { }

	protected virtual void UpdateAttack() { }


	//Ÿ�ٰ� �Ÿ� ���
	protected Vector3 DestPos(Vector3 targetpoint)
	{
		Vector3 dest = targetpoint - transform.position;

		dest.y = 0;

		return dest;
	}
}
