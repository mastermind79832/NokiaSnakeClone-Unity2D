using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NokiaSnakeGame.Core;

namespace NokiaSnakeGame.Grid
{
    public enum TileType
	{
        Deactive = 0,
        Active = 1
	}

    public class TileController : MonoBehaviour, IFloor
    {
        private Vector2Int tileLocation;

        private TileType tileType;
		[SerializeField, Tooltip("Material is assigned using TileType enum value. Add Accordingly.")]
        private Material[] tileMats;
		
        [SerializeField]
        private MeshRenderer meshRenderer;

		public void ActivateFloor()
		{
            if(tileType == TileType.Deactive)
			{
                tileType = TileType.Active;
                SetTileMaterial();
			}
        }

		private void SetTileMaterial()
		{
			meshRenderer.sharedMaterial = tileMats[(int)tileType];
		}
	}
}
