using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Bunch : MonoBehaviour
{
    [SerializeField] private UnityEvent<bool> m_OnEvent;

    private void OnEnable()
    {
        m_OnEvent.Invoke(true);
    }

    private void OnDisable()
    {
        m_OnEvent.Invoke(false);
    }
}
