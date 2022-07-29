using System;
using System.Collections;
using UnityEngine;
using NokiaSnakeGame.Core;

namespace NokiaSnakeGame.Food
{
	public class FoodController : MonoBehaviour, IConsumable
	{
		[SerializeField]
		private int m_ScoreValue;
		[SerializeField]
		private float m_AliveTime;
		private Coroutine m_AliveRoutine;
		private WaitForSeconds m_AliveWait;

		public Action<FoodController> OnDisableGameObject;

		private void Start()
		{
			m_AliveWait = new WaitForSeconds(m_AliveTime);
		}
		public void Consume()
		{
			ScoreManager.Instance.UpdateScore(m_ScoreValue);
			gameObject.SetActive(false);
		}

		private void OnEnable()
		{
			m_AliveRoutine = StartCoroutine(DeathCountDown());
		}

		private IEnumerator DeathCountDown()
		{
			yield return m_AliveWait;
			gameObject.SetActive(false);
		}

		private void OnDisable()
		{
			if(m_AliveRoutine != null)
				StopCoroutine(m_AliveRoutine);
			OnDisableGameObject(this);
		}
	}
}
