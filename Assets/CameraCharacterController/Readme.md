# Unity Camera Character Controller

A complete, production-ready character controller system for Unity featuring both first-person and third-person camera perspectives with advanced movement mechanics, animation blending, and input handling using Unity's new Input System.

## Features

### Movement System
- **Multiple Movement States**: Idle, Walk, Run, Sprint, Jump, Fall
- **Smooth Movement Transitions**: Acceleration-based movement with customizable drag
- **Advanced Ground Detection**: Reliable grounding with sphere-cast validation
- **Slope Handling**: Automatic wall sliding on steep surfaces
- **Configurable Physics**: Adjustable gravity, terminal velocity, and jump mechanics
- **Diagonal Movement Restriction**: Optional sprint limitation for directional movement

### Camera System
- **First-Person & Third-Person Support**: Pre-configured prefabs for both perspectives
- **Smooth Camera Rotation**: Mouse/gamepad look with adjustable sensitivity
- **Vertical Look Clamping**: Prevents over-rotation with configurable limits
- **Camera Distance Control**: Third-person zoom with mouse scroll
- **Smart Player Rotation**: Automatic character rotation to camera direction with idle turning animations

### Animation System
- **Blend Tree Integration**: 8-directional movement (forward, backward, left, right, and diagonals)
- **Movement Speed Blending**: Smooth transitions between walk, run, and sprint animations
- **Action Animations**: Attack and gathering animations with interrupt handling
- **Aerial Animations**: Dedicated jump and fall animations with directional support
- **Rotation Animations**: 90-degree turn animations for idle state
- **Upper Body Masking**: Separate control for upper body actions during movement

### Input System
- **Unity Input System**: Modern input handling with rebinding support
- **Multiple Control Schemes**: Keyboard & Mouse, Gamepad, Touch, XR
- **Toggle & Hold Options**: Configurable sprint behavior (hold vs toggle)
- **Walk Mode Toggle**: Optional walking mode for precise movement
- **Action Inputs**: Separate action map for attacks and gathering
- **Modular Input Architecture**: Clean separation between input handling and game logic

### Mobile Controls
- **On-Screen Virtual Joysticks**: Touch-based movement and camera controls
- **Mobile UI Canvas**: Responsive mobile layout with proper scaling
- **Touch Button Controls**: Jump, Punch, and Gather actions via touch buttons
- **Camera Swap Button**: Toggle between first-person and third-person camera views
- **Platform-Specific Sensitivity**: Separate sensitivity settings for mobile vs desktop

## Package Contents

