using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementInput : MonoBehaviour
{
    [Header("Input Asset")]
    public InputActionAsset inputActions;
    public string actionMapName = "Player";
    public string moveActionName = "Move";
    public string jumpActionName = "Jump";
    public string sprintActionName = "Sprint";
    public string rollActionName = "Roll";

    [Header("Animator Parameters")]
    public string moveXParam = "MoveX";
    public string moveYParam = "MoveY";
    public float sprintAnimationMultiplier = 2.0f;

    [Header("Smoothing")]
    public float dampTime = 0.1f;

    private InputActionMap actionMap;
    private InputAction moveAction;
    private InputAction jumpAction;
    private InputAction sprintAction;
    private InputAction rollAction;
    private Animator anim;

    public Vector2 MoveInput { get; private set; }
    public bool JumpTriggered { get; set; }
    public bool SprintHeld { get; private set; }
    public bool RollTriggered { get; set; }

    void Awake()
    {
        anim = GetComponent<Animator>();

        if (inputActions == null)
        {
            Debug.LogError("ERROR: PlayerControls asset is missing in the Inspector!");
            return;
        }

        // 1. Find the Action Map
        actionMap = inputActions.FindActionMap(actionMapName, throwIfNotFound: true);
        if (actionMap == null)
        {
            Debug.LogError($"ERROR: Could not find Action Map named '{actionMapName}'!");
            return;
        }

        // 2. Find individual actions and print errors if any names don't match
        moveAction = actionMap.FindAction(moveActionName, throwIfNotFound: false);
        if (moveAction == null) Debug.LogError($"Failed to find Move action: '{moveActionName}'");

        jumpAction = actionMap.FindAction(jumpActionName, throwIfNotFound: false);
        if (jumpAction == null) Debug.LogError($"Failed to find Jump action: '{jumpActionName}'");

        sprintAction = actionMap.FindAction(sprintActionName, throwIfNotFound: false);
        if (sprintAction == null) Debug.LogError($"Failed to find Sprint action: '{sprintActionName}'");

        rollAction = actionMap.FindAction(rollActionName, throwIfNotFound: false);
        if (rollAction == null) Debug.LogError($"Failed to find Roll action: '{rollActionName}'");
    }

    void OnEnable()
    {
        // Enable the entire map (this is the safest way for builds)
        actionMap?.Enable();
    }

    void OnDisable()
    {
        actionMap?.Disable();
    }

    void Update()
    {
        if (moveAction != null)
        {
            MoveInput = moveAction.ReadValue<Vector2>();
        }

        if (jumpAction != null && jumpAction.WasPressedThisFrame())
        {
            JumpTriggered = true;
        }

        if (rollAction != null && rollAction.WasPressedThisFrame())
        {
            RollTriggered = true;
        }

        if (sprintAction != null)
        {
            SprintHeld = sprintAction.IsPressed();
        }

        Vector2 animatorInput = MoveInput;
        if (SprintHeld)
        {
            animatorInput *= sprintAnimationMultiplier;
        }

        anim.SetFloat(moveXParam, animatorInput.x, dampTime, Time.deltaTime);
        anim.SetFloat(moveYParam, animatorInput.y, dampTime, Time.deltaTime);
    }
}