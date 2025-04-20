using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMover : MonoBehaviour
{
    [SerializeField] private CharacterController controller;
    [SerializeField] BoxCollider groundingJudgment;
    private bool isGrounded = false;
    private Vector3 playerVelocity;
    private float playerSpeed = 2.0f;
    private float jumpHeight = 1.0f;
    private float gravityValue = -9.81f;
    public bool IsGrounded { set { isGrounded = value; } }
    void Update()
    {
        // 着地、落下処理
        if (isGrounded && playerVelocity.y != 0)
        {
            playerVelocity.y = 0f;
        }
        else if (!isGrounded)
        {
            playerVelocity.y += gravityValue * Time.deltaTime;
        }

        // 移動処理
        float axisX = Input.GetAxisRaw("Horizontal");
        float axisY = Input.GetAxisRaw("Vertical");
        Vector3 move = transform.forward * axisY + transform.right * axisX;
        move.y = 0;
        move = move.normalized;
        controller.Move(move * Time.deltaTime * playerSpeed);
        controller.Move(playerVelocity * Time.deltaTime);
    }
}