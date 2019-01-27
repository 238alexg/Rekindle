public class PCControllerManager : ControllerManager
{
	protected override void UpdateControllers()
	{
		if (!BothControllersInitialized)
		{
			PlayerOneController = new XboxOneController(isPlayerOne: true, "PCJoy1");
			PlayerTwoController = new XboxOneController(isPlayerOne: false, "PCJoy2");

			BothControllersInitialized = true;
			BothControllersPaired = true;
			BothControllersInitializedEvent();
		}

		PlayerOneController.UpdateState();
		PlayerTwoController.UpdateState();
	}
}
