using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace JIANING
{
    [CustomEditor(typeof(JIANINGText))]
    public class YouYouTextInspector : UnityEditor.UI.TextEditor
    {
        private SerializedProperty m_Localization;

        protected override void OnEnable()
        {
            base.OnEnable();
            m_Localization = serializedObject.FindProperty("m_Localization");
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            serializedObject.Update();
            EditorGUILayout.PropertyField(m_Localization);
            serializedObject.ApplyModifiedProperties();
        }
    }
}