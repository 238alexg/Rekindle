using UnityEngine;

public class XboxControllerManager : ControllerManager
{
	protected override void UpdateControllers()
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
				BothControllersInitializedEvent();
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
					ControllersReconnectedEvent();
					Debug.LogError("All controllers paired!");
				}
			}
		}

		PlayerOneController.UpdateState();
		PlayerTwoController.UpdateState();
	}
}
