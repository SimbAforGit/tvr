using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class SensorProvider : MonoBehaviour
{
	public MutablePose3D headPose = new MutablePose3D ();
	// Simulated neck model in the editor mode.
	private static readonly Vector3 neckOffset = new Vector3 (0, 0.075f, -0.08f);
	// Use mouse to emulate head in the editor.
	private float mouseX = 0;
	private float mouseY = 0;
	private float mouseZ = 0;
	private float neckModelScale = 0.0f;
	// Mock settings for in-editor emulation while playing.
	public bool autoUntiltHead = true;

	private static 	WaitForEndOfFrame waitForEndOfFrame = new WaitForEndOfFrame ();

	public delegate void pose3d_listener (Pose3D pose3D);

	public static List<pose3d_listener> Callbacks = new List<pose3d_listener> ();

	public static void addObserverListener (pose3d_listener callback)
	{
		Callbacks.Add (callback);
	}

	public static void delObserverListener (pose3d_listener callback)
	{
		Callbacks.Remove (callback);
	}

	private void doEventDataCallback (Pose3D pose3D)
	{
		for (int i = 0; i < Callbacks.Count; i++) {
			pose3d_listener gameObjectCallback = Callbacks [i];
			if (gameObjectCallback != null) {
				gameObjectCallback (pose3D);
			}
		}
	}

	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		#if UNITY_EDITOR
		UpdateStateFromMouse ();
		#endif	
	}

	public void UpdateStateFromMouse ()
	{
		try {
			Quaternion rot;
			bool rolled = false;
			if (Input.GetKey (KeyCode.LeftAlt) || Input.GetKey (KeyCode.RightAlt)) {
				mouseX += Input.GetAxis ("Mouse X") * 5;
				if (mouseX <= -180) {
					mouseX += 360;
				} else if (mouseX > 180) {
					mouseX -= 360;
				}
				mouseY -= Input.GetAxis ("Mouse Y") * 2.4f;
				mouseY = Mathf.Clamp (mouseY, -85, 85);
			} else if (Input.GetKey (KeyCode.LeftControl) || Input.GetKey (KeyCode.RightControl)) {
				rolled = true;
				mouseZ += Input.GetAxis ("Mouse X") * 5;
				mouseZ = Mathf.Clamp (mouseZ, -85, 85);
			}
			if (!rolled && autoUntiltHead) {
				// People don't usually leave their heads tilted to one side for long.
				mouseZ = Mathf.Lerp (mouseZ, 0, Time.deltaTime / (Time.deltaTime + 0.1f));
			}
			rot = Quaternion.Euler (mouseY, mouseX, mouseZ);
			var neck = (rot * neckOffset - neckOffset.y * Vector3.up) * neckModelScale;
			headPose.Set (neck, rot);
			doEventDataCallback(headPose);
		} catch (Exception e) {
		}
	}
}
