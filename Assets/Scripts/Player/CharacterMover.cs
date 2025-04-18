using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMover : MonoBehaviour
{
    [SerializeField] private CharacterController controller;
    [SerializeField] BoxCollider groundingJudgment;
    private Vector3 playerVelocity;
    private float playerSpeed = 2.0f;
    private float jumpHeight = 1.0f;
    private float gravityValue = -9.81f;
    void Update()
    {
        bool isGrounded = groundingJudgment.isTrigger;
        if (isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        float axisX = Input.GetAxisRaw("Horizontal");
        float axisY = Input.GetAxisRaw("Vertical");
        Vector3 move = transform.forward * axisY + transform.right * axisX;
        move = move.normalized;
        controller.Move(move * Time.deltaTime * playerSpeed);

        Logger.Log($"{((Input.GetKeyDown(KeyCode.Space) && isGrounded) ? "!!!!" : "")} SpaceKey: {Input.GetKeyDown(KeyCode.Space)}, groundedPlayer: {isGrounded}");
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }
}