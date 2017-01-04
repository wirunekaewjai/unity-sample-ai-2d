using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Wirune.L06
{
    [CustomEditor(typeof(Path01))]
    public class Path01Editor : Editor 
    {
        private void OnSceneGUI()
        {
            Path01 path = target as Path01;

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