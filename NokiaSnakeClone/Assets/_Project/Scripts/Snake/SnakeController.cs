using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NokiaSnakeGame.InputContoller;
using NokiaSnakeGame.Core;
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

		public float hitTimeSec;
		private WaitForSeconds hitTime;
		private bool isHit;

		private void Start()
		{
			hitTime = new WaitForSeconds(hitTimeSec);
		}

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

		private void FixedUpdate()
		{
			if (isHit)
				return;

			MoveSnake();
			if (direction != Vector3.zero)
				RotateSnake();
		}

		private void RotateSnake()
		{
			rb.MoveRotation(Quaternion.Euler(0, GetEffectiveRotation(), 0));
		}

		private float GetEffectiveRotation() => transform.eulerAngles.y - (rotationSpeed * Time.fixedDeltaTime * rotation) ;

		public void MoveSnake()
		{
			rb.velocity = GetEffectiveSpeed();
		}

		private Vector3 GetEffectiveSpeed() => snakeSpeed * Time.fixedDeltaTime * transform.forward;

		private void OnTriggerEnter(Collider other)
		{
			if (other.TryGetComponent(out IFloor floor))
				floor.ActivateFloor();
	

			if(other.TryGetComponent(out IWall wall))
			{
				StartCoroutine(WallHit());
				wall.HitWall(rb);	
			}
		}

		private IEnumerator WallHit()
		{
			isHit = true;
			yield return hitTime;
			isHit = false;
		}
	}
}