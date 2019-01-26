using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Application : MonoBehaviour
{
	public static Application Inst;

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
		ControllerManager.BothControllersInitializedEvent += OnBothControllersInitialized;
	}

	void OnBothControllersInitialized()
	{
		// Todo: Initialize 2 player controllers here
	}

	void Update()
	{
		ControllerManager.Update();
	}
}
