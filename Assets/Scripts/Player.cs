using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
	const float SpeedModifier = 0.05f;

	public SpriteRenderer SpriteRenderer;
	
	XboxOneController Controller;
	Physics2DRaycaster Raycaster;

	public void Initialize(XboxOneController controller)
	{
		Controller = controller;
	}

	public void UpdatePlayerWithInput()
	{
		var currentDirection = Controller.GetAxis(XboxOneController.StickValue.LeftStick);
		currentDirection *= SpeedModifier;

		if (!DidHitCollider(currentDirection * 16))
		{
			transform.position += (Vector3)currentDirection;
		}

		// TODO: Update sprite renderer with a character movement animation

		if (Application.Inst.EnableScreenSpaceDarkness)
		{
			Application.Inst.ScreenSpaceDarkness.AddLight(transform.position, radius: 0.2f, new Color(0.8f, 0.2f, 0.2f, 0.3f));
		}
	}

    bool DidHitCollider(Vector2 raycastVector)
    {
		// Player's current starting position
		Vector2 start = transform.position;

		// Calculate end position based on the direction parameters passed in when calling Move.
		Vector2 end = new Vector2(transform.position.x + raycastVector.x, transform.position.y + raycastVector.y);
		RaycastHit2D hit = Physics2D.Linecast(start, end);

		//Check if anything was hit

		// TODO: If player hit an item, react differently than if they hit a wall
		if (hit)
		{
			return true;
		}
		else
		{
			return false;
		}
	}  
}
