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

	public ItemTile BlueDoor;
	public ItemTile GreenDoor;
	public ItemTile RedDoor;
	public ItemTile YellowDoor;
	public ItemTile Open; 

	public ItemTile GetDoor(DoorColor color)
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