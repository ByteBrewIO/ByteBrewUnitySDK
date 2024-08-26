using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ByteBrewSDK;

public class ByteBrewExampleManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ByteBrew.InitializeByteBrew();

        ByteBrew.RemoteConfigsUpdated(() => {
            Debug.Log(ByteBrew.GetRemoteConfigForKey("item", "default value"));
        });

        ByteBrew.NewCustomEvent("item");
    }

    public void IAP()
    {
        ByteBrew.TrackInAppPurchaseEvent("Apple App Store", "USD", 5.99f, "currencyPack01", "Currencies");

        string iosReciept = "...";
        ByteBrew.TrackiOSInAppPurchaseEvent("Apple App Store", "USD", 5.99f, "currencyPack01", "Currencies", iosReciept);

        string googleReciept = "...";
        string googleSignature = "...";
        ByteBrew.TrackGoogleInAppPurchaseEvent("Google Play Store", "USD", 5.99f, "currencyPack01", "Currencies", googleReciept, googleSignature);
    }
}
