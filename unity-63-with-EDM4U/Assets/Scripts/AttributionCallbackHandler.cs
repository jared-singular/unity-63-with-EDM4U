using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Singular;

public class AttributionCallbackHandler : MonoBehaviour, SingularDeviceAttributionCallbackHandler 
{
    void Awake()
    {
        Debug.Log("Singular === Registering SingularDeviceAttributionCallbackHandler");
        SingularSDK.SetSingularDeviceAttributionCallbackHandler(this);
    }

    // Note the generic types: Dictionary<string, object>
    public void OnSingularDeviceAttributionCallback(Dictionary<string, object> attributionInfo) 
    {
        if (attributionInfo == null)
        {
            Debug.Log("Singular === OnSingularDeviceAttributionCallback: attributionInfo is null");
            return;
        }

        foreach (KeyValuePair<string, object> kvp in attributionInfo)
        {
            Debug.Log($"Singular === OnSingularDeviceAttributionCallback Key: {kvp.Key}, Value: {kvp.Value}");
        }
    }
}
