public class Config {
    //Whether to enable Time Warp
    public static bool TW_STATE = true;
    //Setting Screen sleepTimeout
    public static bool ScreenNeverSleep = true;
    //Whether to enable MojingSDK
    public static bool MojingSDKActive = true;

	// Enumerate Glasses
	public enum GlassesTypes
	{
		MojingII,
		MojingIII,
		MojingIIIPlusB,
		MojingIIIPlusA,
		MojingIV,
		MojingMovie,
		MojingYoungD,
		MojingV,
		MojingRIO,
		MojingS1,
	};

	public enum GUITypes
	{
		Gaze,
		Controller,
	}
}
