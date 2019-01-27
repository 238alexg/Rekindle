using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;

public class Player : MonoBehaviour
{
	enum Direction
	{
		North,
		South,
		East,
		West
	}

	const float SpeedModifier = 5f;
	const float ShellRadius = 0.01f;

	public SpriteRenderer SpriteRenderer;
	public Rigidbody2D Rigidbody;

	public Animator Animator;

	[NonSerialized] public readonly List<Item> Inventory = new List<Item>();

	ContactFilter2D ContactFilter;
	RaycastHit2D[] HitBuffer = new RaycastHit2D[16];
	protected readonly List<RaycastHit2D> HitBufferList = new List<RaycastHit2D>(16);

	XboxOneController Controller;
	Physics2DRaycaster Raycaster;

	Direction CurrentDirection;
	
	public void Initialize(XboxOneController controller)
	{
		Controller = controller;
	}

	public void UpdatePlayerWithInput()
	{
		var joystickVector = Controller.GetAxis(XboxOneController.StickValue.LeftStick);
		joystickVector *= SpeedModifier;

		var horizontalMove = joystickVector;
		horizontalMove.y = 0;
		RequestMove(horizontalMove);

		var verticalMove = joystickVector;
		verticalMove.x = 0;
		RequestMove(verticalMove);

		CurrentDirection = GetDirection(joystickVector);
		if (Controller.GetButtonState(XboxOneController.ButtonValue.AButton) ==
		    XboxOneController.ButtonState.JustReleased)
		{
			AttemptButtonInteraction(XboxOneController.ButtonValue.AButton, CurrentDirection);
		}
		else if (Controller.GetButtonState(XboxOneController.ButtonValue.BButton) ==
		         XboxOneController.ButtonState.JustReleased)
		{
			AttemptButtonInteraction(XboxOneController.ButtonValue.BButton, CurrentDirection);
		}

		// TODO: Update sprite renderer with a character movement animation

		if (Application.Inst.EnableScreenSpaceDarkness)
		{
			var screenPoint = Camera.main.WorldToScreenPoint(transform.position);
			var normalizedScreenSpace = new Vector2(screenPoint.x / Screen.width, screenPoint.y / Screen.height);
			Application.Inst.ScreenSpaceDarkness.AddLight(normalizedScreenSpace, radius: 0.2f, new Color(0.8f, 0.2f, 0.2f, 0.3f));
		}
	}

	Direction GetDirection(Vector2 joystickVector)
	{
		if (joystickVector == Vector2.zero)
		{
			Animator.SetInteger("State", 0);
			return CurrentDirection;
		}
		else if (Mathf.Abs(joystickVector.x) > Mathf.Abs(joystickVector.y))
		{
			Animator.SetInteger("State", joystickVector.x > 0 ? 2 : 4);
			return joystickVector.x > 0 ? Direction.East : Direction.West;
		}
		else
		{
			Animator.SetInteger("State", joystickVector.y > 0 ? 1 : 3);
			return joystickVector.y > 0 ? Direction.North : Direction.South;
		}
	}

    void RequestMove(Vector2 requestedMoveVector)
    {
	    float moveDistance = requestedMoveVector.magnitude;

		int count = Rigidbody.Cast(requestedMoveVector, ContactFilter, HitBuffer, moveDistance + ShellRadius);
		HitBufferList.Clear();

		for (int i = 0; i < count; i++)
		{
			HitBufferList.Add(HitBuffer[i]);
		}

	    foreach (var raycastHit2D in HitBufferList)
		{
			float modifiedDistance = raycastHit2D.distance - ShellRadius;
			moveDistance = modifiedDistance < moveDistance ? modifiedDistance : moveDistance;
		}

		Rigidbody.position = Rigidbody.position + requestedMoveVector.normalized * moveDistance;
	}

	void AttemptButtonInteraction(XboxOneController.ButtonValue buttonValue, Direction direction)
	{
		var destination = transform.position;
		var directionVector = DirectionToVector2(direction);
		destination.x += directionVector.x * 32;
		destination.y += directionVector.y * 32;
		var hit = Physics2D.Linecast(transform.position, destination);
		
		if (hit)
		{
			var tileMap = hit.collider.GetComponent<Tilemap>();

			if (tileMap == null) return;

			
			var cellPos = tileMap.WorldToCell(destination);
			var obstacle = tileMap.GetTile<Obstacle>(cellPos);

			if (obstacle == null) return;

			if (buttonValue == XboxOneController.ButtonValue.AButton)
			{
				obstacle.OnAButtonPress(this);
			}
			else
			{
				obstacle.OnBButtonPress(this);
			}
		}
	}

	Vector3Int GetPlayerTilePosition()
	{
		var halfScreenSize = new Vector2(Screen.width / 2, Screen.height / 2);
		var vectorToPlayer = (Vector2)transform.position + halfScreenSize;
		return new Vector3Int((int)(vectorToPlayer.x / 32), (int)(vectorToPlayer.y / 32), 0);
	}

	Vector2 DirectionToVector2(Direction direction)
	{
		switch (direction)
		{
			case Direction.North:
				return Vector2.up;
			case Direction.South:
				return Vector2.down;
			case Direction.East:
				return Vector2.right;
			case Direction.West:
				return Vector2.left;
			default:
				return Vector2.zero;
		}
	}
}
