using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NokiaSnakeGame.Grid
{
	[CreateAssetMenu(menuName = "Grid/New Tile", fileName = "NewTileSO")]
    public class TileTypeSO : ScriptableObject
    {
        public float tileSize;
        public TileController tilePrefab;
    }
}
