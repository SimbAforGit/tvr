using UnityEngine;
using System.Collections;

public class VRMain : MonoBehaviour
{
	// Which types of tracking this instance will use.
	public bool trackRotation = true;
	public bool trackPosition = false;

	public bool VRModeEnabled {
		get {
			return vrModeEnabled;
		}
		set {
			vrModeEnabled = value;
		}
	}

	[SerializeField]
	private bool vrModeEnabled = true;

	public Config.GlassesTypes GlassesType {
		get {
			return glassesType;
		}
		set {
			glassesType = value;
		}
	}

	[SerializeField]
	private Config.GlassesTypes glassesType = Config.GlassesTypes.MojingIII;

	void Update(){
		UpdateHead ();
	}

	private void UpdateHead(){
		if (trackPosition) {
			
		}
		if (trackRotation) {
			transform.rotation = SensorState.Head_Quaternion;
		}
	}
}
