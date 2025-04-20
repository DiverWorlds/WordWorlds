using UnityEngine;

public class GroundingJudgment : MonoBehaviour
{
    [SerializeField] private CharacterMover characterMover;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            characterMover.IsGrounded = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            characterMover.IsGrounded = false;
        }
    }
}