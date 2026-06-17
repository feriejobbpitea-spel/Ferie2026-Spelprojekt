using Blocks.Sessions;
using Blocks.Sessions.Common;
using System;
using System.Collections;
using System.Threading.Tasks;
using Unity.Netcode;
using Unity.Properties;
using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// Hanterar UI att hosta eller joina ett spel
/// </summary>
public class NetworkStartUI : MonoBehaviour
{
    [SerializeField] private QuickJoinViewModel m_ViewModel;
    [SerializeField] private QuickJoinSettings QuickJoinSettings;
    [SerializeField] private SessionSettings m_SessionSettings;

    [SerializeField] private GameObject LoadingScreen;

    public SessionSettings SessionSettings
    {
        get => m_SessionSettings;
        set
        {
            if (m_SessionSettings == value)
            {
                return;
            }

            m_SessionSettings = value;
        }
    }

    [SerializeField] private ButtonWrapper quickJoinButton;
    
    private void Awake()
    {
        LoadingScreen.SetActive(false);

        m_ViewModel = new QuickJoinViewModel(m_SessionSettings?.sessionType);
        SessionSettings = m_SessionSettings;
    }

    private void OnEnable()
    {
        quickJoinButton.onClick.AddListener(OnQuickJoinClicked);
    }

    private void OnDisable()
    {
        quickJoinButton.onClick.RemoveListener(OnQuickJoinClicked);
    }

    private void Update()
    {
        quickJoinButton.interactable = m_ViewModel.CanClickButton;
    }

    private void OnQuickJoinClicked()
    {

        if (!SessionSettings)
        {
            Debug.LogError("SessionSettings is null, it needs to be assigned.");
            return;
        }
        if (!m_ViewModel.AreMultiplayerServicesInitialized())
        {
            Debug.LogError("Multiplayer Services are not initialized. You can initialize them with default settings by adding a ServicesInitialization and PlayerAuthentication components in your scene.");
            return;
        }

        Task joiningTask = m_ViewModel.MatchmakeSessionAsync(QuickJoinSettings.ToQuickJoinOptions(), m_SessionSettings.ToSessionOptions());
        StartCoroutine(JoiningGame(joiningTask));
    }

    private IEnumerator JoiningGame(Task joiningTask) 
    {
        while(!joiningTask.IsCompleted)
        {
            LoadingScreen.SetActive(true);
            yield return null;
        }
        LoadingScreen.SetActive(false);
    }
}
