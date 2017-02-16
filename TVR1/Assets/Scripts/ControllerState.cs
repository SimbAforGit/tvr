using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum TvrConnectionState
{
	Distannected,
	Scanning,
	Connecting,
	Connected,
	Error,
};

public class ControllerState : MonoBehaviour
{
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

	#endregion --- Controller0's state

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

	private static ControllerState instance;

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

	void Awake ()
	{
		if (instance != null) {
			Debug.LogError ("More than one ControllerState instance was found in your scene. "
			+ "Ensure that there is only one ControllerState.");
			this.enabled = false;
			return;
		}
		instance = this;
	}

	// Use this for initialization
	void Start ()
	{
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

	/*
	private void updateController (REMOTE_EVENT_DATA rtData)
	{

		if (null == rtData)
			return;

		switch (rtData.type) {

		case (int)eEventType.EVENT_TYPE_KEYEVENT:
			KEY_EVENT_DATA key_data = (KEY_EVENT_DATA)rtData;
			Debug.Log ("ControllerState keycode = " + key_data.keycode + ", action = " + key_data.action);

			switch (key_data.keycode) {
			case 23:
				if (key_data.action == 0) {
					if (key_data.address == instance.controllerid) {
						instance.touchDown = true;
					} else if (key_data.address == instance.controllerid1) {
						instance.touchDown1 = true;
					}
				} else if (key_data.address == instance.controllerid) {
					instance.touchDown = false;
				} else if (key_data.address == instance.controllerid1) {
					instance.touchDown1 = false;
				}
				break;
			case 4:
				if (key_data.action == 0) {
					instance.recentering = true;
					SvrPlugin.Instance.RecenterTracking ();
				} else
					instance.recentering = false;
				break;
			default:
				break;
			}

			break;

		case (int)eEventType.EVENT_TYPE_SENSOR_EVENT:
			
			SENSOR_EVENT_DATA sensor_data = (SENSOR_EVENT_DATA)rtData;

			float x = sensor_data.x / 10000.0f;
			float y = sensor_data.y / 10000.0f;
			float z = sensor_data.z / 10000.0f;
			float w = sensor_data.w / 10000.0f;

			if (Application.isEditor) {
				instance.orientation = new Quaternion (x, y, z, w);
			} else {
				
				if (sensor_data.address == instance.controllerid) {
					if (sensor_data.sensor_type == 0x0F) {
						instance.orientation = new Quaternion (x, y, z, w);
					} else if (sensor_data.sensor_type == 0x01) {
						instance.accel = new Vector3 (x, -z, y);
					} else if (sensor_data.sensor_type == 0x04) {
						instance.gyro = new Vector3 (x, -z, y);
					}

				} else if (sensor_data.address == instance.controllerid1) {
					if (sensor_data.sensor_type == 0x0F) {
						instance.orientation1 = new Quaternion (x, y, z, w);
					} else if (sensor_data.sensor_type == 0x01) {
						instance.accel1 = new Vector3 (x, -z, y);
					} else if (sensor_data.sensor_type == 0x04) {
						instance.gyro1 = new Vector3 (x, -z, y);
					}
				}
			}

			break;

		case (int)eEventType.EVENT_TYPE_MOTIONEVENT:
			
			TOUCH_EVENT_DATA motion_data = (TOUCH_EVENT_DATA)rtData;

			for (int i = 0; i < motion_data.point_count; i++) {
				POINT_INFO curPoint = motion_data.point_info [i];
				Debug.Log ("ControllerState pointId: " + curPoint.point_id + "  (" + curPoint.x + ", " + curPoint.y + ")");
			}

			break;

		case (int)eEventType.EVENT_TYPE_CONNECT_STATE_EVENT:

			CONNECT_STATE_EVENT_DATA state_data = (CONNECT_STATE_EVENT_DATA)rtData;
			Debug.Log ("ControllerState state : state_data.state -" + state_data.state + " state_data.address - " + state_data.address);

			if (state_data.state == 1) {
				if (instance.controllerid == 0) {
					instance.controllerid = state_data.address;
					instance.connectionState = TvrConnectionState.Connected;
				} else if (instance.controllerid1 == 1) {
					instance.controllerid1 = state_data.address;
					instance.connectionState1 = TvrConnectionState.Connected;
				}
			} else if (state_data.state == 0) {
				if (state_data.address == instance.controllerid) {
					instance.controllerid = 0;
					instance.connectionState = TvrConnectionState.Distannected;
				} else if (state_data.address == instance.controllerid1) {
					instance.controllerid1 = 1;
					instance.connectionState = TvrConnectionState.Distannected;
				}
			}

			break;

		default:
			break;
		}


	}
	*/
}
