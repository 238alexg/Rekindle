using System;
using UnityEngine;
using UnityEngine.Tilemaps;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class ItemTile : Tile, IAButtonHandler
{
	public event Action<ItemTile> ItemPickedUpEvent = delegate { };

	public Item Item;
	public Vector3Int Position;

	private void OnCollisionEnter2D(Collision2D collision)
	{
		Player player = collision.gameObject.GetComponent<Player>();
		//Detecting the Grid Position of Player
		if (player != null)
		{
			player.Inventory.Add(Item);
			Item = null;
			ItemPickedUpEvent(this);
		}
	}

	public void OnAButtonPress(Player player)
	{
		player.Inventory.Add(Item);
		Item = null;
		ItemPickedUpEvent(this);
	}

#if UNITY_EDITOR
	[MenuItem("Assets/CustomTiles/ItemTile")]
	public static void CreateItemTile()
	{
		string path = EditorUtility.SaveFilePanelInProject("Save Item Tile", "Obstacle", "Asset", "Save Item Tile", "Assets/Art/Tilemaps/Items");
		if (path == "")
			return;
		AssetDatabase.CreateAsset(CreateInstance<ItemTile>(), path);
	}
#endif
}
