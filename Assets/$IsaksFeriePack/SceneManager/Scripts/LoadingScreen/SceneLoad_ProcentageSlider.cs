using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class SceneLoad_ProcentageSlider : MonoBehaviour
{
    private Slider slider;

    private void Awake()
    {
        slider = GetComponent<Slider>();
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
        slider.minValue = 0;
        slider.maxValue = 1;
        slider.value = procentage / 100f;
    }
}
