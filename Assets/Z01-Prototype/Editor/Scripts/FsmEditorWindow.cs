using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

using Wirune.Z01;

namespace WiruneEditor.Z01
{
    public sealed class FsmEditorWindow : EditorWindow
    {
        [MenuItem("W2Kit/Fsm Editor Window")]
        public static void RequestWindow()
        {
            var window = EditorWindow.GetWindow<FsmEditorWindow>();

            window.titleContent = new GUIContent("FSM Editor");
            window.Show();
        }

        private Texture2D m_BackgroundTexture;
        private Texture2D m_ShadowLeftTexture;
        private Texture2D m_ShadowTopTexture;

        private Vector2 m_Position = new Vector2();
        private Vector2 m_MousePosition;

        private float m_Width = 200;
        private float m_MouseDownWidth;
        private float m_MouseDownX;

        private bool m_Scrolling;
        private bool m_Resizing;

        private int m_SelectingEventNameIndex;
        private string m_SelectingEventName;

        private Fsm m_Fsm;

        private void OnEnable()
        {
            m_SelectingEventNameIndex = -1;

            string directory = "Z01-Prototype/Editor/Textures";

            m_BackgroundTexture = AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/" + directory + "/Grid.png");
            m_ShadowLeftTexture = AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/" + directory + "/ShadowLeft.png");
            m_ShadowTopTexture = AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/" + directory + "/ShadowTop.png");

            Selection.selectionChanged += OnSelectionChanged;
            OnSelectionChanged();
        }

        private void OnDisable()
        {
            Selection.selectionChanged -= OnSelectionChanged;
        }

        private void OnSelectionChanged()
        {
            GameObject obj = Selection.activeGameObject;

            if (null != obj)
            {
                m_Fsm = obj.GetComponent<Fsm>();
            }
            else
            {
                m_Fsm = null;
            }
        }

        private void OnGUI()
        {

            if (null == m_Fsm)
            {
                OnDrawNonEditableGUI();
            }
            else
            {
                OnDrawEditableGUI();
            }
        }

        private void OnDrawNonEditableGUI()
        {
            EditorGUILayout.BeginVertical(GUI.skin.box);
            EditorGUILayout.LabelField("Please Select Fsm Object");
            EditorGUILayout.EndVertical();
        }

        private void OnDrawEditableGUI()
        {
            GUILayout.BeginHorizontal(GUILayout.Height(position.height));

            DrawLeftPanel();
            DrawRightPanel();

            GUILayout.EndHorizontal();

            Resizing();
        }

        private void DrawLeftPanel()
        {
            GUILayout.BeginVertical(EditorStyles.toolbar, GUILayout.Width(m_Width));

            GUILayout.Label(m_Fsm.gameObject.name, EditorStyles.toolbarButton);


//            EditorGUILayout.Foldout(true, "Events");

            int eventNameCount = m_Fsm.EventNameCount;
            for (int i = 0; i < eventNameCount; i++)
            {
                string index = "[" + i.ToString() + "]";
                string eventName = m_Fsm.GetEventName(i);

                Rect hRect = EditorGUILayout.BeginHorizontal();

                GUILayout.Label(index, GUILayout.Width(48));

                hRect.width -= 48;
                hRect.x += 48;

                if (m_SelectingEventNameIndex == i)
                {
                    EventType type = Event.current.type;
                    KeyCode keyCode = Event.current.keyCode;
                    bool mouseContains = hRect.Contains(Event.current.mousePosition);

                    if ((type == EventType.KeyDown && (keyCode == KeyCode.Return || keyCode == KeyCode.KeypadEnter)) ||
                        (type == EventType.MouseDown && !mouseContains))
                    {
                        if (!m_Fsm.ContainsEventName(m_SelectingEventName))
                        {
                            m_Fsm.SetEventName(i, m_SelectingEventName);
                        }

                        m_SelectingEventNameIndex = -1;
                        m_SelectingEventName = "";
                    }

                    m_SelectingEventName = EditorGUILayout.TextField(m_SelectingEventName);


                }
                else if (GUILayout.Button(eventName, EditorStyles.label))
                {
                    m_SelectingEventNameIndex = i;
                    m_SelectingEventName = eventName;
                }


//                EditorGUI.BeginChangeCheck();
//                GUILayout.TextField(eventName);


                EditorGUILayout.EndHorizontal();
            }


            GUILayout.EndVertical();
        }

        private void DrawRightPanel()
        {
            var rect = EditorGUILayout.BeginVertical(GUILayout.ExpandHeight(true));

            DrawGridBackground(rect);
            Scrolling(rect);

            EditorGUILayout.EndVertical();
        }

        private void DrawGridBackground(Rect rect)
        {
            var coords = new Rect(0, 0, rect.width / m_BackgroundTexture.width, rect.height / m_BackgroundTexture.height);
            GUI.DrawTextureWithTexCoords(rect, m_BackgroundTexture, coords);

            var ls = rect;
            ls.width = m_ShadowLeftTexture.width * 2f;

            var ts = rect;
            ts.height = m_ShadowTopTexture.height * 2f;

            GUI.DrawTexture(ls, m_ShadowLeftTexture);
            GUI.DrawTexture(ts, m_ShadowTopTexture);
        }

        private void Scrolling(Rect rect)
        {
            if (Event.current.type == EventType.MouseDown &&
                rect.Contains(Event.current.mousePosition))
            {
                m_MousePosition = Event.current.mousePosition;

                if (Event.current.button == 1)
                {
                    GenericMenu menu = new GenericMenu();

                    menu.AddItem(new GUIContent("Create New State"), false, OnCreateNewState);
                    menu.AddItem(new GUIContent("Move View to Center"), false, () => m_Position = new Vector2());
                    menu.ShowAsContext();
                }
            }
            else if (Event.current.type == EventType.MouseDrag)
            {
                Vector2 current = Event.current.mousePosition;
                Vector2 delta = current - m_MousePosition;

                m_Position += delta;
                m_MousePosition = current;
            }
        }

        private void Resizing()
        {
            Rect cursor = new Rect(m_Width - 2, 0, 3, position.height);
            EditorGUIUtility.AddCursorRect(cursor, MouseCursor.ResizeHorizontal);

            if (Event.current.type == EventType.MouseDown &&
                cursor.Contains(Event.current.mousePosition))
            {
                m_Resizing = true;

                m_MouseDownWidth = m_Width;
                m_MouseDownX = Event.current.mousePosition.x;
            }
            else if (Event.current.type == EventType.MouseUp)
            {
                m_Resizing = false;
            }

            if (m_Resizing)
            {
                float delta = Event.current.mousePosition.x - m_MouseDownX;
                m_Width = Mathf.Clamp(m_MouseDownWidth + delta, 180, 360);

                Repaint();
            }
        }

        private void OnCreateNewState()
        {
            FsmState state = new FsmState();
            m_Fsm.AddState(state);
        }
    }

}