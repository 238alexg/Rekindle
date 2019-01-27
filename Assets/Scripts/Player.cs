using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
	const float SpeedModifier = 5f;
	const float ShellRadius = 0.01f;

	public SpriteRenderer SpriteRenderer;
	public Rigidbody2D Rigidbody;

	ContactFilter2D ContactFilter;
	RaycastHit2D[] HitBuffer = new RaycastHit2D[16];
	protected List<RaycastHit2D> HitBufferList = new List<RaycastHit2D>(16);

	XboxOneController Controller;
	Physics2DRaycaster Raycaster;

	public void Initialize(XboxOneController controller)
	{
		Controller = controller;
	}

	public void UpdatePlayerWithInput()
	{
		var movementVector = Controller.GetAxis(XboxOneController.StickValue.LeftStick);
		movementVector *= SpeedModifier;

		var horizontalMove = movementVector;
		horizontalMove.y = 0;
		RequestMove(horizontalMove);

		var verticalMove = movementVector;
		verticalMove.x = 0;
		RequestMove(verticalMove);
		
		// TODO: Update sprite renderer with a character movement animation

		if (Application.Inst.EnableScreenSpaceDarkness)
		{
			Application.Inst.ScreenSpaceDarkness.AddLight(transform.position, radius: 0.2f, new Color(0.8f, 0.2f, 0.2f, 0.3f));
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
}
