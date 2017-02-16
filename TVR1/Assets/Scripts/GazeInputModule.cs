using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Threading;
using System;

public class GazeInputModule : PointerInputModule
{
	public const int POINTER_ID = 0;
	public const string GUI_CAMERA_NAME = "LaserCamera";
	public const string CURSOR_NAME = "Cursor";
	/// <summary>
	/// The GameObject to place at the location where a pointer hits UI
	/// </summary>
	private GameObject Cursor;

	/// <summary>
	/// How far the line will render if there is no UI hit
	/// </summary>
	public float MaxLineLength = 1400.0f;
	/// <summary>
	/// Camera used to project input to the UI
	/// </summary>
	private Camera GuiCamera;
	/// <summary>
	/// Transform for the UI camera
	/// </summary>
	private Transform guiCameraTransform;
	/// <summary> 
	/// GameObject currently under the UI cursor
	/// </summary>
	private GameObject selected;

	private bool touchdown;

	protected override void Start ()
	{
		Input.multiTouchEnabled = true; 
		base.Start ();



		Camera[] cameras = FindObjectsOfType<Camera> ();
		foreach (var camera in cameras) {
			if (camera.name.Equals (GUI_CAMERA_NAME)) {
				guiCameraTransform = camera.transform;
				GuiCamera = camera;
			}
		}

		Canvas[] canvases = FindObjectsOfType<Canvas> ();
		for (int i = 0; i < canvases.Length; i++) {
			canvases [i].worldCamera = GuiCamera;
		}

		GameObject[] gos = FindObjectsOfType<GameObject> ();
		foreach (var go in gos) {
			if (go.name.Equals (CURSOR_NAME)) {
				Cursor = go;
			}
		}
	}
	public override void Process ()
	{

		//guiCameraTransform.position = Controller.position;
		guiCameraTransform.rotation = ControllerState.Orientation;
		PointerEventData pointer = ProcessPointer ();
		selected = pointer.pointerCurrentRaycast.gameObject;

		// handle enter and exit events (highlight)
		HandlePointerExitAndEnter (pointer, selected);
		ProcessTrigger (pointer);

		if (pointer.pointerEnter != null) {
			RectTransform draggingPlane = pointer.pointerEnter.GetComponent<RectTransform> ();
			Vector3 globalLookPos;
			if (RectTransformUtility.ScreenPointToWorldPointInRectangle (draggingPlane,
				pointer.position, pointer.enterEventCamera, out globalLookPos)) {
				Cursor.SetActive (true);
				Cursor.transform.position = globalLookPos;

			}
		} else {
			if (Cursor.activeSelf) {
				Cursor.SetActive (true);
				Cursor.transform.position = guiCameraTransform.position + guiCameraTransform.forward * MaxLineLength;
			}

		}
	}

	private PointerEventData ProcessPointer ()
	{
		PointerEventData pointerEventData;
		GetPointerData (POINTER_ID, out pointerEventData, true);
		pointerEventData.Reset ();

		// Center if the camera on the controller
		Vector2 screenPosition = new Vector2 (0.5f * Screen.width, 0.5f * Screen.height);

		pointerEventData.position = screenPosition;

		// Save the raycast results so we can query them later for bubbling
		m_RaycastResultCache.Clear ();
		eventSystem.RaycastAll (pointerEventData, m_RaycastResultCache);
		pointerEventData.pointerCurrentRaycast = FindFirstRaycast (m_RaycastResultCache);

		return pointerEventData;
	}

	private void ProcessTrigger (PointerEventData pointer)
	{
		if (pointer.eligibleForClick) {
			ExecuteEvents.Execute (pointer.pointerPress, pointer, ExecuteEvents.pointerUpHandler);
			GameObject currentOverObject = pointer.pointerCurrentRaycast.gameObject;
			GameObject clickHandler = ExecuteEvents.GetEventHandler<IPointerClickHandler> (currentOverObject);
			if (pointer.pointerPress == clickHandler) {
				ExecuteEvents.Execute (pointer.pointerPress, pointer, ExecuteEvents.pointerClickHandler);
			}
			pointer.eligibleForClick = false;
			pointer.pointerPress = null;
			pointer.rawPointerPress = null;
			pointer.dragging = false;
			pointer.pointerDrag = null;
			if (currentOverObject != pointer.pointerEnter) {
				HandlePointerExitAndEnter (pointer, null);
				HandlePointerExitAndEnter (pointer, currentOverObject);
			}
		} else if (ControllerState.TouchDown) {
			Debug.Log ("pointer "+ ControllerState.TouchDown);
			pointer.eligibleForClick = true;
			pointer.delta = Vector2.zero;
			pointer.dragging = false;
			pointer.useDragThreshold = true;
			pointer.pressPosition = pointer.position;
			pointer.pointerPressRaycast = pointer.pointerCurrentRaycast;
			GameObject currentOverObject = pointer.pointerCurrentRaycast.gameObject;
			DeselectIfSelectionChanged (currentOverObject, pointer);
			GameObject downHandler = ExecuteEvents.ExecuteHierarchy (currentOverObject, pointer,
				ExecuteEvents.pointerDownHandler);
			if (!downHandler) {
				downHandler = ExecuteEvents.GetEventHandler<IPointerClickHandler> (currentOverObject);
			}
			pointer.pointerPress = downHandler;
			pointer.rawPointerPress = currentOverObject;
			pointer.clickTime = Time.unscaledTime;
			pointer.pointerDrag = null;
			touchdown = false;
			ControllerState.TouchDown = false;
		}
	}
}
