using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NokiaSnakeGame.Core;

namespace NokiaSnakeGame.Grid
{
    public enum TileState
	{
        Deactive = 0,
        Active = 1
	}

    public class TileController : MonoBehaviour, IFloor
    {
        public Vector2Int tileLocation;

        private TileState tileType;
		[SerializeField, Tooltip("Material is assigned using TileType enum value. Add Accordingly.")]
        private Material[] tileMats;
		
        [SerializeField]
        private MeshRenderer meshRenderer;

		public void ActivateFloor()
		{
            if(tileType == TileState.Deactive)
			{
                tileType = TileState.Active;
                SetTileMaterial();
			}
        }

		private void SetTileMaterial()
		{
			meshRenderer.sharedMaterial = tileMats[(int)tileType];
		}
	}
}
