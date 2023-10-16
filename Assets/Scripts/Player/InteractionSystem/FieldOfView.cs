using System.Collections;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    [SerializeField] private float fovRadius;
    [SerializeField] private float fovMaxAngle;
    [SerializeField] private LayerMask enemyLayerMask;
    [SerializeField] private Collider closestEnemy;
    [SerializeField] private Transform cameraTransform;

    private void Update() {
        if (Input.GetKey(KeyCode.L)) {
            FieldOfViewCheck();
        }
    }

    private void FieldOfViewCheck()
    {
        Collider[] enemyArray = Physics.OverlapSphere(transform.position, fovRadius, enemyLayerMask);
    
        foreach (Collider enemy in enemyArray) {
            if (closestEnemy == null) {
                closestEnemy = enemy;
            } else if (Vector3.Angle(cameraTransform.forward, (cameraTransform.position - closestEnemy.transform.position).normalized) < Vector3.Angle(cameraTransform.forward, (cameraTransform.position - enemy.transform.position).normalized)) {
                closestEnemy = enemy;
            }
        }
    }
}

