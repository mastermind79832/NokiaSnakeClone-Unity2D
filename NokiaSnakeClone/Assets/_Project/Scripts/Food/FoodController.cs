using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NokiaSnakeGame.Core;

namespace NokiaSnakeGame.Food
{
	public class FoodController : MonoBehaviour, IConsumable
	{
		[SerializeField]
		private int m_ScoreValue;

		public void Consume()
		{
			ScoreManager.Instance.UpdateScore(m_ScoreValue);
			Destroy(gameObject);
		}
	}
}
