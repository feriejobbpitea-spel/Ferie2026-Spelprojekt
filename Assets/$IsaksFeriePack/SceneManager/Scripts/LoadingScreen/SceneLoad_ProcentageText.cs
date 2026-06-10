using System;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class SceneLoad_ProcentageText : MonoBehaviour
{
    private TMP_Text text;

    private void Awake()
    {
        text = GetComponent<TMP_Text>();
    }

    private void OnEnable()
    {
        OnSceneProgressChanged(0);
        SceneLoader.OnSceneProgressChanged += OnSceneProgressChanged;
    }

    private void OnDisable()
    {
        SceneLoader.OnSceneProgressChanged -= OnSceneProgressChanged;
    }

    private void OnSceneProgressChanged(int procentage)
    {
        text.text = $"{procentage}%";
    }
}
