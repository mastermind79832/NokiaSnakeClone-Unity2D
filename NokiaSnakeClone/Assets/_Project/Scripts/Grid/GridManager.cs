using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NokiaSnakeGame.Grid
{
    public class GridManager : MonoBehaviour
    {
		[Header("Grid Properties")]
		[SerializeField]
        private Vector2Int m_MaxGridSize;
		public Vector2Int GridSize { get { return m_MaxGridSize; }	}

		[Header("Tile Properties")]
		public TileTypeSO tileType;
		private TileController[,] m_Tiles;

		[Header("Wall Properties")]
		public WallTypeSO wallType;

		private void Start()
		{
			m_Tiles = new TileController[m_MaxGridSize.x, m_MaxGridSize.y];
			CreateGrid();
		}

		private void CreateGrid()
		{
			Vector2Int CurrentIndex = Vector2Int.zero;
			for (int x = 0; x < m_MaxGridSize.x; x++)
			{
				CurrentIndex.x = x;
				for (int y = 0; y < m_MaxGridSize.y; y++)
				{
					CurrentIndex.y = y;
					CreateSingleTile(CurrentIndex);
					CreateWall(CurrentIndex);
				}
			}
		}

		private void CreateWall(Vector2Int index)
		{
			Vector3 newPosition = Vector3.zero;
			newPosition.y = wallType.wallYOffset;
			Quaternion rotation;
			if (index.x == 0 || index.x == m_MaxGridSize.x - 1)
			{
				// To place wall to the edge of X axis
				newPosition.x = (index.x * tileType.tileSize) + tileType.tileSize / 2 * (index.x == 0 ? -1 : 1);
				newPosition.z = index.y * tileType.tileSize;
				// Rotate to make sure the normal faces inwards. Used for bounce 
				rotation = Quaternion.Euler(0, (index.x == 0 ? 0 : 180), 90);
				CreateSingleWall(newPosition, rotation);
			}

			if (index.y == 0 || index.y == m_MaxGridSize.y - 1)
			{
				newPosition.x = index.x * tileType.tileSize;
				// To place wall to the edge of Z axis
				newPosition.z = (index.y * tileType.tileSize) + tileType.tileSize / 2 * (index.y == 0 ? -1 : 1);
				// Rotate to make sure the normal faces inwards. Used for bounce 
				rotation = Quaternion.Euler(0, (index.y == 0 ? -90 : 90), 90);
				CreateSingleWall(newPosition, rotation);
			}
		}

		private void CreateSingleWall(Vector3 newPosition, Quaternion rotation)
		{
			Instantiate(wallType.wallPrefab, newPosition, rotation, transform);
		}

		private void CreateSingleTile(Vector2Int newIndex)
		{
			Vector3 newposition = Vector3.zero;
			newposition.x = newIndex.x * tileType.tileSize;
			newposition.z = newIndex.y * tileType.tileSize;

			m_Tiles[newIndex.x, newIndex.y] = Instantiate(tileType.tilePrefab, newposition, Quaternion.identity, transform);
		}
		public bool CheckTileState(TileState state, Vector2Int index) => m_Tiles[index.x, index.y].State == state;
		public Vector3 GetTilePosition(Vector2Int index) => m_Tiles[index.x, index.y].transform.position;
	}
}
