# JamStarterKit
Used to come with some basic stuff for a Unity jam

# Introduction
This small project was created to help everyone start their GameJam with a starter kit
The project includes a small script and prefab that uses the new Unity input system
Also a small UI scene setupping so you just have to add your own behaviors
This project is just a small helper for new comers to Unity that start with the Engine. 

#### Note : The "BasicPlayer3D" and "BasicPlayer2D" prefabs both have a Rigodbody / Rigidbody2D attached, allowing them to have physic applied to them / gravity.
If you don't want your character to be simulated with Physic, just remove the components. Smile :)

# Requirement 
[Having Unity 2019.2.12f1 installed](https://unity3d.com/fr/get-unity/download/archive "Having Unity 2019.2.12f1 installed")

# Input system
Unity 2019 provides inside the new InputPackage a way of handling Inputs.
We did a small project already embedding that, so you should just need to take the package and start coding

## Where to start ?
Inside "Scene/InputExemple" you'll get a basic scene setupping that uses the new InputSystem.
You can open this file by going at : Assets/Inputs/BasicPlayerInput.inputactions
Now Unity let's you create "Actions" (Jump, Fire, Move ...) linked to one or multiple inputs.

In this exemple, we can see that the Action "Move" is related to "WASD" AND "Left stick" from any gamepad. Meaning that both by pressing WASD key or moving the joystick, the action is going to be called.


Think about it like a dictionnary, that takes a Key as an action and has inputs as Value to trigger this actions.

*Note* : Unity also handle automaticly that if you put "W" as a key on a US keyboard, it's going to shift to "Z" in a French one, you have nothing to worry about that (smile) 


## Let's get to it !
When you start the scene no player is present into it. Unity provides a way to spawn automaticly a Prefab when you press specific inputs.
Let's say you're launching the game, you press any key present into the actions that we saw below. Automaticly a Player will spawn in the scene, and only receive inputs from where you spawned it.
Meaning that if you press the keyboard or mouse input, your player will only receive callbacks from the action made from theses inputs, and not the gamepad.

And vice versa.

## Let's say you have 3 inputs :
One keyboard / Mouse
2 xbox gamepads
Each player press a key with their controller. 3 player will spawn and each of their script will only receive their callbacks (Keyboard / Mouse, Gamepad 1, Gamepad 2)

The InputManager as a "PlayerInputManager" script attached to it that handles that behavior.

## You have multiple choice :
You can automaticly spawn players from this script, when specific inputs are pressed
You can limit the number of player spawned (4 for exemple)
You can split screen automaticly regarding the number of player into the scene. NOTE : By doing that, you'll need to add a Camera inside your player and fill it in the"PlayerInput" script.
If your game is solo player, just put your player into the scene and you don't care about the "PlayerInputManager"

## BasicPlayer
If you check the BasicPlayer.prefab (Assets/Prefabs/BasicPlayer/BasicPlayer.prefab)
You can see that you have a "PlayerInput" script attached to it and a" BasicPlayerInput" script also.
"BasicPlayerInput" : Receive all the callbacks from the Actions you defined. Meaning that when you press "W" for exemple, the function "OnMove" is going to be called.

The callback link was not made using code, but Unity's interface like so :

Here are the callback linked to, you can see it in "BasicPlayerInput.cs" at Assets/Scripts/Input: 

    // All of this callback can be found on the prefab attached to this script
    // Just click on "PlayerInput" script -> Events -> Name of the group action
    // And just drag'n'drop whatever callback you want. 
    public void OnMove(InputAction.CallbackContext context)
    {
        Debug.Log("BasicPlayerInput : Move");
        m_Move = context.ReadValue<Vector2>();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        Debug.Log("BasicPlayerInput : Look");
        m_Look = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        Debug.Log("BasicPlayerInput : Jump");
        m_Jump = context.ReadValue<float>();
    }

    // I added here simple "context" exemple, if you want to do specific action on how the input was made.
    // In Unity's exemple they used this state to make some kind of "Charging" shoot
    public void OnFire(InputAction.CallbackContext context)
    {
        Debug.Log("BasicPlayerInput : Fire");
        switch (context.phase)
        {
            case InputActionPhase.Performed:
                break;
            case InputActionPhase.Started:
                break;
            case InputActionPhase.Canceled:
                break;
        }
    }
    
Now you know how to give an action inputs and read them throught code! 
The input asset made right now has basic behavior, if you need more, add your and then link them in the "PlayerInput" script. 
Also, right now the functions only sends logs, but this is where you would code your behavior.

## Device lost event
Unity spawn automaticly a player if you want to. But doesn't kill it when the device is lost (AKA A Gamepad being disconnected)

For exemple a Gamepad being disconnected. 

We just destroy the player, so it creates a new one when you plug back the input, but you could pause the game, for exemple.

    public void DeviceLostEvent(PlayerInput test)
    {
        Destroy(gameObject);
    }


## Conclusion
#### If it's a solo game :

Just drag'n'drop the "BasicPlayer" prefab inside your game, and code your behavior inside the "BasicInput" script.

#### If it's a local multiplayer game :

Use the "InputManger" gameobject inside "InputExempleScene" inside your Scene. 
If it's a local split-screen game, click on "Enable Split Screen", attach a Camera to your "BasicPlayer" and add it the Camera to the "PlayerInput" script.

If it's a local game, with only one camera, you're good to go (smile)

If you don't want to spawn players when doing inputs, and do it yourself, just remove the "InputManager" script.
In the end, the "BasicPlayerScript" receive all callbacks defined from your action, and that's it. You should normally be able to code from the get-go without having to setup anything (smile)

The scene is setup in a 3D world (Copy / pasted from Unity sample project), but you can use it for 2D without issues, because the script only receive inputs. So you do what you want about it
