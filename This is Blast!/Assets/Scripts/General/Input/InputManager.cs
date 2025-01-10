using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class InputManager : MonoSingleton<InputManager>
{
    private const float MAX_RAYCAST_DISTACNCE = 500f;

    [SerializeField] private Camera m_mainCamera;
    
    protected override void Init()
    {
        EnhancedTouchSupport.Enable();
        Touch.onFingerDown += FingerDown;
        Touch.onFingerMove += FingerMove;
    }

    private void OnDestroy()
    {
        Touch.onFingerDown -= FingerDown;
        Touch.onFingerDown -= FingerMove;
        EnhancedTouchSupport.Disable();
    }

    private void FingerDown(Finger finger)
    {
        IHandleInput inputHandler = GetInputHandlerAtPosition(finger.screenPosition);
        inputHandler?.HandleClick();
    }
    private void FingerMove(Finger finger)
    {
        IHandleInput inputHandler = GetInputHandlerAtPosition(finger.screenPosition);
        inputHandler?.HandleDrag();
    }

    private IHandleInput GetInputHandlerAtPosition(Vector3 position)
    {
        Ray rayCast = m_mainCamera.ScreenPointToRay(position);
        RaycastHit rayHit;
        if (!Physics.Raycast(rayCast, out rayHit, MAX_RAYCAST_DISTACNCE))
        {
            return null;
        }
        return rayHit.collider.GetComponent<IHandleInput>();
    }
}
