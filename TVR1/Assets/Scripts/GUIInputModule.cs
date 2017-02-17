using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Threading;
using System;

public class GUIInputModule : PointerInputModule
{

	#region -- Constants --

	public const int POINTER_ID = 0;
	public const string GUI_CAMERA_NAME = "LaserCamera";
	public const string CURSOR_NAME = "Cursor";
	public const string CONTROLLER_NAME = "ControllerHead";

	#endregion

	#region -- public values the dev need set --

	/// <summary>
	/// How far the line will render if there is no UI hit
	/// </summary>
	public float MaxLineLength = 1400.0f;

	#endregion

	/// <summary>
	/// The GameObject to place at the location where a pointer hits UI
	/// </summary>
	public GameObject Cursor;

	#region -- Controller --

	/// <summary>
	/// Transform for the input controller. The position and rotation of this will be used to raycast into the UI.
	/// </summary>
	public Transform ControllerHead;
	/// <summary>
	/// Line Renderer to use for the cursor.
	/// </summary>
	private LineRenderer Line;

	#endregion

	/// <summary>
	/// Camera used to project input to the UI
	/// </summary>
	public Camera GuiCamera;
	/// <summary> 
	/// GameObject currently under the UI cursor
	/// </summary>
	private GameObject selected;
	private bool touchdown;
	private static bool useGaze = false;
	private static bool useController = false;

	public Config.GUITypes GUIType {
		get { 
			return guiType;
		}
		set {
			guiType = value;
		}
	}

	[SerializeField]
	private Config.GUITypes guiType = Config.GUITypes.Controller;

	void Awake ()
	{
		if (guiType == Config.GUITypes.Gaze) {
			useGaze = true;
		} else if (guiType == Config.GUITypes.Controller) {
			useController = true;
		}
	}

	protected override void Start ()
	{
		base.Start ();

		Canvas[] canvases = FindObjectsOfType<Canvas> ();
		for (int i = 0; i < canvases.Length; i++) {
			canvases [i].worldCamera = GuiCamera;
		}

		// we need controller line and the camera should move to below
		if (useController) {
			Line = GetComponent<LineRenderer> ();
			GuiCamera.transform.Translate (new Vector3 (0, -0.5f, 0),Space.World);
			ControllerHead.transform.Translate (new Vector3 (0, -0.5f, 0),Space.World);
		}

	}

	private void ShowCursorAndLine (PointerEventData pointer)
	{
		if (pointer.pointerEnter != null) {
			RectTransform draggingPlane = pointer.pointerEnter.GetComponent<RectTransform> ();
			Vector3 globalLookPos;
			if (RectTransformUtility.ScreenPointToWorldPointInRectangle (draggingPlane,
				    pointer.position, pointer.enterEventCamera, out globalLookPos)) {
				Cursor.SetActive (true);
				Cursor.transform.position = globalLookPos;
				if (Line != null) {
					Line.SetPosition (0, ControllerHead.position);
					Line.SetPosition (1, Cursor.transform.position);
				}
			}
		} else {
			if (Cursor.activeSelf) {
				Cursor.SetActive (true);
				Cursor.transform.position = ControllerHead.position + ControllerHead.forward * MaxLineLength;
			}
			if (Line != null) {
				Line.SetPosition (0, ControllerHead.position);
				Line.SetPosition (1, ControllerHead.position + ControllerHead.forward * MaxLineLength);
			}
		}
	}

	#region -- process the pointer event -----------

	public override void Process ()
	{
		if (useGaze) {
			ControllerHead.rotation = SensorState.Head_Quaternion;
			GuiCamera.transform.rotation = SensorState.Head_Quaternion;
			Cursor.transform.rotation = SensorState.Head_Quaternion;
		} else if (useController) {
			ControllerHead.rotation = SensorState.Head_Quaternion;
			GuiCamera.transform.rotation = SensorState.Head_Quaternion;
			Cursor.transform.rotation = SensorState.Head_Quaternion;
		}

		PointerEventData pointer = ProcessPointer ();
		selected = pointer.pointerCurrentRaycast.gameObject;

		// handle enter and exit events (highlight)
		HandlePointerExitAndEnter (pointer, selected);
		ProcessTrigger (pointer);

		ShowCursorAndLine (pointer);


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
		} else if (SensorState.TouchDown) {
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
			SensorState.TouchDown = false;
		}
	}

	#endregion
}
