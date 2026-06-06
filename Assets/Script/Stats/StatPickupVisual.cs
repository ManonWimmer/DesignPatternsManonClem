using UnityEngine;

public class StatPickupVisual : MonoBehaviour
{
    // ----- FIELDS ----- //
    [SerializeField] private StatPickupTrigger _trigger;
    [SerializeField] private GameObject _mesh;
    [SerializeField] private ParticleSystem _collectFx;
    // ----- FIELDS ----- //

    private void Start()
    {
        if (_trigger)
            _trigger.OnCollected += HandleCollected;
    }

    private void OnDestroy()
    {
        if (_trigger)
            _trigger.OnCollected -= HandleCollected;
    }

    private void HandleCollected()
    {
        if (_mesh)
            _mesh.SetActive(false);

        if (_collectFx)
            _collectFx.Play();
    }
}
