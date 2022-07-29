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
        private TileState m_State;
		public TileState State {  get { return m_State; } }

		[SerializeField, Tooltip("Material is assigned using TileType enum value. Add Accordingly.")]
        private Material[] m_TileMats;
		
        [SerializeField]
        private MeshRenderer m_MeshRenderer;

		public void ActivateFloor()
		{
            if(m_State == TileState.Deactive)
			{
                m_State = TileState.Active;
                SetTileMaterial();
			}
        }


		private void SetTileMaterial()
		{
			m_MeshRenderer.sharedMaterial = m_TileMats[(int)m_State];
		}
	}
}
