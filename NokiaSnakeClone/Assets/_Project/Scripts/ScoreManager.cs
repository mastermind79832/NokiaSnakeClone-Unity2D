using UnityEngine;
using NokiaSnakeGame.Core;
using TMPro;

namespace NokiaSnakeGame
{
    public class ScoreManager : MonoSingletonGeneric<ScoreManager>
    {
        private int m_Score;
		[SerializeField]
        private TextMeshProUGUI m_ScoreText;
		[SerializeField]
		private string m_ScoreTitle;

		private void Start()
		{
			m_Score = 0;
			UpdateScore(0);
		}

		public void UpdateScore(int value)
		{
			m_Score += value;
			m_ScoreText.text = $"{m_ScoreTitle} : {m_Score}";
		}
	}
}
