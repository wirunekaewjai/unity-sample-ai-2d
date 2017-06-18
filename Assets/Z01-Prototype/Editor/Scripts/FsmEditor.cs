using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

using Wirune.Z01;

namespace WiruneEditor.Z01
{
    [CustomEditor(typeof(Fsm))]
    public sealed class FsmEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("Open Fsm Editor Window"))
            {
                FsmEditorWindow.RequestWindow();
            }
        }
    }

}