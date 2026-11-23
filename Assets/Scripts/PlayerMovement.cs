using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Varibles")]
    [SerializeField] float movementSpeed;
    [SerializeField] float jumpForce;
    [SerializeField] Vector2 moveInput;
    [SerializeField] Rigidbody playerRB;
    [SerializeField] Animator playerAnimator;
    private int MoveInputXHash = Animator.StringToHash("moveInputX");
    private int MoveInputYHash = Animator.StringToHash("moveInputY");
    private int IsMovingHash = Animator.StringToHash("isMoving");



    private void Awake()
    {
        playerRB = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        HandlePlayerMovement();
        HandlePlayerAnimation();
    }

    private void Update()
    {
        
    }

    #region Movement

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void OnJump()
    {
        if (Mathf.Abs(playerRB.linearVelocity.y) < 0.01f) playerRB.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    private void HandlePlayerMovement()
    {
        Vector3 move = new Vector3(moveInput.x, 0, moveInput.y) * movementSpeed * Time.deltaTime;
        playerRB.MovePosition(playerRB.position + move);
    }
    #endregion

    private void HandlePlayerAnimation()
    {
        if (moveInput != Vector2.zero)
        {
            playerAnimator.SetFloat(MoveInputXHash, moveInput.x);
            playerAnimator.SetFloat(MoveInputYHash, moveInput.y);


            playerAnimator.SetBool(IsMovingHash, true);
        }
        else
        {
            playerAnimator.SetBool(IsMovingHash, false);
        }
    }
}
