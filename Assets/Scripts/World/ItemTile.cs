using System;
using UnityEngine;
using UnityEngine.Tilemaps;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class ItemTile : Tile
{
	public event Action<Obstacle> ObstacleLockChangedEvent = delegate { };

	public Item Solution;
	public Vector3Int Position;

	private void OnCollisionEnter2D(Collision2D collision)
	{
		Player player = collision.gameObject.GetComponent<Player>();
		//Detecting the Grid Position of Player
		if (player != null)
		{
			Debug.Log("Colliding with player: " + player);
		}
	}
	
	void OnPlayerWalkOver()
	{

	}

#if UNITY_EDITOR
	[MenuItem("Assets/CustomTiles/ItemTile")]
	public static void CreateItemTile()
	{
		string path = EditorUtility.SaveFilePanelInProject("Save Item Tile", "Obstacle", "Asset", "Save Item Tile", "Assets/Art/Tilemaps/Items");
		if (path == "")
			return;
		AssetDatabase.CreateAsset(CreateInstance<Obstacle>(), path);
	}
#endif
}