### 📁 Scripts/
Core logic components:
- **PlayerController.cs** - Main movement controller with physics and camera handling
- **PlayerAnimation.cs** - Animation state management and blend tree control
- **PlayerState.cs** - Movement state machine and state queries
- **CharacterControllerUtils.cs** - Utility functions for ground detection
- **Input/** - Modular input system components
  - `PlayerInputManager.cs` - Singleton input manager
  - `PlayerLocomotionInput.cs` - Movement and camera input
  - `PlayerActionsInput.cs` - Action input (attack, gather)
  - `ThirdPersonInput.cs` - Third-person camera controls

### 📁 Prefabs/
Ready-to-use character controllers:
- **FirstPersonController.prefab** - First-person configuration with camera setup
- **ThirdPersonController.prefab** - Third-person configuration with Cinemachine camera
- **InputManager.prefab** - Persistent input manager singleton

### 📁 Input/
Unity Input System assets:
- **PlayerControls.inputactions** - Input action definitions
- **PlayerControls.cs** - Auto-generated C# wrapper for input actions

### 📁 Animation/
Complete animation system:
- **PlayerAnimator.controller** - Animator controller with blend trees and transitions
- **UpperBody.mask** - Avatar mask for upper body animations
- Movement animations: Idle, Walk, Run, Sprint (all 8 directions)
- Action animations: PunchRight, Gathering
- Aerial animations: Jump variations, Falling loops
- Turn animations: 90-degree left/right turns
- **FBX files** - Source animation files

### 📁 Materials/
Example materials:
- **GroundMat.mat** - Ground surface material
- **ObstacleMat.mat** (x3) - Obstacle materials with variations

### 📁 Presets/
Unity project settings:
- **PlayerSettings.preset** - Recommended player settings for the character controller
- **TagManager.preset** - Required tags and layers configuration

### 📁 FREE_HumanLowPoly/
Low-poly character assets:
- Character meshes (optimized for games)
- Character materials and textures
- Pre-configured character prefabs

## Requirements

- **Unity Version**: 2021.3 LTS or newer
- **Unity Packages**:
  - Input System (com.unity.inputsystem) - v1.0.0+
  - Cinemachine (com.unity.cinemachine) - v3.0.0+ (for third-person controller)
- **Rendering Pipeline**: Universal Render Pipeline (URP) recommended

## Installation

### Option 1: Unity Package Import
1. Download the `.unitypackage` file
2. In Unity, go to **Assets > Import Package > Custom Package**
3. Select the downloaded package and click **Import**
4. Import all contents or select specific folders as needed

### Option 2: Direct Folder Copy
1. Copy the `CameraCharacterController` folder
2. Paste into your Unity project's `Assets` folder
3. Unity will automatically import and compile the scripts

### Post-Installation Setup
1. **Apply Project Settings**:
   - Go to `Assets/Alex/CameraCharacterController/Presets/`
   - Apply `TagManager.preset` to configure required layers
   - Apply `PlayerSettings.preset` for optimal settings (optional)

2. **Install Required Packages**:
   - Open **Window > Package Manager**
   - Install "Input System" if not already installed
   - Install "Cinemachine" if using third-person controller

3. **Configure Input System**:
   - When prompted, allow Unity to restart after enabling the new Input System
   - Ensure "Both" or "New Input System" is selected in **Edit > Project Settings > Player > Active Input Handling**

## Usage

### Quick Start

1. **Add Input Manager to Scene**:
   ```
   Drag 'InputManager.prefab' into your scene hierarchy
   This prefab uses DontDestroyOnLoad and manages all input
   ```

2. **Add Character Controller**:
   ```
   Drag either 'FirstPersonController.prefab' or 'ThirdPersonController.prefab' into your scene
   Position the character at your desired spawn point
   ```

3. **Configure Ground Layers**:
   - Select the character controller in the hierarchy
   - In the Inspector, find the `Player Controller` component
   - Set "Ground Layers" to include your ground/floor layer(s)

4. **Test the Setup**:
   - Press Play
   - Use WASD/Arrow Keys to move
   - Use Mouse to look around
   - Press Space to jump
   - Hold Left Shift to sprint
   - Press Left Click for attack
   - Press E for gather action

### Input Controls

#### Keyboard & Mouse
- **Movement**: WASD or Arrow Keys
- **Look**: Mouse movement
- **Jump**: Spacebar
- **Sprint**: Left Shift (hold or toggle - configurable)
- **Walk**: Left Ctrl (toggle)
- **Attack**: Left Mouse Button
- **Gather**: E key
- **Zoom** (Third-Person): Mouse Scroll Wheel

#### Gamepad
- **Movement**: Left Stick
- **Look**: Right Stick
- **Jump**: A Button (Xbox) / X Button (PlayStation)
- **Sprint**: Left Stick Press (L3)
- **Attack**: Y Button (Xbox) / Triangle (PlayStation)

#### Mobile Touch Controls
- **Movement**: Left virtual joystick (On-Screen Stick)
- **Look Around**: Right virtual joystick (On-Screen Stick)
- **Jump**: Touch button
- **Punch**: Touch button
- **Gather**: Touch button
- **Camera Swap**: Camera toggle button (switches between first-person and third-person views)

### Camera Switching

The system supports seamless switching between first-person and third-person camera perspectives:

#### Desktop Controls
- **Camera Toggle**: Press **V** key to switch between camera modes
- **Third-Person Default**: The system starts in third-person mode by default
- **Smooth Transitions**: Character position and rotation are preserved during camera switches

#### Mobile Controls
- **Camera Button**: Tap the camera swap button in the mobile UI to toggle camera modes
- **Visual Feedback**: Button provides clear indication of current camera mode
- **Touch Integration**: Works seamlessly with other mobile controls

#### Camera Switching Features
- **Position Synchronization**: Characters maintain their exact position when switching cameras
- **Input System Management**: All movement and action controls work correctly in both camera modes
- **State Preservation**: Movement state, animations, and input handling are properly maintained
- **Cross-Platform**: Camera switching works identically on desktop and mobile platforms

#### Setup Requirements
To use camera switching, ensure both controller prefabs are in your scene:
1. **ThirdPersonController**: Contains Cinemachine-based third-person camera
2. **FirstPersonController**: Contains first-person camera setup
3. **MobileControls**: UI canvas with camera swap button (for mobile)

The camera switching system automatically manages input callbacks and component states to ensure smooth operation.

## Customization

### Movement Settings
Select your character controller and adjust in the **Player Controller** component:

**Base Movement**
- `Walk Speed` - Speed when walking (default: 2)
- `Run Speed` - Normal running speed (default: 4)
- `Sprint Speed` - Maximum sprint speed (default: 7)
- `Walk/Run/Sprint Acceleration` - How quickly each speed is reached
- `Drag` - Ground movement deceleration
- `In Air Drag` - Air movement deceleration

**Jump & Gravity**
- `Gravity` - Downward acceleration (default: 25)
- `Jump Speed` - Initial jump velocity multiplier (default: 0.8)
- `Terminal Velocity` - Maximum falling speed (default: 50)

**Camera Settings**
- `Look Sense H` - Horizontal look sensitivity (default: 0.1)
- `Look Sense V` - Vertical look sensitivity (default: 0.1)
- `Look Limit V` - Vertical look angle limit in degrees (default: 89)

**Mobile Camera Settings**
- `Mobile Mutliplier` - Sensitivity multiplier for mobile devices (default: 3.0)

**Animation**
- `Player Model Rotation Speed` - How fast the character rotates to face camera direction (default: 10)
- `Rotate To Target Time` - Duration of idle turning animation (default: 0.67)

### Input Configuration
To modify input mappings:
1. Open `Assets/Alex/CameraCharacterController/Input/PlayerControls.inputactions`
2. Edit action bindings in the Input Actions editor
3. Unity will auto-regenerate the C# wrapper file

**Sprint Behavior**:
- Select the character controller
- Find `Player Locomotion Input` component
- Toggle `Hold To Sprint` (checked = hold, unchecked = toggle)

### Animation Customization
The `PlayerAnimator.controller` uses blend trees for:
- **Locomotion Blend Tree**: 8-directional movement based on `inputX`, `inputY`, and `inputMagnitude`
- **Speed Layers**: Walk (0-0.5), Run (0.5-1.0), Sprint (1.0-1.5)

To add custom animations:
1. Import your animation files
2. Add them to the appropriate blend tree in the Animator Controller
3. Adjust blend tree thresholds as needed

### Creating Custom Actions
To add new actions (e.g., crouch, roll):

1. **Add Input Action**:
   - Open `PlayerControls.inputactions`
   - Add new action to `PlayerActionMap`
   - Assign key binding

2. **Handle in Code**:
   - Edit `PlayerActionsInput.cs`
   - Add property: `public bool CrouchPressed { get; private set; }`
   - Implement callback: `public void OnCrouch(InputAction.CallbackContext context)`
   - Add to interface implementation

3. **Add Animation**:
   - Add animator parameter in `PlayerAnimator.controller`
   - Create animation state and transitions
   - Set animation parameter in `PlayerAnimation.cs`

## Architecture

### Execution Order
The system uses Unity's `DefaultExecutionOrder` attribute:
- **-3**: `PlayerInputManager` (reads input first)
- **-2**: All input components (process input)
- **-1**: `PlayerController` (applies movement)
- **0**: `PlayerAnimation`, `PlayerState` (update visuals)

### Component Dependencies
```
PlayerInputManager (Singleton)
  ↓
PlayerLocomotionInput ←─┐
PlayerActionsInput      │
ThirdPersonInput        │ (All reference PlayerInputManager.Instance)
  ↓                     │
PlayerController ───────┘
  ↓
PlayerAnimation
PlayerState
```

## Troubleshooting

### Character not moving
- Ensure InputManager prefab is in the scene
- Check that Player Controller component's "Ground Layers" includes your floor
- Verify Unity Input System is enabled in Project Settings

### Camera not following character
- **Third-Person**: Ensure Cinemachine is installed and camera's "Follow" target is set to character
- **First-Person**: Check that camera is child of character object

### Animation not playing
- Verify Animator component has `PlayerAnimator` controller assigned
- Check that character model has a properly configured Avatar
- Ensure all animation parameters exist in the Animator Controller

### Input not responding
- Check `PlayerControls.inputactions.meta` has correct namespace: `Alex.CameraCharacterController`
- Reimport the PlayerControls.inputactions file to regenerate C# wrapper
- Ensure Input System package is installed and active

### Mobile UI not visible
- Ensure EventSystem is present in the scene with Input System UI Input Module
- Check Canvas Render Mode is set to Screen Space - Overlay or Screen Space - Camera
- Verify Canvas Scaler settings for proper mobile scaling
- Ensure mobile UI elements are on the "UI" layer

### Errors after importing package
- If you see "PlayerControls could not be found" errors:
  1. Select `PlayerControls.inputactions` in Project window
  2. In Inspector, verify "Generate C# Class" is enabled
  3. Check "Namespace" field shows `Alex.CameraCharacterController`
  4. Click "Apply" to regenerate the code

## Performance Considerations

- **Ground Detection**: Uses optimized sphere cast only when needed
- **Animation Blending**: Smoothed over time to reduce frame-to-frame changes
- **Physics**: Single `CharacterController.Move()` call per frame
- **Low-Poly Models**: Included character models are optimized for real-time rendering

## Credits & License

**Character Controller System**
- Developed by: Alex
- Namespace: `Alex.CameraCharacterController`
- Based on tutorial by: spaderdabomb
  - YouTube: [@spaderdabomb](https://www.youtube.com/@spaderdabomb)
  - GitHub: [spaderdabomb](https://github.com/spaderdabomb)

**Included Assets**
- Low-poly character models: FREE_HumanLowPoly
- Built with Unity Input System
- Cinemachine integration for third-person camera

## Version History

**v1.0**
- Initial release
- First and third-person controllers
- Complete animation system
- Unity Input System integration
- Gamepad support
- Action system with attack and gather

**v1.1**
- Added mobile touch controls support
- On-screen virtual joysticks for movement and camera
- Touch buttons for Jump, Punch, and Gather actions
- Platform-specific sensitivity settings
- Mobile UI Canvas with proper scaling

## Support

For issues, questions, or contributions:
1. Check the Troubleshooting section above
2. Verify all requirements are met
3. Ensure latest version of required packages are installed

---

**Note**: This package uses the `Alex.CameraCharacterController` namespace. Ensure no conflicting namespaces exist in your project.
