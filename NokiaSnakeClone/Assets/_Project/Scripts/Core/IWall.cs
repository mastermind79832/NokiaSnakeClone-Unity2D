using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NokiaSnakeGame.Core
{
    public interface IWall
    {
        public void HitWall(Rigidbody rb);
    }
}
