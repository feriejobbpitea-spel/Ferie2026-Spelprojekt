using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SceneLoader_Example : MonoBehaviour
{
    [SerializeField] private Button Button;
    [SerializeField] private string SceneToLoad;

    private void OnEnable()
    {
        Button.onClick.AddListener(OnButtonClicked);
    }

    private void OnDisable()
    {
        Button.onClick.RemoveListener(OnButtonClicked);
    }

    private void OnButtonClicked()
    {
        SceneLoader.LoadScene(SceneToLoad);
    }
}
