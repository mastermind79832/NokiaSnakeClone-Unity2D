using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NokiaSnakeGame.InputContoller;
using System;

namespace NokiaSnakeGame.Snake
{
	public class SnakeController : MonoBehaviour
	{
		[SerializeField]
		private Rigidbody rb;

		[Header("Snake Movement")]
		[SerializeField, Tooltip("The speed at which the snake moves forward")]
		private float snakeSpeed;
		[SerializeField, Tooltip("The speed at which the snake rotates")]
		private float rotationSpeed;

		private Vector3 direction;
		private float rotation;

		private void OnEnable()
		{
			InputController.Instance.OnJoyDrag += SetDirection;
		}
		private void OnDisable()
		{
			InputController.Instance.OnJoyDrag -= SetDirection;
		}

		private void SetDirection(Vector3 dir) 
		{
			direction = dir;
			rotation = Mathf.Abs(Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg) - 90;
		}

		private void Update()
		{
			CaluculateRotation();
		}

		private void CaluculateRotation()
		{
			//rotation = transform.localRotation.y + (rotationSpeed * Time.fixedDeltaTime * rotation * 50);
		}

		private void FixedUpdate()
		{
			MoveSnake();
			if (direction != Vector3.zero)
				RotateSnake();
		}

		private void RotateSnake()
		{
			rb.MoveRotation(Quaternion.Euler(0, GetEffectiveRotation(), 0));
		}

		private float GetEffectiveRotation()
		{
			return transform.eulerAngles.y - (rotationSpeed * Time.fixedDeltaTime * rotation) ;
		}

		public void MoveSnake()
		{
			rb.MovePosition(GetEffectiveSpeed());
		}

		private Vector3 GetEffectiveSpeed()
		{
			return rb.position + snakeSpeed * Time.fixedDeltaTime * transform.forward;
		}
	}
}