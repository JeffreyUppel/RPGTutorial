using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraArm : MonoBehaviour 
{
	[SerializeField] private Transform playerTrans;

	// Update is called once per frame
	void LateUpdate () 
	{
		Vector3 pos = new Vector3(playerTrans.position.x, playerTrans.position.y, playerTrans.position.z);
		this.transform.position = pos;
	}
}
