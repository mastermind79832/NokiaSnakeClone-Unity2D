using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NokiaSnakeGame.Food
{
    [CreateAssetMenu(menuName = "Food/New Food", fileName = "NewFoodSO")]
    public class FoodTypeSO : ScriptableObject
    {
        public FoodController foodPrefab;
        public float yOffset;
    }
    
}
