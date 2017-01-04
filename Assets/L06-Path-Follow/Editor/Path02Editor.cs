using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Wirune.L06
{
    [CustomEditor(typeof(Path02))]
    public class Path02Editor : Editor 
    {
        private void OnSceneGUI()
        {
            Path02 path = target as Path02;

            Handles.color = Color.magenta;

            int count = path.Count;
            for (int i = 0; i < count; i++)
            {
                Vector2 point = path.GetPoint(i);

                EditorGUI.BeginChangeCheck();
                point = Handles.FreeMoveHandle(point, Quaternion.identity, 0.5f, Vector3.one, Handles.CubeCap);

                if (EditorGUI.EndChangeCheck())
                {
                    Undo.RecordObject(target, "Move Point");
                    path.SetPoint(i, point);
                }
            }
        }
    }

}