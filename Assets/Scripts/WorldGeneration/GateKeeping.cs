using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu]
public class GateKeeping : ScriptableObject
{
	public enum DoorColor
	{
		Blue,
		Green,
		Red,
		Yellow
	}

	public Obstacle BlueDoor;
	public Obstacle GreenDoor;
	public Obstacle RedDoor;
	public Obstacle YellowDoor;
	public Obstacle Open; 

	public Obstacle  GetDoor(DoorColor color)
	{
		switch(color)
		{
			case DoorColor.Blue:
				return BlueDoor;
			case DoorColor.Green:
				return GreenDoor;
			case DoorColor.Red:
				return RedDoor;
			case DoorColor.Yellow:
				return YellowDoor;
			default:
				return null;
		}
	}
}