using UnityEngine;
using System.Collections.Generic;

public abstract class Space : MonoBehaviour
{
    public Vector2 Position;
    public HashSet<Entity> Entities;
}
