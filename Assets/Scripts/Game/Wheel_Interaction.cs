using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Wheel_Interaction : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Camera playerCamera;

    [Header("Kick Settings")]
    [SerializeField] private float kickRange = 3f;
    [SerializeField] private float kickForce = 8f;
    [SerializeField] private float torqueForce = 15f;
    [SerializeField] private LayerMask tireLayer;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            TryKick();
        }        
    }

    private void TryKick()
    {
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);

        if(Physics.Raycast(ray, out RaycastHit hit, kickRange))
        {
            Rigidbody tireRb = hit.collider.attachedRigidbody;
            if(tireRb == null) return;

            Vector3 kickDirection = playerCamera.transform.forward;
            kickDirection.y = 0f;
            kickDirection.Normalize();

            tireRb.AddForceAtPosition(kickDirection * kickForce, hit.point, ForceMode.Impulse);

            Vector3 rotationAxis = Vector3.Cross(Vector3.up, kickDirection);
            tireRb.AddTorque(rotationAxis * torqueForce, ForceMode.Impulse);

        }
    }
}
