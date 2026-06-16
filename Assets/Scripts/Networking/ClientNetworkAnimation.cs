using Unity.Netcode.Components;
using UnityEngine;

public class ClientNetworkAnimation : NetworkAnimator
{
    protected override bool OnIsServerAuthoritative()
    {
        return false;
    }
}
