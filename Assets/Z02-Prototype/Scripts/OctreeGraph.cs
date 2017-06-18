using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wirune.Z02
{
    public sealed class OctreeGraph : MonoBehaviour 
    {
        [Header("Debug")]
        [SerializeField]
        private bool m_Gizmos = true;

        [Header("Setting")]
        [SerializeField]
        private int m_GroupSize = 10;

        private readonly List<List<OctreeNode>> m_Groups = new List<List<OctreeNode>>();

        private void OnDrawGizmos()
        {
            if (!m_Gizmos)
                return;

//            Gizmos.color = new Color(0, 1, 0, 0.1f);
//            Gizmos.DrawCube(transform.position, Vector3.one * m_GroupSize);
//
//            Gizmos.color = Color.green;
//            Gizmos.DrawWireCube(transform.position, Vector3.one * m_GroupSize);
        }

        public void InsertOrUpdateNode(OctreeNode node, Vector3 position, Vector3 size)
        {
            
        }

        private static OctreeGraph m_Instance;
        public static OctreeGraph Instance
        {
            get 
            {
                if (null == m_Instance)
                {
                    m_Instance = FindObjectOfType<OctreeGraph>();
                }

                if (null == m_Instance)
                {
                    var gameObject = new GameObject("Octree Graph");

                    m_Instance = gameObject.AddComponent<OctreeGraph>();
                    m_Instance.transform.SetAsFirstSibling();
                }

                return m_Instance;
            }
        }
    }

}