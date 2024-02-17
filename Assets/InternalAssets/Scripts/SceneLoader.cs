using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private List<Loader> m_Loader;
    [SerializeField] private GameObject m_CurrentScene;

    private void Start()
    {
        LinkScene();
        ResetScenes();
    }

    private void LinkScene()
    {
        if (m_Loader == null && m_Loader.Count == 0) return;
        m_Loader.ForEach((a) =>
        {
            a.Button.onClick.AddListener(() => m_CurrentScene.SetActive(false));
            a.Button.onClick.AddListener(() => OnClick(a.Scene));
        });
    }
    private void ResetScenes()
    {
        m_Loader.ForEach(a => { a.Scene.SetActive(false); });
        m_CurrentScene.SetActive(true);
    }

    private void OnClick(GameObject @object)
    {
        @object.SetActive(true);
        m_CurrentScene = @object;
    }
}

[Serializable]
public struct Loader
{
    public GameObject Scene;
    public Button Button;
}

