using UnityEngine;

public partial class PlayerStateManager
{
    private float interactionAreaRadius = 1.2f;
    [SerializeField] private Collider closestInteractable;
    [SerializeField] private LayerMask interactablesLayerMask;
    public void InteractionArea()
    {
        //          reset closestInteractable to null
        closestInteractable = null;
        //          interaction area
        Vector3 playerPosition = transform.position;
        Collider[] interactableArray = Physics.OverlapSphere(playerPosition, interactionAreaRadius, interactablesLayerMask);
        //          get closest interactable
        foreach (Collider interactable in interactableArray) {
            if (closestInteractable == null) {
                closestInteractable = interactable;
            } else if (Vector3.Distance(transform.position, interactable.transform.position) < Vector3.Distance(transform.position, closestInteractable.transform.position)) {
                closestInteractable = interactable;
            }   
        }
    }
}
