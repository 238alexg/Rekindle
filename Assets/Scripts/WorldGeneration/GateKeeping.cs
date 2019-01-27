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

	public TileBase BlueDoor;
	public TileBase GreenDoor;
	public TileBase RedDoor;
	public TileBase YellowDoor;
	public TileBase Open; 

	public TileBase GetDoor(DoorColor color)
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