using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Application : MonoBehaviour
{
	public static Application Inst;

	readonly ControllerManager ControllerManager = new ControllerManager();

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
}
