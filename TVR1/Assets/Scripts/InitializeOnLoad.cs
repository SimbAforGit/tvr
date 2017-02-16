using UnityEngine;
using System.Collections;
using System.Runtime.Hosting;

public class InitializeOnLoad : MonoBehaviour {
	
	[RuntimeInitializeOnLoadMethod]
	static void Initialize()
	{
		Screen.orientation = ScreenOrientation.LandscapeLeft;
		if (Config.ScreenNeverSleep)
			Screen.sleepTimeout = SleepTimeout.NeverSleep;
		else
			Screen.sleepTimeout = SleepTimeout.SystemSetting;
	}
}
