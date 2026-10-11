
using UnityEngine;
using UnityEngine.InputSystem;

namespace Alex.CameraCharacterController
{
  [DefaultExecutionOrder(-2)]
  public class PlayerLocomotionInput : MonoBehaviour, PlayerControls.IPlayerLocomotionMapActions
  {
    #region Class Variables
    [SerializeField] private bool holdToSprint = true;
    // public PlayerControls PlayerControls { get; private set; }
    public Vector2 MovementInput { get; private set; }
    public Vector2 LookInput { get; private set; }
    public bool JumpPressed { get; private set; }
    public bool SprintToggledOn { get; private set; }
    public bool WalkToggledOn { get; private set; }
    #endregion

    #region Startup
    private void OnEnable()
    {
      if (PlayerInputManager.Instance?.PlayerControls == null)
      {
        Debug.LogError("Player controls is not initialized - cannot enable");
        return;
      }

      PlayerInputManager.Instance.PlayerControls.PlayerLocomotionMap.Enable();
      PlayerInputManager.Instance.PlayerControls.PlayerLocomotionMap.SetCallbacks(this);
    }

    private void OnDisable()
    {
      if (PlayerInputManager.Instance?.PlayerControls == null)
      {
        Debug.LogError("Player controls is not initialized - cannot disable");
        return;
      }

      PlayerInputManager.Instance.PlayerControls.PlayerLocomotionMap.Disable();
      PlayerInputManager.Instance.PlayerControls.PlayerLocomotionMap.RemoveCallbacks(this);
    }
    #endregion

    #region Late Update Logic
    private void LateUpdate()
    {
      JumpPressed = false;
    }
    #endregion

    #region Input Callbacks
    public void OnMovement(InputAction.CallbackContext context)
    {
      MovementInput = context.ReadValue<Vector2>();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
      LookInput = context.ReadValue<Vector2>();
    }

    public void OnToggleSprint(InputAction.CallbackContext context)
    {
      if (context.performed)
      {
        SprintToggledOn = holdToSprint || !SprintToggledOn;
      }
      else if (context.canceled)
      {
        SprintToggledOn = !holdToSprint && SprintToggledOn;
      }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
      if (!context.performed)
        return;

      JumpPressed = true;
    }

    public void OnToggleWalk(InputAction.CallbackContext context)
    {
      if (!context.performed)
        return;

      WalkToggledOn = !WalkToggledOn;
    }

    public void OnCameraToggle(InputAction.CallbackContext context)
    {
      if (!context.performed)
        return;

      // Find both camera controllers in the scene (including inactive ones)
      GameObject thirdPersonController = FindGameObjectInScene("ThirdPersonController");
      GameObject firstPersonController = FindGameObjectInScene("FirstPersonController");

      if (thirdPersonController != null && firstPersonController != null)
      {
        // Get current active state of third person controller
        bool isThirdPersonActive = thirdPersonController.activeInHierarchy;
        
        // Get the input components from both controllers
        PlayerLocomotionInput thirdPersonInput = thirdPersonController.GetComponent<PlayerLocomotionInput>();
        PlayerLocomotionInput firstPersonInput = firstPersonController.GetComponent<PlayerLocomotionInput>();
        PlayerActionsInput thirdPersonActions = thirdPersonController.GetComponent<PlayerActionsInput>();
        PlayerActionsInput firstPersonActions = firstPersonController.GetComponent<PlayerActionsInput>();
        
        // Ensure proper input system cleanup and re-initialization
        if (isThirdPersonActive)
        {
          // Switching to First Person
          // Disable third person input components first
          if (thirdPersonInput != null)
          {
            thirdPersonInput.enabled = false;
          }
          if (thirdPersonActions != null)
          {
            thirdPersonActions.enabled = false;
          }
          
          // Sync positions before switching
          SyncControllerPositions(thirdPersonController, firstPersonController);
          
          // Activate first person controller
          firstPersonController.SetActive(true);
          
          // Re-enable first person input components
          if (firstPersonInput != null)
          {
            firstPersonInput.enabled = true;
          }
          if (firstPersonActions != null)
          {
            firstPersonActions.enabled = true;
          }
          
          // Deactivate third person controller
          thirdPersonController.SetActive(false);
        }
        else
        {
          // Switching to Third Person
          // Disable first person input components first
          if (firstPersonInput != null)
          {
            firstPersonInput.enabled = false;
          }
          if (firstPersonActions != null)
          {
            firstPersonActions.enabled = false;
          }
          
          // Sync positions before switching
          SyncControllerPositions(firstPersonController, thirdPersonController);
          
          // Activate third person controller
          thirdPersonController.SetActive(true);
          
          // Re-enable third person input components
          if (thirdPersonInput != null)
          {
            thirdPersonInput.enabled = true;
          }
          if (thirdPersonActions != null)
          {
            thirdPersonActions.enabled = true;
          }
          
          // Deactivate first person controller
          firstPersonController.SetActive(false);
        }
        
        Debug.Log($"Camera switched to: {(isThirdPersonActive ? "First Person" : "Third Person")}");
      }
      else
      {
        Debug.LogError("Camera controllers not found! Make sure ThirdPersonController and FirstPersonController exist in the scene.");
      }
    }

    private GameObject FindGameObjectInScene(string name)
    {
      // This method finds GameObjects even if they are inactive
      // Using FindObjectsByType with FindObjectsSortMode.None for better performance
      GameObject[] allObjects = FindObjectsByType<GameObject>(FindObjectsInactive.Include, FindObjectsSortMode.None);
      
      foreach (GameObject obj in allObjects)
      {
        if (obj.name == name)
        {
          return obj;
        }
      }
      
      return null;
    }

    private void SyncControllerPositions(GameObject sourceController, GameObject targetController)
    {
      if (sourceController != null && targetController != null)
      {
        // Sync the main controller positions
        targetController.transform.position = sourceController.transform.position;
        targetController.transform.rotation = sourceController.transform.rotation;
        
        Debug.Log($"Position synced from {sourceController.name} to {targetController.name} at position: {sourceController.transform.position}");
      }
    }
    #endregion
  }
}