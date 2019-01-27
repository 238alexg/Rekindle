using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    const float MovementAmount = 1f;  
    public enum Direction
    {
        up,
        down,
        left, 
        right
    }

    Direction FacingDirection;
    Vector2 WorldPosition;

    public void Move(Direction direction)
    {
        FacingDirection = direction;
        switch (direction)
        {
            case (Direction.up):
                WorldPosition += MovementAmount * Vector2.up;
                break;
            case (Direction.down):
                WorldPosition += MovementAmount * Vector2.down;
                break;
            case (Direction.left):
                WorldPosition += MovementAmount * Vector2.left;
                break;
            case (Direction.right):
                WorldPosition += MovementAmount * Vector2.right;
                break;
        }
    }

    public void Interact()
    {
        Vector2 offset = Vector2.zero;
        switch (FacingDirection)
        {
            case (Direction.up):
                offset = Vector2.up;
                break;
            case (Direction.down):
                offset = Vector2.down;
                break;
            case (Direction.left):
                offset = Vector2.left;
                break;
            case (Direction.right):
                offset = Vector2.right;
                break;
        }
    }  
}
