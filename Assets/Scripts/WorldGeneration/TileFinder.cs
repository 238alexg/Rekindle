using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu]
public class TileFinder : ScriptableObject
{
	public TileBase Wall;
	public TileBase Floor;
	public Obstacle Door;

	public WallOrientations HappyStoneWalls;
	public WallOrientations SadStoneWalls;

	public ItemActivation Activated;
	public ItemActivation NotActivatedYet;

	public GateKeeping Open;
	public GateKeeping Close;
}
