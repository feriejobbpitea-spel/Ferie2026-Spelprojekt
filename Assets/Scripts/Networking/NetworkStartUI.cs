using Unity.Netcode;
using UnityEngine;

/// <summary>
/// Skapar en tillfällig UI för att hosta eller joina ett spel
/// </summary>
public class NetworkStartUI : MonoBehaviour
{
    private void OnGUI()
    {
        float w = 200f, h = 40f;
        float x = 10f, y = 10f;

        if(!NetworkManager.Singleton.IsClient && !NetworkManager.Singleton.IsServer)
        {
            if (GUI.Button(new Rect(x, y, w, h), "Start Client"))
                NetworkManager.Singleton.StartClient();

            if (GUI.Button(new Rect(x, y + h, w, h), "Start Host"))
                NetworkManager.Singleton.StartHost();
        }
    }
}
