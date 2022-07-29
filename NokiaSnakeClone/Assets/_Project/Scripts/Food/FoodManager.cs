using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NokiaSnakeGame.Grid;

namespace NokiaSnakeGame.Food
{
    public class FoodManager : MonoBehaviour
    {
        public GridManager gridManager;

		[Header("Food Properties")]
		[SerializeField]
		public FoodTypeSO foodType;
        [SerializeField]
        private float m_SpawnInterval;

		private GenericPool<FoodController> foodPool = new GenericPool<FoodController>();
        private float m_Timer = 0;
        
		private void Update()
		{
            if (m_Timer >= m_SpawnInterval)
			{
                CreateFood();
				ResetTimer();
			}
			IncreaseTimer();
		}
        private void ResetTimer() => m_Timer = 0;
        private void IncreaseTimer() => m_Timer += Time.deltaTime;

		private void CreateFood()
		{
			Vector3 newPosition = gridManager.GetTilePosition(GetRandomPosition());
			newPosition.y += foodType.yOffset;
			SpawnFood(newPosition);
		}

		private void SpawnFood(Vector3 newPosition)
		{
			FoodController newFood;
			if (!foodPool.IsEmpty())
			{
				newFood = foodPool.GetItem();
				newFood.transform.position = newPosition;
				newFood.gameObject.SetActive(true);
			}
			else
			{
				newFood = Instantiate(foodType.foodPrefab, newPosition, Quaternion.identity, transform);
				newFood.OnDisableGameObject += BackToPool; 
			}
		}

		private Vector2Int GetRandomPosition()
		{
			Vector2Int randomIndex = Vector2Int.zero;
			do
			{
				randomIndex.x = Random.Range(0, gridManager.GridSize.x - 1);
				randomIndex.y = Random.Range(0, gridManager.GridSize.y - 1);

			} while (!gridManager.CheckTileState(TileState.Deactive, randomIndex));
			return randomIndex;
		}

		public void BackToPool(FoodController food)
		{
			foodPool.AddToPool(food);
		}
	}
}
