using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum Layer
{
	Item = 6,
	Monster = 7,
	Npc = 8,
	Ground = 9


}
public class CursorController : MonoBehaviour
{
	
	int _mask = (1 << (int)Layer.Npc) | (1 << (int)Layer.Monster) | (1 << (int)Layer.Ground);

	Texture2D _attackIcon;
	Texture2D _handIcon;
	Texture2D _talkIcon;
	enum CursorType
	{
		None,
		Attack,
		Hand,
		Talk
	}

	CursorType _cursorType = CursorType.None;

	void Start()
	{
		_attackIcon = Managers.Resource.Load<Texture2D>("Cursor/Attack");
		_handIcon = Managers.Resource.Load<Texture2D>("Cursor/Hand");
		_talkIcon = Managers.Resource.Load<Texture2D>("Cursor/TalkNPC");
	}

	void Update()
	{
		
		if (Input.GetMouseButton(0))
			return;

		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

		RaycastHit hit;
		if (Physics.Raycast(ray, out hit, 100.0f, _mask))
		{
			if (hit.collider.gameObject.layer == (int)Layer.Monster)
			{
				if (_cursorType != CursorType.Attack)
				{

					Cursor.SetCursor(_attackIcon, new Vector2(_attackIcon.width / 5, 0), CursorMode.Auto);
					_cursorType = CursorType.Attack;
				}
			}
			else if(hit.collider.gameObject.layer ==(int)Layer.Npc)
			{
				if (_cursorType != CursorType.Talk)
				{
					
					Cursor.SetCursor(_talkIcon, new Vector2(_talkIcon.width / 3, 0), CursorMode.Auto);
					_cursorType = CursorType.Talk;
				}
			}
			else
            {
				if (_cursorType != CursorType.Hand)
				{
					Cursor.SetCursor(_handIcon, new Vector2(_handIcon.width / 3, 0), CursorMode.Auto);
					_cursorType = CursorType.Hand;
				}
			}
		}
	}
}
