using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class AteyGenerator : MonoBehaviour
{
    [SerializeField] private Vector2 m_Size;
    [SerializeField] private GameObject m_Prefab;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        bool isCharacter = collision.TryGetComponent();
    }
}
