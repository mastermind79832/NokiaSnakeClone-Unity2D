using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NokiaSnakeGame
{
    [CreateAssetMenu(menuName = "Grid/New Wall", fileName = "NewWallSO")]
    public class WallTypeSO : ScriptableObject
    {
        public float wallYOffset;
        public WallController wallPrefab;
    }
}
