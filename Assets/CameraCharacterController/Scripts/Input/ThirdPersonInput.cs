using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Alex.CameraCharacterController
{
    [DefaultExecutionOrder(-2)]
    public class ThirdPersonInput : MonoBehaviour, PlayerControls.IThirdPersonMapActions
    {
        public Vector2 ScrollInput { get; private set; }

        [SerializeField] private CinemachineCamera _cmCamera;
        [SerializeField] private float _cameraZoomSpeed = 0.1f;
        [SerializeField] private float _cameraMinZoom = 1f;
        [SerializeField] private float _cameraMaxZoom = 5f;

        private CinemachineThirdPersonFollow _thirdPersonFollow;

        private void Awake()
        {
            // If not wired in the Inspector, try to find it on this GameObject.
            if (_cmCamera == null)
                _cmCamera = GetComponent<CinemachineCamera>();

            if (_cmCamera == null)
            {
                Debug.LogError("CinemachineCamera reference is missing. Assign it in the Inspector or put this script on the CM camera.");
                enabled = false;
                return;
            }

            // Grab the ThirdPersonFollow component from the same GO as the CM camera
            _thirdPersonFollow = _cmCamera.GetComponent<CinemachineThirdPersonFollow>();
            if (_thirdPersonFollow == null)
            {
                Debug.LogError("CinemachineThirdPersonFollow is missing on the CinemachineCamera. Add the Third Person Follow component.");
                enabled = false;
            }
        }

        private void OnEnable()
        {
            if (PlayerInputManager.Instance?.PlayerControls == null)
            {
                Debug.LogError("Player controls is not initialized - cannot enable");
                return;
            }
            PlayerInputManager.Instance.PlayerControls.ThirdPersonMap.Enable();
            PlayerInputManager.Instance.PlayerControls.ThirdPersonMap.SetCallbacks(this);
        }

        private void OnDisable()
        {
            if (PlayerInputManager.Instance?.PlayerControls == null)
            {
                Debug.LogError("Player controls is not initialized - cannot disable");
                return;
            }
            PlayerInputManager.Instance.PlayerControls.ThirdPersonMap.Disable();
            PlayerInputManager.Instance.PlayerControls.ThirdPersonMap.RemoveCallbacks(this);
        }

        private void Update()
        {
            if (_thirdPersonFollow == null) return;

            _thirdPersonFollow.CameraDistance =
                Mathf.Clamp(_thirdPersonFollow.CameraDistance + ScrollInput.y, _cameraMinZoom, _cameraMaxZoom);
        }

        private void LateUpdate()
        {
            ScrollInput = Vector2.zero;
        }

        public void OnScrollCamera(InputAction.CallbackContext context)
        {
            if (!context.performed) return;

            Vector2 scrollInput = context.ReadValue<Vector2>();
            ScrollInput = -1f * scrollInput.normalized * _cameraZoomSpeed;
        }
    }
}
