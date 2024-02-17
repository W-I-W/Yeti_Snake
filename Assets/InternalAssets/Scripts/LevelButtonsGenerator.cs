using Bow.UI;

using JetBrains.Annotations;

using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;


namespace Bow
{
    public class LevelButtonsGenerator : MonoBehaviour
    {
        [SerializeField] private List<GameObject> m_Levels;
        [SerializeField] private Button m_PrefabButton;
        [SerializeField] private Transform m_GroupHeader;
        [SerializeField] private Transform m_GroupOther;
        [SerializeField] private UnityEvent m_OnClick;

        private int m_level = 0;

        public void Start()
        {
            int maxHeader = m_Levels.Count % 3;
            int maxOther = m_Levels.Count - maxHeader;

            GeneratorButton(m_GroupOther, maxOther);
            GeneratorButton(m_GroupHeader, maxHeader);
        }

        private void GeneratorButton(Transform parent, int max)
        {

            for (int i = 0; i < max; i++)
            {
                m_level++;
                Button button = Instantiate(m_PrefabButton, parent);
                button.OnClick(m_Levels[i], m_level);
                button.OnEvent(m_OnClick);
            };
        }
    }
}
