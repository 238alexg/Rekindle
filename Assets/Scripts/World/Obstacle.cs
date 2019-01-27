using System;
using UnityEngine;
using UnityEngine.Tilemaps;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class Obstacle : Tile, IAButtonHandler, IBButtonHandler
{
	public event Action<Obstacle> ObstacleLockChangedEvent = delegate { };

	public Item Solution;
	public Vector3Int Position;

	public bool Unlocked = false;
	
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
		ObstacleLockChangedEvent(this);
	}

	public void SubscribeToLever()
	{
		// TODO: Subscribe to level pulled event
	}

	void UnlockObstacle()
	{
		Unlocked = true;
		ObstacleLockChangedEvent(this);
	}

	public void OnAButtonPress(Player player)
	{
		TryUnlockObstacle(player);
	}

	public void OnBButtonPress(Player player)
	{
		RelockObstacle();
	}

	void OnDestroy()
	{
		// TODO: Unsubscribe to level pulled event
	}

#if UNITY_EDITOR
	[MenuItem("Assets/CustomTiles/ObstacleTile")]
	public static void CreateObstacleTile()
	{
		string path = EditorUtility.SaveFilePanelInProject("Save Obstacle Tile", "Obstacle", "Asset", "Save Road Tile", "Assets/Art/Tilemaps/Obstacles");
		if (path == "")
			return;
		AssetDatabase.CreateAsset(CreateInstance<Obstacle>(), path);
	}
#endif
}
