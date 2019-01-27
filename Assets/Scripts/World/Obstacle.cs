using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Obstacle : TileBase, IAButtonHandler, IBButtonHandler
{
	public event Action<bool> ObstacleLockChangedEvent = delegate { };

	public Item Solution;
	public Vector3Int Position;

	bool Unlocked = false;
	
	void TryUnlockObstacle(Player player)
	{
		foreach (var item in player.Inventory)
		{
			if (item == Solution)
			{
				UnlockObstacle();
			}
		}
	}

	public void RelockObstacle()
	{
		ObstacleLockChangedEvent(false);
	}

	void UnlockObstacle()
	{
		Unlocked = true;
		ObstacleLockChangedEvent(true);
	}

	public void OnAButtonPress(Player player)
	{
		TryUnlockObstacle(player);
	}

	public void OnBButtonPress(Player player)
	{
		RelockObstacle();
	}
}
