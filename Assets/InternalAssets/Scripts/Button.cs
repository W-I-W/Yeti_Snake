using System;
using System.Collections;
using System.Collections.Generic;

using TMPro;

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.UIElements;

namespace Bow.UI
{
    public class Button : MonoBehaviour
    {
        [SerializeField] private UnityEngine.UI.Button m_Button;
        [SerializeField] private TextMeshProUGUI m_Text;
        [SerializeField] private GameObject m_CurrentScene;

        public void SetText(string text) => m_Text.text = text;

        public void OnClick(GameObject @object, int level)
        {
            m_Text.text = level.ToString();
            m_Button.onClick.AddListener(() => @object.SetActive(true));
        }

        public void OnEvent(UnityEvent action)=>
            m_Button.onClick.AddListener(() => action.Invoke());
    }
}
