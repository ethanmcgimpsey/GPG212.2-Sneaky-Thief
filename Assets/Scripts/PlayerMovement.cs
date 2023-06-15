using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float gizmosSphere = 1f;
    [SerializeField] private InputAction mouseClickAction;

    private Camera mainCamera;
    private NavMeshAgent navMeshAgent;
    private Vector3 targetPosition;

    private void Awake()
    {
        mainCamera = Camera.main;
        navMeshAgent = GetComponent<NavMeshAgent>();

        // Subscribe to the mouseClickAction event
        mouseClickAction.performed += UpdateTargetPosition;
    }

    private void OnEnable()
    {
        // Enable the mouseClickAction
        mouseClickAction.Enable();
    }

    private void OnDisable()
    {
        // Disable the mouseClickAction
        mouseClickAction.Disable();
    }

    private void UpdateTargetPosition(InputAction.CallbackContext context)
    {
        // Create a ray from the camera to the clicked point
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit) && hit.collider)
        {
            // Move the player to the clicked point
            navMeshAgent.SetDestination(hit.point);
            targetPosition = hit.point;
        }
    }

    private void Update()
    {
        if (navMeshAgent.hasPath)
        {
            targetPosition = navMeshAgent.destination;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(targetPosition, gizmosSphere);
    }
}
