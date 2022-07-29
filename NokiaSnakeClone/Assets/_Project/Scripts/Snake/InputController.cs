using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using NokiaSnakeGame.Core;

namespace NokiaSnakeGame.InputContoller
{
	public class InputController : MonoSingletonGeneric<InputController>, IPointerDownHandler, IPointerUpHandler
	{
		[SerializeField]
		private RectTransform m_JoyHeadTransfrom;
	
		// is Mouse clicked
		private bool m_IsClicked;

		public Action<Vector3> OnJoyDrag;

		private void OnEnable()
		{
			OnJoyDrag += SetRotationToJoy;
		}
		private void OnDisable()
		{
			OnJoyDrag -= SetRotationToJoy;
		}

		public void OnPointerDown(PointerEventData eventData)
		{
			m_IsClicked = true;
		}

		private void Update()
		{
			if (m_IsClicked)
				OnPointerMove();
		}

		public void OnPointerMove()
		{
			OnJoyDrag(CaculateDirection());
		}

		private Vector3 CaculateDirection()
		{
			return (Input.mousePosition - m_JoyHeadTransfrom.position).normalized;
		}

		void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
		{
			m_IsClicked = false;
			OnJoyDrag(Vector3.zero);
		}

		private void SetRotationToJoy(Vector3 direction)
		{
			float rotation = (direction != Vector3.zero)? Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90 : 0;
			//Debug.Log(rotation);
			m_JoyHeadTransfrom.rotation = Quaternion.Euler(0, 0, rotation);
		}
	}
}