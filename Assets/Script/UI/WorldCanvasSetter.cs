using UnityEngine;

public class WorldCanvasSetter : MonoBehaviour
{
    // ----- FIELDS ----- //
    [SerializeField] private Canvas _canvas;
    // ----- FIELDS ----- //

    private void Start()
    {
        if (!_canvas)
            return;

        if (!_canvas.worldCamera)
            _canvas.worldCamera = Camera.main;
    }
}
