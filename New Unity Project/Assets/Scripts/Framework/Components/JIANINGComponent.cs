using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JIANING
{
    /// <summary>
    /// framework base
    /// </summary>
    public class JIANINGComponent : MonoBehaviour
    {
        private void Awake()
        {
            OnAwake();
        }

        protected virtual void OnAwake() { }
    }

   
}
