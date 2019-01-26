using System;
using UnityEngine;

public class ControllerManager : MonoBehaviour
{
	XboxOneController PlayerOneController;
	XboxOneController PlayerTwoController;

	bool BothControllersInitialized = false;
	bool BothControllersPaired = false;

	public event Action OneOrMoreControllersDisconnectedEvent = delegate { };
	public event Action AllControllersConnectedEvent = delegate { };
	
	void Update()
	{
		var joystickNames = Input.GetJoystickNames();

		if (!BothControllersInitialized)
		{
			if (joystickNames.Length >= 2)
			{
				if (PlayerOneController == null)
				{
					PlayerOneController = new XboxOneController(isPlayerOne: true, joystickNames[0]);
				}
				if (PlayerTwoController == null)
				{
					PlayerTwoController = new XboxOneController(isPlayerOne: false, joystickNames[1]);
				}

				BothControllersInitialized = true;
				BothControllersPaired = true;
				AllControllersConnectedEvent();
			}
			else
			{
				return;
			}
		}
		else
		{
			if (BothControllersPaired)
			{
				bool oneOrBothControllersDisconnected = joystickNames[0] != PlayerOneController.JoystickName ||
				                                        joystickNames[1] != PlayerTwoController.JoystickName;
				if (oneOrBothControllersDisconnected)
				{
					BothControllersPaired = false;
					OneOrMoreControllersDisconnectedEvent();
					Debug.LogError("Cannot play game with less than 2 controllers!");
				}
			}
			else if (!BothControllersPaired && joystickNames.Length >= 2)
			{
				if (joystickNames[0] == PlayerOneController.JoystickName &&
				    joystickNames[1] == PlayerTwoController.JoystickName)
				{
					BothControllersPaired = true;
					AllControllersConnectedEvent();
					Debug.LogError("All controllers paired!");
				}
			}
		}
		
		PlayerOneController.UpdateState();
		PlayerTwoController.UpdateState();
    }
}
