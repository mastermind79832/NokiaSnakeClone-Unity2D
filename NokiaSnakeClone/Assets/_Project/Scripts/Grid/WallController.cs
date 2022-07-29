using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NokiaSnakeGame.Core;

namespace NokiaSnakeGame
{
    public class WallController : MonoBehaviour, IWall
    {
		[SerializeField]
		private float m_BounceForce;
		public void HitWall(Rigidbody rb)
		{
			Vector3 reflectVelocity = Vector3.Reflect(rb.transform.forward, -transform.up) * m_BounceForce;
			reflectVelocity.y = 0;
			rb.velocity += reflectVelocity;
		}
    }
}
