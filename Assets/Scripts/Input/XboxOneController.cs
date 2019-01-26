
using System;
using UnityEngine;

public class XboxOneController
{
	public enum AxisValue
	{
		LeftStickHorizontal,
		LeftStickVertical,
		RightStickHorizontal,
		RightStickVertical,
		LeftTrigger,
		RightTrigger
	}

	public enum ButtonValue
	{
		AButton,
		BButton
	}

	public enum ButtonState
	{
		NotPressed,
		JustPressed,
		CurrentlyPressed,
		JustReleased
	}

	class ControllerState
	{
		public bool AnyInputThisFrame;

		public Vector2 LeftStickAxes;
		public Vector2 RightStickAxes;

		public float LeftTriggerAxis;
		public float RightTriggerAxis;

		public bool AButton;
		public bool BButton;
	}

	ControllerState LastFrame = new ControllerState();
	ControllerState ThisFrame = new ControllerState();

	public readonly string JoystickName;
	readonly bool IsPlayerOne = true;

	readonly string LeftStickHorizontalTag;
	readonly string LeftStickVerticalTag;
	readonly string RightStickHorizontalTag;
	readonly string RightStickVerticalTag;
	readonly string LeftTriggerTag;
	readonly string RightTriggerTag;

	public XboxOneController(bool isPlayerOne, string joystickName)
	{
		IsPlayerOne = true;
		JoystickName = joystickName;

		LeftStickHorizontalTag = "Joy" + (IsPlayerOne ? "1" : "2") + "_LeftStickHorizontal";
		LeftStickVerticalTag = "Joy" + (IsPlayerOne ? "1" : "2") + "_LeftStickVertical";
		RightStickHorizontalTag = "Joy" + (IsPlayerOne ? "1" : "2") + "_RightStickHorizontal";
		RightStickVerticalTag = "Joy" + (IsPlayerOne ? "1" : "2") + "_RightStickVertical";
		LeftTriggerTag = "Joy" + (IsPlayerOne ? "1" : "2") + "_LeftTrigger";
		RightTriggerTag = "Joy" + (IsPlayerOne ? "1" : "2") + "_RightTrigger";
	}

	public void UpdateState()
	{
		SwapInputBuffers();
		
		ThisFrame.AButton = Input.GetKey(IsPlayerOne ? KeyCode.Joystick1Button0 : KeyCode.Joystick2Button0);
		ThisFrame.BButton = Input.GetKey(IsPlayerOne ? KeyCode.Joystick1Button1 : KeyCode.Joystick2Button1);

		ThisFrame.LeftStickAxes.x = Input.GetAxis(LeftStickHorizontalTag);
		ThisFrame.LeftStickAxes.y = Input.GetAxis(LeftStickVerticalTag);

		ThisFrame.RightStickAxes.x = Input.GetAxis(RightStickHorizontalTag);
		ThisFrame.RightStickAxes.y = Input.GetAxis(RightStickVerticalTag);

		ThisFrame.LeftTriggerAxis = Input.GetAxis(LeftTriggerTag);
		ThisFrame.RightTriggerAxis = Input.GetAxis(RightTriggerTag);

		bool anyInputThisFrame = ThisFrame.AButton || ThisFrame.BButton || ThisFrame.LeftStickAxes != Vector2.zero ||
		                         ThisFrame.RightStickAxes != Vector2.zero || ThisFrame.LeftTriggerAxis != 0 ||
		                         ThisFrame.RightTriggerAxis != 0;
		ThisFrame.AnyInputThisFrame = anyInputThisFrame;
	}

	void SwapInputBuffers()
	{
		var oldState = LastFrame;
		LastFrame = ThisFrame;
		ThisFrame = oldState;
	}
	
	public float GetAxisValue(AxisValue axis)
	{
		switch (axis)
		{
			case AxisValue.LeftStickHorizontal:
				return ThisFrame.LeftStickAxes.x;
			case AxisValue.LeftStickVertical:
				return ThisFrame.LeftStickAxes.y;
			case AxisValue.RightStickHorizontal:
				return ThisFrame.RightStickAxes.x;
			case AxisValue.RightStickVertical:
				return ThisFrame.RightStickAxes.y;
			case AxisValue.LeftTrigger:
				return ThisFrame.LeftTriggerAxis;
			case AxisValue.RightTrigger:
				return ThisFrame.RightTriggerAxis;
			default:
				throw new NotImplementedException("No assigned axis value for " + axis);
		}
	}

	public ButtonState GetButtonState(ButtonValue button)
	{
		bool lastFrame = button == ButtonValue.AButton ? LastFrame.AButton : LastFrame.BButton;
		bool thisFrame = button == ButtonValue.AButton ? ThisFrame.AButton : ThisFrame.BButton;

		if (lastFrame && !thisFrame)
		{
			return ButtonState.JustReleased;
		}
		else if (!lastFrame && thisFrame)
		{
			return ButtonState.JustPressed;
		}
		else if (thisFrame)
		{
			return ButtonState.CurrentlyPressed;
		}

		return ButtonState.NotPressed;
	}
}
