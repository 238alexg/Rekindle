using UnityEngine;

public class Application : MonoBehaviour
{
	public static Application Inst;
	public ScreenSpaceDarkness ScreenSpaceDarkness;
	public bool EnableScreenSpaceDarkness = false;

#if USE_XB1_CONTROLLERS
	readonly ControllerManager ControllerManager = new XboxControllerManager();
#else
	readonly ControllerManager ControllerManager = new PCControllerManager();
#endif

	// Todo: Add 2 readonly player controllers here

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
		// Todo: Initialize 2 player controllers here
	}

	void Update()
	{
		ControllerManager.Update();
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
}
