using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace JIANING
{
    public enum GameLanguage
    {
        Chinese = 0,//中文
        English = 1//英文
    }
    public class LocalizationComponent : JIANINGBaseComponent
    {
        /// <summary>
        /// 当前语言 和表里面的字段一致
        /// </summary>
        public GameLanguage CurrLanguage
        {
            get;
            private set;
        }

        protected override void OnAwake()
        {
            base.OnAwake();
        }

        protected override void OnStart()
        {
            base.OnStart();
        }

        private void Init()
        {
            switch (Application.systemLanguage)
            {
                default:
                    CurrLanguage = GameLanguage.Chinese;
                    break;
                case SystemLanguage.Chinese:
                case SystemLanguage.ChineseSimplified:
                    CurrLanguage = GameLanguage.Chinese;
                    ; break;
                case SystemLanguage.English:
                    CurrLanguage = GameLanguage.English;
                    ; break;

            }
        }


        public string GetString(string str)
        {
            return str;
        }
        public override void Shutdown()
        {
        }
    }
}