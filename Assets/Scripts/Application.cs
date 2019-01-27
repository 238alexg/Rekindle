using UnityEngine;

public class Application : MonoBehaviour
{
	public static Application Inst;
	public ScreenSpaceDarkness ScreenSpaceDarkness;
	public bool EnableScreenSpaceDarkness = false;

	public Player PlayerOne;
	public Player PlayerTwo;

#if USE_XB1_CONTROLLERS
	readonly ControllerManager ControllerManager = new XboxControllerManager();
#else
	readonly ControllerManager ControllerManager = new PCControllerManager();
#endif
	
	void Start()
	{
		if (Inst != null)
		{
			Destroy(this);
		}
		else
		{
			Inst = this;
			InitializeGame();
		}
	}

	void InitializeGame()
	{
		Debug.Assert(ScreenSpaceDarkness != null, "No screen space darkness script attached to Application");
		ControllerManager.BothControllersInitializedEvent += OnBothControllersInitialized;
	}

	void OnBothControllersInitialized()
	{
		PlayerOne.Initialize(ControllerManager.PlayerOneController);
		PlayerTwo.Initialize(ControllerManager.PlayerTwoController);
	}

	void Update()
	{
		ControllerManager.Update();

		PlayerOne.UpdatePlayerWithInput();
		PlayerTwo.UpdatePlayerWithInput();

		UpdateScreenSpaceShader();
	}

	void UpdateScreenSpaceShader()
	{
		if (EnableScreenSpaceDarkness)
		{
			if (!ScreenSpaceDarkness.gameObject.activeSelf)
			{
				ScreenSpaceDarkness.gameObject.SetActive(true);
			}
			ScreenSpaceDarkness.UpdateTextureWithLights();
		}
		else if (ScreenSpaceDarkness.gameObject.activeSelf)
		{
			ScreenSpaceDarkness.gameObject.SetActive(false);
		}
	}

		void Loader()
	{
<<<<<<< HEAD
		string loadedWorld = JsonSaver.Load("");
		int dataSeparator = loadedWorld.IndexOf("+", 0);
		string mapData = loadedWorld.Substring (0, dataSeparator);
		string characterData = loadedWorld.Substring (dataSeparator, loadedWorld.Length - dataSeparator);
=======
		string loadedWorld = JsonSaver.load();
		int dataSeparator = loadedWorld.IndexOf("+", 0);
		string mapData = loadedWorld.Substring (0, dataSeparator);
		string characterData = loadedWorld.Substring (dataSeparator, loadedWorld.Length - dataSeparator);
		
>>>>>>> 09ee793... change Loader function in application.cs
	}
}
