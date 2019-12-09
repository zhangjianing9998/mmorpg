using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace JIANING
{
    public class UIComponent : JIANINGBaseComponent, IUpdataComponent
    {
        protected override void OnAwake()
        {
            base.OnAwake();
            GameEntry.RegisterUpdateComponent(this);
        }

      

        public void OnUpdate()
        {

        }

        public override void Shutdown()
        {

        }
    }
}