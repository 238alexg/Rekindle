using System;
using UnityEngine;

public abstract class ControllerManager
{
	public XboxOneController PlayerOneController;
	public XboxOneController PlayerTwoController;

	protected bool BothControllersInitialized = false;
	protected bool BothControllersPaired = false;

	public Action BothControllersInitializedEvent = delegate { };
	public Action OneOrMoreControllersDisconnectedEvent = delegate { };
	public Action ControllersReconnectedEvent = delegate { };
	
#if DEBUG_CONTROLLERS
	string ControllerOneString;
	string ControllerTwoString;
#endif

	public void Update()
	{
		UpdateControllers();

#if DEBUG_CONTROLLERS
		var controllerOneDebugString = PlayerOneController.ToString();
		var controllerTwoDebugString = PlayerTwoController.ToString();

		if (controllerOneDebugString != ControllerOneString)
		{
			ControllerOneString = controllerOneDebugString;
			Debug.LogWarning(PlayerOneController.JoystickName + " change : " + ControllerOneString);
		}
		if (controllerTwoDebugString != ControllerTwoString)
		{
			ControllerTwoString = controllerTwoDebugString;
			Debug.LogWarning(PlayerTwoController.JoystickName + " change: " + ControllerTwoString);
		}
#endif
	}

	protected abstract void UpdateControllers();
}
