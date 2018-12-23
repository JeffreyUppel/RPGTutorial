using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CameraRaycaster))]
public class CursorAffordance : MonoBehaviour
{

	[SerializeField] private Texture2D walkCursor = null;
	[SerializeField] private Texture2D attackCursor = null;
	[SerializeField] private Texture2D unknownCursor = null;
	
	[SerializeField] private Vector2 cursorHotspot = new Vector2(0, 0);

	[SerializeField] private CameraRaycaster cameraRaycaster;

    //TODO solve fight between serialize and const
	[SerializeField] private const int walkableLayerNumber = 8;
	[SerializeField] private const int enemyLayerNumber = 9;
	

	private void Start()
	{
		cameraRaycaster.notifyLayerChangeObservers += OnLayerChanged;	//registering 
	}

	private void OnLayerChanged (int newLayer)
	{

		Debug.Log(newLayer);
		
		switch (newLayer)
		{
			case walkableLayerNumber:
				Cursor.SetCursor(walkCursor, cursorHotspot, CursorMode.Auto);
				break;
			
			case enemyLayerNumber:
				Cursor.SetCursor(attackCursor, cursorHotspot, CursorMode.Auto);
				break;
			
			default:
				Debug.LogError("Don't know what cursor to put");
				Cursor.SetCursor(unknownCursor, cursorHotspot, CursorMode.Auto);
				return;
		}
	}
	
	//TODO consider de-registering OnLayerChanged on leaving all game scenes
}
