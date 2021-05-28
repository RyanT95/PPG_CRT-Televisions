using UnityEngine;

namespace TelevisionPVM
{
	public class PVM14Behaviour : TVBehaviour
	{
		//// Debug Stuff
		//// =============================================================================================
		//public readonly string Ruler = "Sprites/ruler.png";
		//public readonly float RulerScale = 2.381f;
		//public readonly string RulerView = "Sprites/RulerView.png";

		// Sprites
		// =============================================================================================
		protected SpriteRenderer RemoteButtonSprite;
		protected LightSprite RemoteButtonLight;

		// TV Lights
		// =============================================================================================
		protected static Color RemoteButtonColour;
		protected static Vector3 RemoteButtonPosition;

		// Everything Else
		// =============================================================================================
		protected GameObject RemoteButton;

		// ********************************
		// Constructor
		// ********************************
		public PVM14Behaviour()
		{
			Scale = 7.20f;              // PVM14 | ~34.0cm tall irl | ~37.5cm tall ingame
			Weight = 10.0f;             // PVM14 | ~16.7KG irl | 10.0KG ingame
			Health = 35.0f;
			DamageThreshold = 24.0f;

			ScreenColour = Color.white;
			OnButtonColour = Color.green;
			RemoteButtonColour = new Color(254.0f, 90.0f, 0.0f, 1.0f);

			ScreenPosition =		new Vector3(0.0f, 0.0326f, 0.0f);				// new Vector3(0.0f, 0.0324f, 0.0f);
			OnButtonPosition =		new Vector3(0.17866f, -0.19230f, 0.0f);			// 0.17866f, -0.19230f
			RemoteButtonPosition =	new Vector3(0.17866f, -0.20810f, 0.0f);			// 0.17866f, -0.20810f
			ButtonScale =			new Vector3(0.049f, 0.0245f, 0.0f);
			ScreenScale =			new Vector3(0.3648f, 0.2857f, 0.0f);            // new Vector3(0.3648f, 0.2857f, 0.0f);
		}

		// ********************************
		// Start
		// ********************************
		protected override void Start()
		{
			base.Start();

			RemoteButtonColour = new Color(254.0f, 90.0f, 0.0f, 1.0f);
			RemoteButtonPosition = new Vector3(0.17866f, -0.20810f, 0.0f);
		}

		// ********************************
		// Awake
		// ********************************
		protected override void Awake()
		{
			base.Awake();

			// Remote button sprite
			RemoteButton = new GameObject("Remote Button Sprite");
			RemoteButton.transform.SetParent(this.transform);
			RemoteButton.transform.localPosition = RemoteButtonPosition;
			RemoteButton.transform.localScale = ButtonScale;
			RemoteButtonSprite = RemoteButton.AddComponent<SpriteRenderer>();
			RemoteButtonSprite.sprite = GlowSprite;
			RemoteButtonSprite.sharedMaterial = ModAPI.FindMaterial("VeryBright");
			RemoteButtonSprite.color = RemoteButtonColour;
			RemoteButtonSprite.enabled = false;

			// Remote button light
			RemoteButtonLight = ModAPI.CreateLight(transform, Color.red);
			RemoteButtonLight.Color = RemoteButtonColour;
			RemoteButtonLight.Brightness = 10.0f;
			RemoteButtonLight.Radius = 0.01f;
			RemoteButtonLight.transform.localPosition = RemoteButtonPosition;
			RemoteButtonLight.SpriteRenderer.enabled = false;
		}

		// ********************************
		// TV turned on
		// ********************************
		protected override void DeviceOn()
		{
			base.DeviceOn();

			RemoteButtonSprite.enabled = true;
			RemoteButtonLight.SpriteRenderer.enabled = true;
		}

		// ********************************
		// TV turned off
		// ********************************
		protected override void DeviceOff()
		{
			base.DeviceOff();

			RemoteButtonSprite.enabled = false;
			RemoteButtonLight.SpriteRenderer.enabled = false;
		}
	}
}
