using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
	[SerializeField]
	private RectTransform rectTransform;
	private float rotation;
	private bool isClicked;

	public Action<float> OnDrag;

	public void OnPointerDown(PointerEventData eventData)
	{
		isClicked = true;
	}

	private void Update()
	{
		if (isClicked)
		{
			OnPointerMove();
		}
	}

	public void OnPointerMove()
	{
		Vector3 direction = (Input.mousePosition - rectTransform.position).normalized;
		rotation = Mathf.Atan2(direction.y,direction.x) * Mathf.Rad2Deg - 90;
		SetRotationToJoy(rotation);
		OnDrag(rotation);
	}

	void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
	{
		isClicked = false;
		SetRotationToJoy(0);
	}

	private void SetRotationToJoy(float rotate)
	{
		rectTransform.rotation = Quaternion.Euler(0,0,rotate);
	}
}
