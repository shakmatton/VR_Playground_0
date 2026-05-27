using UnityEngine;
using UnityEngine.InputSystem; 

public class CameraToggle : MonoBehaviour
{
    [SerializeField] private GameObject topViewCanvas;  // o Canvas com RawImage
    [SerializeField] private Camera topViewCamera;      // sua "2ndCamera"

    private bool _showingTop = false;

    void Start()
    {
        // Garante estado inicial correto
        topViewCanvas.SetActive(false);
        topViewCamera.enabled = false;
    }

    void Update()
    {
        if (Keyboard.current.tabKey.wasPressedThisFrame)   // Tab alterna entre câmeras
        {
            _showingTop = !_showingTop;
            topViewCanvas.SetActive(_showingTop);
            topViewCamera.enabled = _showingTop;
        }
    }
}