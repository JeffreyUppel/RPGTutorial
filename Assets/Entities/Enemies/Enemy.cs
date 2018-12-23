using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class Enemy : MonoBehaviour
{

	[SerializeField] private float maxHealthPoints = 100f;
	private float currentHealthPoints = 100f;

	[SerializeField]private ThirdPersonCharacter thirdPersonCharacter;
	[SerializeField]private AICharacterControl aiCharacterControl;
	[SerializeField] private Transform target = null;

	[SerializeField] private float range = 5f;
	[SerializeField] private float attackRadius = .5f;
	
	

	private void Start()
	{
		
	}

	public float healthAsPercentage
	{
		get { return currentHealthPoints / maxHealthPoints; }
	}

	private void Update()
	{
		float dis = Vector3.Distance(target.position, this.transform.position);

		if (dis <= range && dis >= attackRadius)
		{
			aiCharacterControl.SetTarget(target.transform);
		}
		else
		{
			aiCharacterControl.SetTarget(transform);
		}
	}
}
