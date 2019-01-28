using UnityEditor;
using UnityEngine;

public class ScreenSpaceDarkness : MonoBehaviour
{
	const int MaxLights = 10;
	const float MaxLightAlpha = 0.5f;

	public SpriteRenderer ScreenSpaceSprite;

	[Header("Test Light")]
	[SerializeField] Vector2 TestLight = Vector2.one;
	[SerializeField] float TestRadius = 1;
	[SerializeField] Color TestColor = new Color();

	readonly Vector4[] LightPositions = new Vector4[MaxLights];
	readonly float[] LightRadii = new float[MaxLights];
	readonly Color[] LightColors = new Color[MaxLights];

	readonly Vector2 ScreenSize = new Vector2(Screen.width, Screen.height);

	Material ScreenSpaceMaterial;
	RenderTexture RenderTexture;

	int ActiveLightsThisFrame;
	
	void Start()
	{
		Debug.Assert(ScreenSpaceSprite != null, "No sprite renderer attached to ScreenSpaceDarkness script");
		ScreenSpaceMaterial = ScreenSpaceSprite.material;
		ScreenSpaceMaterial.SetVector("_TextureSize", new Vector4(Screen.width, Screen.height));
	}
	
	public void UpdateTextureWithLights()
	{
		//AddLight(TestLight, TestRadius, TestColor);

		ScreenSpaceMaterial.SetColorArray("_LightColors", LightColors);
		ScreenSpaceMaterial.SetFloatArray("_LightRadii", LightRadii);
		ScreenSpaceMaterial.SetVectorArray("_LightPositions", LightPositions);
		ScreenSpaceMaterial.SetVector("_UVTiling", new Vector4()); // Palmer todo: Can you do a gentle oscillating wave here? For mist

		ActiveLightsThisFrame = 0;
	}

	public void AddLight(Vector2 position, float radius, Color lightColor)
	{
		Debug.Assert(ActiveLightsThisFrame < MaxLights, "Trying to add too many lights this frame!");

		Debug.Assert(position.x >= 0 && position.x <= ScreenSize.x && 
		             position.y >= 0 && position.y <= ScreenSize.y, "Light position is out of bounds!");
		Debug.Assert(radius > 0 && radius < 1, "Light radius is out of bounds!");
		Debug.Assert(lightColor.a > 0 && lightColor.a <= MaxLightAlpha, "Light alpha is out of bounds!");
		
		LightPositions[ActiveLightsThisFrame] = position;
		LightRadii[ActiveLightsThisFrame] = radius;
		LightColors[ActiveLightsThisFrame] = lightColor;

		ActiveLightsThisFrame++;
	}
}
