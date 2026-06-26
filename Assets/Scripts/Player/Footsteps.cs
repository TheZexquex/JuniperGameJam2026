using System;
using System.Collections;
using Audio;
using UnityEngine;
using Random = UnityEngine.Random;

public class Footsteps : MonoBehaviour
{
g    [Header("Step Timing")]
    [SerializeField] private float walkStepInterval = 0.3f;
    [SerializeField] private float runStepInterval = 0.2f;
    [SerializeField] private float walkSpeedThreshold = 8.1f;
    [SerializeField] private AudioClip[] footsteps;
    

    private FirstPersonController _firstPersonController;

    private void Start()
    {
        _firstPersonController = GetComponent<FirstPersonController>();
        StartCoroutine(Walk());
    }

    IEnumerator Walk()
    {
        while (true)
        {
            if (_firstPersonController.MovementVelocity.magnitude > 0.1 && _firstPersonController._isGrounded)
            {
                PlayFootstep();
            }
            
            var interval = _firstPersonController.MovementVelocity.magnitude > walkSpeedThreshold ? runStepInterval : walkStepInterval;
            
            yield return new WaitForSeconds(interval);
        }
    }

    void PlayFootstep()
    {
        AudioManager.PlaySoundAtPlayer(footsteps[Random.Range(0, footsteps.Length)]);
    }
}
