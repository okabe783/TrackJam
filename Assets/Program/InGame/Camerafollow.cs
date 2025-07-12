using UnityEngine;

public class Camerafollow : MonoBehaviour
{
    [SerializeField]private Transform _player;
    [SerializeField] Vector3 _offset;
    private float _smoothSpeed = 0.125f;

    void FixedUpdate() // または LateUpdate()
    {
        if (_player != null)
        {
            Vector3 desiredPosition = _player.position + _offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, _smoothSpeed);
            transform.position = smoothedPosition;
        }
    }
}
