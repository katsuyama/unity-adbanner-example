using UnityEngine;
using System.Collections;

public class AdBannerObserver : MonoBehaviour {
    public enum LayoutGravity {
		NO_GRAVITY = 0,
		CENTER_HORIZONTAL = 1,
		LEFT = 3,
		RIGHT = 5,
		FILL_HORIZONTAL = 7,
		CENTER_VERTICAL = 16,
		CENTER = 17,
		TOP = 48,
		BOTTOM = 80,
		FILL_VERTICAL = 112
    };

    private static AdBannerObserver sInstance;
    
    public static void Initialize() {
        Initialize(null, null, 0.0f, (int)LayoutGravity.BOTTOM);
    }
    
    public static void Initialize(string publisherId, string testDeviceId, float refresh, int layoutGravity) {
        if (sInstance == null) {
            // Make a game object for observing.
            GameObject go = new GameObject("_AdBannerObserver");
            go.hideFlags = HideFlags.HideAndDontSave;
            DontDestroyOnLoad(go);
            // Add and initialize this component.
            sInstance = go.AddComponent<AdBannerObserver>();
            sInstance.mAdMobPublisherId = publisherId;
            sInstance.mAdMobTestDeviceId = testDeviceId;
            sInstance.mRefreshTime = refresh;
			sInstance.mLayoutGravity = layoutGravity;
        }
    }
    
    public string mAdMobPublisherId;
    public string mAdMobTestDeviceId;
    public float mRefreshTime;
	public int mLayoutGravity;
    
    IEnumerator Start () {
#if UNITY_IPHONE
        ADBannerView banner = new ADBannerView();
        banner.autoSize = true;
        banner.autoPosition = ADPosition.Bottom;
        
        while (true) {
            if (banner.error != null) {
                Debug.Log("Error: " + banner.error.description);
                break;
            } else if (banner.loaded) {
                banner.Show();
                break;
            }
            yield return null;
        }
#elif UNITY_ANDROID && !UNITY_EDITOR
        AndroidJavaClass plugin = new AndroidJavaClass("jp.radiumsoftware.unityplugin.admob.AdBannerController");
        AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
        while (true) {
            plugin.CallStatic("tryCreateBanner", activity, mAdMobPublisherId, mAdMobTestDeviceId, mLayoutGravity);
            yield return new WaitForSeconds(Mathf.Max(30.0f, mRefreshTime));
        }
#else
        return null;
#endif
    }
}
