using System;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;
using UnityEngine.AI;

[RequireComponent(typeof (ThirdPersonCharacter))]
[RequireComponent(typeof(AICharacterControl))]
[RequireComponent(typeof (NavMeshAgent))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float walkMoveStopRadius = .2f;
    [SerializeField] private float attackMoveStopRadius = 5f;

    ThirdPersonCharacter thirdPersonCharacter;   // A reference to the ThirdPersonCharacter on the object
    CameraRaycaster cameraRaycaster;
    Vector3 currentDestination, clickPoint;
    AICharacterControl aiCharacterControl;

    [SerializeField] private const int walkableLayerNumber = 8;
    [SerializeField] private const int enemyLayerNumber = 9;

    private bool isInDirectMode = false;

    private GameObject playerMoveTarget;
        
    private void Start()
    {
        cameraRaycaster = Camera.main.GetComponent<CameraRaycaster>();
        thirdPersonCharacter = GetComponent<ThirdPersonCharacter>();
        currentDestination = transform.position;
        aiCharacterControl = this.GetComponent<AICharacterControl>();

        cameraRaycaster.notifyMouseClickObservers += ProcessMouseClick;

        playerMoveTarget = new GameObject("playerMoveTarget");
    }

    private void ProcessDirectMovement()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        
        // calculate camera relative direction to move:
        Vector3 camForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 Move = v*camForward + h*Camera.main.transform.right;
        
        thirdPersonCharacter.Move(Move, false, false);
    }

    void ProcessMouseClick(RaycastHit raycastHit, int layerhit)
    {
        switch(layerhit)
        {
            case enemyLayerNumber:
                //Navigate to the enemy
                GameObject enemy = raycastHit.collider.gameObject;
                aiCharacterControl.SetTarget(enemy.transform);
                break;

            case walkableLayerNumber:
                playerMoveTarget.transform.position = raycastHit.point;
                aiCharacterControl.SetTarget(playerMoveTarget.transform);
                break;

            default:
                Debug.LogError("Don't know what to do with mouseclick");
                return;
        }
    }



    //private void WalkToDestination()
    //{
    //    Vector3 playerToClickPoint = currentDestination - transform.position;
    //    if (playerToClickPoint.magnitude >= 0)
    //    {
    //        thirdPersonCharacter.Move(playerToClickPoint, false, false);
    //    }
    //    else
    //    {
    //        thirdPersonCharacter.Move(Vector3.zero, false, false);
    //    }
    //}
}

