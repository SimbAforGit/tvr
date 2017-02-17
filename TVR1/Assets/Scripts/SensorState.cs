using UnityEngine;
using System.Collections;

public enum TvrConnectionState
{
	Distannected,
	Scanning,
	Connecting,
	Connected,
	Error,
};

public class SensorState : MonoBehaviour {

	#region --- Controller0's state

	internal int controllerid = 0;
	internal TvrConnectionState connectionState = TvrConnectionState.Distannected;
	internal string errorDetails = "";

	internal Quaternion orientation = Quaternion.identity;
	internal Vector3 gyro = Vector3.zero;
	internal Vector3 accel = Vector3.zero;

	internal bool isTouching = false;
	internal Vector2 touchPos = Vector2.zero;
	internal bool touchDown = false;
	internal bool touchUp = false;

	internal bool recentering = false;
	internal bool recentered = false;

	internal bool clickButtonState = false;
	internal bool clickButtonDown = false;
	internal bool clickButtonUp = false;

	internal bool appButtonState = false;
	internal bool appButtonDown = false;
	internal bool appButtonUp = false;

	#endregion

	#region --- Controller1's state

	internal int controllerid1 = 1;
	internal TvrConnectionState connectionState1 = TvrConnectionState.Distannected;
	internal string errorDetails1 = "";

	internal Quaternion orientation1 = Quaternion.identity;
	internal Vector3 gyro1 = Vector3.zero;
	internal Vector3 accel1 = Vector3.zero;

	internal bool isTouching1 = false;
	internal Vector2 touchPos1 = Vector2.zero;
	internal bool touchDown1 = false;
	internal bool touchUp1 = false;

	internal bool recentering1 = false;
	internal bool recentered1 = false;

	internal bool clickButtonState1 = false;
	internal bool clickButtonDown1 = false;
	internal bool clickButtonUp1 = false;

	internal bool appButtonState1 = false;
	internal bool appButtonDown1 = false;
	internal bool appButtonUp1 = false;

	#endregion --- Controller1's state

	#region --- Head's state

	internal Quaternion head_orientation = Quaternion.identity;
	internal Vector3 head_position = Vector3.zero;
	internal Vector3 head_gyro = Vector3.zero;
	internal Vector3 head_accel = Vector3.zero;

	#endregion --- Head's state

	private static SensorState instance;

	public static TvrConnectionState State {
		get {
			return instance != null ? instance.connectionState : TvrConnectionState.Distannected;
		}
	}

	public static Quaternion Orientation {
		get {
			return instance != null ? instance.orientation : Quaternion.identity;
		}
	}

	public static Vector3 Gyro {
		get {
			return instance != null ? instance.gyro : Vector3.zero;
		}
	}

	public static Vector3 Accel {
		get {
			return instance != null ? instance.accel : Vector3.zero;
		}
	}

	public static bool IsTouching {
		get {
			return instance != null ? instance.isTouching : false;
		}
	}

	public static bool TouchDown {
		get {
			return instance != null ? instance.touchDown : false;
		}
		set { 
			instance.touchDown = value;
		}
	}

	public static bool TouchUp {
		get {
			return instance != null ? instance.touchUp : false;
		}
	}

	public static Vector2 TouchPos {
		get {
			return instance != null ? instance.touchPos : Vector2.zero;
		}
	}

	public static bool Recentering {
		get {
			return instance != null ? instance.recentering : false;
		}
	}

	public static bool Recentered {
		get {
			return instance != null ? instance.recentered : false;
		}
	}

	public static bool ClickButton {
		get {
			return instance != null ? instance.clickButtonState : false;
		}
	}

	public static bool ClickButtonDown {
		get {
			return instance != null ? instance.clickButtonDown : false;
		}
	}

	public static bool ClickButtonUp {
		get {
			return instance != null ? instance.clickButtonUp : false;
		}
	}

	public static bool AppButton {
		get {
			return instance != null ? instance.appButtonState : false;
		}
	}

	public static bool AppButtonDown {
		get {
			return instance != null ? instance.appButtonDown : false;
		}
	}

	public static bool AppButtonUp {
		get {
			return instance != null ? instance.appButtonUp : false;
		}
	}

	public static TvrConnectionState State1 {
		get {
			return instance != null ? instance.connectionState1 : TvrConnectionState.Distannected;
		}
	}

	public static Quaternion Orientation1 {
		get {
			return instance != null ? instance.orientation1 : Quaternion.identity;
		}
	}

	public static Vector3 Gyro1 {
		get {
			return instance != null ? instance.gyro1 : Vector3.zero;
		}
	}

	public static Vector3 Accel1 {
		get {
			return instance != null ? instance.accel1 : Vector3.zero;
		}
	}

	public static bool IsTouching1 {
		get {
			return instance != null ? instance.isTouching1 : false;
		}
	}

	public static bool TouchDown1 {
		get {
			return instance != null ? instance.touchDown1 : false;
		}
		set { 
			instance.touchDown = value;
		}
	}

	public static bool TouchUp1 {
		get {
			return instance != null ? instance.touchUp1 : false;
		}
	}

	public static Vector2 TouchPos1 {
		get {
			return instance != null ? instance.touchPos1 : Vector2.zero;
		}
	}

	public static bool Recentering1 {
		get {
			return instance != null ? instance.recentering1 : false;
		}
	}

	public static bool Recentered1 {
		get {
			return instance != null ? instance.recentered1 : false;
		}
	}

	public static bool ClickButton1 {
		get {
			return instance != null ? instance.clickButtonState1 : false;
		}
	}

	public static bool ClickButtonDown1 {
		get {
			return instance != null ? instance.clickButtonDown1 : false;
		}
	}

	public static bool ClickButtonUp1 {
		get {
			return instance != null ? instance.clickButtonUp1 : false;
		}
	}

	public static bool AppButton1 {
		get {
			return instance != null ? instance.appButtonState1 : false;
		}
	}

	public static bool AppButtonDown1 {
		get {
			return instance != null ? instance.appButtonDown1 : false;
		}
	}

	public static bool AppButtonUp1 {
		get {
			return instance != null ? instance.appButtonUp1 : false;
		}
	}

	public static Quaternion Head_Quaternion {
		get {
			return instance != null ? instance.head_orientation : Quaternion.identity;
		}
	}

	void Awake ()
	{
		if (instance != null) {
			this.enabled = false;
			return;
		}
		instance = this;
	}

	// Use this for initialization
	void Start ()
	{
		SensorProvider.addObserverListener (updateSensorState);
		/*
		List<CONNECT_STATE_EVENT_DATA> clientConnectedList = InputModule.getAllConnectedController ();
		foreach (var state in clientConnectedList) {
			updateController (state);
		}
		InputModule.addObserverListener (updateController);
		*/
	}

	// Update is called once per frame
	void Update ()
	{

	}

	void OnDestroy ()
	{
		instance = null;
	}

	private void updateSensorState(Pose3D pose3d){
		instance.head_orientation = pose3d.Orientation;
	}
}
