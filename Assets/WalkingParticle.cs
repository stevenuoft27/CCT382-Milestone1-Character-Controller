using UnityEngine;

public class WalkingParticle : MonoBehaviour
{
    [SerializeField] private ParticleSystem dustParticles;
    private PlayerMovementInput inputScript;

    private void Awake()
    {
        inputScript = GetComponent<PlayerMovementInput>();
    }

    private void Update()
    {
        if (dustParticles == null || inputScript == null) return;

        // Check if WASD is being pressed
        bool isMoving = inputScript.MoveInput.sqrMagnitude > 0.01f;

        if (isMoving && !dustParticles.isPlaying)
        {
            dustParticles.Play();
        }
        else if (!isMoving && dustParticles.isPlaying)
        {
            dustParticles.Stop();
        }
    }
}