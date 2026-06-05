using UnityEngine;

public class RotateTowardsCamera : MonoBehaviour
{
    // ----- FIELDS ----- //
    [SerializeField] private Camera _camera;
    // ----- FIELDS ----- //

    private void Start()
    {
        if (!_camera)
            _camera = Camera.main;
    }

    private void Update()
    {
        if (!_camera)
            return;

        transform.LookAt(transform.position + _camera.transform.rotation * Vector3.forward, _camera.transform.rotation * Vector3.up);
    }
}
