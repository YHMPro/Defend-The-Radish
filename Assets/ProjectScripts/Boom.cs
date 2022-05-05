using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Farme.Extend;
namespace DTR
{
    public class Boom : MonoBehaviour
    {
        /// <summary>
        /// Boom动画结束自动回调
        /// </summary>
        [SerializeField]
        private void BoomCallback()
        {
            gameObject.Recycle(gameObject.name);
        }
    }
}
