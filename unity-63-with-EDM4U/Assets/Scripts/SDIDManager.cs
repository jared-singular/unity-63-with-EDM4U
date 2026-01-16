using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Singular;

public class SDIDManager : MonoBehaviour, SingularSdidAccessorHandler
{
    private const string SDID_KEY = "singular_sdid";

    void Awake()
    {
        Debug.Log("Singular === Registering SingularSdidAccessorHandler");
        SingularSDK.SetSingularSdidAccessorHandler(this);

        // Optional: try to read any previously stored SDID
        if (PlayerPrefs.HasKey(SDID_KEY))
        {
            var savedSdid = PlayerPrefs.GetString(SDID_KEY);
            Debug.Log($"Singular === Loaded saved SDID: {savedSdid}");
        }
    }

    public void SdidReceived(string sdid)
    {
        Debug.Log($"Singular === SDID Received: {sdid}");

        // Save to App Storage (PlayerPrefs on Unity, NSUserDefaults on iOS)
        PlayerPrefs.SetString(SDID_KEY, sdid);
        PlayerPrefs.Save(); // ensure it’s written

        // You can also call DidSetSdid logic here if desired
        DidSetSdid(sdid);
    }

    public void DidSetSdid(string sdid)
    {
        Debug.Log($"Singular === SDID Set: {sdid}");

        // Use the SDID that was stored (or passed in)
        // Example: compare with stored value, or just rely on the argument
        string stored = PlayerPrefs.GetString(SDID_KEY, string.Empty);
        Debug.Log($"Singular === Stored SDID (for verification): {stored}");
        
        // TODO: use sdid for your hybrid/S2S flow or other logic
    }
}
