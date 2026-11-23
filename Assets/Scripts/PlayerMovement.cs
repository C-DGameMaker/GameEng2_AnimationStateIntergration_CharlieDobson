using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Varibles")]
    [SerializeField] float movementSpeed;
    [SerializeField] Vector2 moveInput;
    [SerializeField] Rigidbody playerRB;
    [SerializeField] Animator playerAnimator;
    
    private int MoveInputYHash = Animator.StringToHash("moveInputY");
    private int IsMovingHash = Animator.StringToHash("isMoving");
    private int IsInteractingHash = Animator.StringToHash("isInteracting");



    private void Awake()
    {
        playerRB = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        HandlePlayerMovement();
        HandlePlayerAnimation();
    }

    public void Interact()
    {
        playerAnimator.SetBool(IsInteractingHash, true);
    }
    
    #region Movement

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
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
            playerAnimator.SetFloat(MoveInputYHash, moveInput.y);
            playerAnimator.SetBool(IsMovingHash, true);
            playerAnimator.SetBool(IsInteractingHash, false);
        }
        else
        {
            playerAnimator.SetBool(IsMovingHash, false);
            playerAnimator.SetBool(IsInteractingHash, false);
        }
    }
}
