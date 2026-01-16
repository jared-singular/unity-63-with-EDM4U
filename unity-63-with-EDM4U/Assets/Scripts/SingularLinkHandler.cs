using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Singular;

public class DeepLinkManager : MonoBehaviour, SingularLinkHandler
{
    void Awake()
    {
        Debug.Log("Singular === Registering Singular Link Handler");

        // Register this class as the Singular Link handler
        // This will fetch the tracking link details and call OnSingularLinkResolved
        SingularSDK.SetSingularLinkHandler(this);
    }

    // Callback method that receives deep link parameters
    public void OnSingularLinkResolved(SingularLinkParams linkParams)
    {
        Debug.Log("Singular === Singular Link Resolved");

        // Extract parameters from the tracking link
        string deeplink = linkParams.Deeplink;
        string passthrough = linkParams.Passthrough;
        bool isDeferred = linkParams.IsDeferred;

        // Log the parameters
        Debug.Log($"Singular === Deeplink: {deeplink ?? "null"}");
        Debug.Log($"Singular === Passthrough: {passthrough ?? "null"}");
        Debug.Log($"Singular === Is Deferred: {isDeferred}");
    }
}