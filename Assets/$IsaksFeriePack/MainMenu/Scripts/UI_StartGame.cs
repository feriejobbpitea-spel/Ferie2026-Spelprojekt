using System;
using UnityEngine;

[RequireComponent(typeof(ButtonWrapper))]
public class UI_StartGame : MonoBehaviour
{
    [SerializeField] private string SceneToLoad;
    private ButtonWrapper button;

    private void Awake()
    {
        button = GetComponent<ButtonWrapper>();
    }
    private void OnEnable()
    {
        button.onClick.AddListener(StartGame);
    }

    private void OnDisable()
    {
        button.onClick.RemoveListener(StartGame);
    }

    private void StartGame()
    {
        SceneLoader.LoadScene(SceneToLoad);
    }
}
