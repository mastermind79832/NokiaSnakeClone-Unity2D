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
		private Rigidbody m_RigidBody;

		[Header("Snake Movement")]
		[SerializeField, Tooltip("The speed at which the snake moves forward")]
		private float m_SnakeSpeed;
		[SerializeField, Tooltip("The speed at which the snake rotates")]
		private float m_RotationSpeed;

		private Vector3 m_Direction;
		private float m_Rotation;

		public float hitTimeSec;
		private WaitForSeconds m_HitTime;
		private bool b_IsHit;
		private Vector3 m_Initalpos;

		private void Start()
		{
			m_HitTime = new WaitForSeconds(hitTimeSec);
			m_Initalpos = transform.position;
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
			m_Direction = dir;
			m_Rotation = Mathf.Abs(Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg) - 90;
		}

		private void FixedUpdate()
		{
			if (b_IsHit)
				return;

			MoveSnake();
			if (m_Direction != Vector3.zero)
				RotateSnake();
			FixSnakeToGround();
		}

		private void FixSnakeToGround()
		{
			m_Initalpos.x = transform.position.x;
			m_Initalpos.z = transform.position.z;
			// Make sure the Y axis stays same
			transform.position = m_Initalpos;
		}

		private void RotateSnake()
		{
			m_RigidBody.MoveRotation(Quaternion.Euler(0, GetEffectiveRotation(), 0));
		}

		private float GetEffectiveRotation() => transform.eulerAngles.y - (m_RotationSpeed * Time.fixedDeltaTime * m_Rotation) ;

		public void MoveSnake()
		{
			m_RigidBody.velocity = GetEffectiveSpeed();		
		}

		private Vector3 GetEffectiveSpeed() => m_SnakeSpeed * Time.fixedDeltaTime * transform.forward;

		private void OnTriggerEnter(Collider other)
		{
			if (other.TryGetComponent(out IFloor floor))
				floor.ActivateFloor();

			if(other.TryGetComponent(out IWall wall))
			{
				StartCoroutine(WallHit());
				wall.HitWall(m_RigidBody);	
			}

			if (other.TryGetComponent(out IConsumable consumable))
				consumable.Consume();
		}
		// For recovery time
		private IEnumerator WallHit()
		{
			b_IsHit = true;
			yield return m_HitTime;
			b_IsHit = false;
		}
	}
}