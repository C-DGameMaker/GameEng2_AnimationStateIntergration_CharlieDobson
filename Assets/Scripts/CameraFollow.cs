using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Camera Settings")]
    public Transform playerCharacter;
    public float smoothSpeed = 5f;
    public float distanceFromPlayer = 5f;

    [SerializeField] Vector3 offset;

    private void Awake()
    {
        offset = transform.position - playerCharacter.position;
    }
    public void SwapLeader(Transform leaderPosition)
    {
        playerCharacter = leaderPosition;

        offset = transform.position - playerCharacter.position;
    }

    private void LateUpdate()
    {
        Vector3 cameraOffset = Quaternion.Euler(0f, 0f, 0f) * offset * distanceFromPlayer;
        transform.position = playerCharacter.position + cameraOffset;
    }
}
