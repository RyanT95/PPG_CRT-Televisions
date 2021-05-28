using UnityEngine;

namespace TelevisionPVM
{
	public class TVBehaviour : DeviceBehaviour
	{
		// Sprites
		// =============================================================================================
		protected SpriteRenderer TvSprite;
		//public SpriteRenderer ScreenSprite;
		protected SpriteRenderer OnButtonSprite;

		protected Sprite PalSprite;
		protected Sprite GeometrySprite;
		protected Sprite ColourBarsSprite;

		protected LightSprite ScreenLight;
		protected LightSprite OnButtonLight;

		// TV Lights
		// =============================================================================================
		protected static Color ScreenColour;
		protected static Color OnButtonColour;

		protected static Vector3 ScreenPosition;
		protected static Vector3 OnButtonPosition;

		protected static Vector3 ButtonScale;
		protected static Vector3 ScreenScale;

		// Setters
		// =============================================================================================
		public void SetPalSprite(Sprite S) { this.PalSprite = S; }
		public void SetGeometrySprite(Sprite S) { this.GeometrySprite = S; }
		public void SetColourBarsSprite(Sprite S) { this.ColourBarsSprite = S; }

		// Everything Else
		// =============================================================================================
		protected GameObject Screen;
		protected GameObject OnButton;

		// ********************************
		// Constructor
		// ********************************
		public TVBehaviour()
		{
			ScreenColour = Color.white;
			OnButtonColour = Color.green;

			ScreenPosition = new Vector3(0.0f, 0.0326f, 0.0f);
			OnButtonPosition = new Vector3(0.17866f, -0.19230f, 0.0f);

			ButtonScale = new Vector3(0.049f, 0.0245f, 0.0f);
			ScreenScale = new Vector3(0.3648f, 0.2857f, 0.0f);
		}

		// ********************************
		// Start
		// ********************************
		protected override void Start()
		{
			base.Start();

			InvokeRepeating("Flicker", 0.1f, 0.08f);    // Call the Flicker() function every 0.08 seconds
		}

		// ********************************
		// Awake
		// ********************************
		protected override void Awake()
		{
			base.Awake();

			CreateContextMenuOptions();

			/*
			// Screen sprite
			Screen = new GameObject("Screen Sprite");
			Screen.transform.SetParent(this.transform);
			Screen.transform.localPosition = _tempScreenPosition;
			Screen.transform.localScale = _tempScreenScale;
			ScreenSprite = Screen.AddComponent<SpriteRenderer>();
			ScreenSprite.sprite = GlowSprite;
			ScreenSprite.sharedMaterial = ModAPI.FindMaterial("Sprites-Default");
			ScreenSprite.color = ScreenColour;
			ScreenSprite.enabled = false;
			*/

			// On button sprite
			OnButton = new GameObject("On Button Sprite");
			OnButton.transform.SetParent(this.transform);
			OnButton.transform.localPosition = OnButtonPosition;
			OnButton.transform.localScale = ButtonScale;
			OnButtonSprite = OnButton.AddComponent<SpriteRenderer>();
			OnButtonSprite.sprite = GlowSprite;
			OnButtonSprite.sharedMaterial = ModAPI.FindMaterial("VeryBright");
			OnButtonSprite.color = OnButtonColour;
			OnButtonSprite.enabled = false;

			// Screen light
			ScreenLight = ModAPI.CreateLight(transform, Color.red);
			ScreenLight.Color = ScreenColour;
			ScreenLight.Brightness = 0.5f;
			ScreenLight.Radius = 0.8f;
			ScreenLight.transform.localPosition = ScreenPosition;
			ScreenLight.SpriteRenderer.enabled = false;

			// On button light
			OnButtonLight = ModAPI.CreateLight(transform, Color.red);
			OnButtonLight.Color = OnButtonColour;
			OnButtonLight.Brightness = 10.0f;
			OnButtonLight.Radius = 0.01f;
			OnButtonLight.transform.localPosition = OnButtonPosition;
			OnButtonLight.SpriteRenderer.enabled = false;
		}

		// ********************************
		// Flicker screen light
		// ********************************
		public void Flicker()
		{
			ScreenLight.Brightness = UnityEngine.Random.Range(0.3f, 0.5f);
		}

		// ********************************
		// TV turned on
		// ********************************
		protected override void DeviceOn()
		{
			base.DeviceOn();

			ChangeSprite(OnSprite);
			//ScreenSprite.enabled = true;
			OnButtonSprite.enabled = true;

			ScreenLight.SpriteRenderer.enabled = true;
			OnButtonLight.SpriteRenderer.enabled = true;
		}

		// ********************************
		// TV turned off
		// ********************************
		protected override void DeviceOff()
		{
			base.DeviceOff();

			ChangeSprite(OffSprite);
			//ScreenSprite.enabled = false;
			OnButtonSprite.enabled = false;

			ScreenLight.SpriteRenderer.enabled = false;
			OnButtonLight.SpriteRenderer.enabled = false;
		}

		// ********************************
		// Create right click menu options
		// ********************************
		protected override void CreateContextMenuOptions()
		{
			base.CreateContextMenuOptions();

			this.Phys.ContextMenuOptions.Buttons.Add(new ContextMenuButton("geometry", "Geometry", "Geometry Test Screen", () =>
			{
				if (!this.Broken && this.Activated)
				{
					//ModAPI.Notify("Geometry");
					ChangeSprite(GeometrySprite);
				}
			}));

			this.Phys.ContextMenuOptions.Buttons.Add(new ContextMenuButton("colourBars", "Colour Bars", "SMPTE Colour Bars Screen", () =>
			{
				if (!this.Broken && this.Activated)
				{
					//ModAPI.Notify("Colour Bars");
					ChangeSprite(ColourBarsSprite);
				}
			}));

			this.Phys.ContextMenuOptions.Buttons.Add(new ContextMenuButton("pal", "PAL", "PAL Input Screen", () =>
			{
				if (!this.Broken && this.Activated)
				{
					//ModAPI.Notify("PAL");
					ChangeSprite(PalSprite);
				}
			}));
		}

		// ********************************
		// Changes displayed sprite of TV
		// ********************************
		protected override void ChangeSprite(Sprite sprite)
		{
			base.ChangeSprite(sprite);

			if (sprite == OffSprite)
				ScreenLight.Color = Color.clear;

			if (sprite == OnSprite)
				ScreenLight.Color = new Color(187.0f, 207.0f, 223.0f, 0.5f);

			if (sprite == GeometrySprite)
				ScreenLight.Color = Color.white;

			if (sprite == ColourBarsSprite)
				ScreenLight.Color = Color.yellow;

			if (sprite == PalSprite)
				ScreenLight.Color = Color.grey;

			if (sprite == ColourBarsSprite)
				ScreenLight.Color = Color.yellow;
		}

		// ********************************
		// DEBUG DRAW
		// ********************************
		public void OnWillRenderObject()
		{
			//draw line from this GameObject to some place else
			//ModAPI.Draw.Line(transform.position, new Vector3(76, 43));

			//Vector3 DBG_smallScreenOffset = transform.position + new Vector3(0, 0.0324f, 0);
			//Vector3 DBG_smallScreenSize = new Vector3(0.3648f, 0.2857f);
			//
			//Vector3 DBG_bigScreenOffset = transform.position + new Vector3(0, 0.0368f, 0);
			//Vector3 DBG_bigScreenSize = new Vector3(0.5369f, 0.4002f);
			//
			//
			//// Small screen
			//if (this.name == "PVM 14M4E")
			//{
			//	ModAPI.Draw.Rect(DBG_smallScreenOffset, DBG_smallScreenSize);
			//
			//	ModAPI.Draw.Circle(DBG_smallScreenOffset, 0.005f);
			//}
			//
			//// Big screen
			//if (this.name == "PVM 20M4E")
			//{
			//	ModAPI.Draw.Rect(DBG_bigScreenOffset, DBG_bigScreenSize);
			//
			//	ModAPI.Draw.Circle(DBG_bigScreenOffset, 0.005f);
			//}

			//// Draw all colliders
			//foreach (var item in GetComponents<Collider2D>())
			//{
			//	ModAPI.Draw.Collider(item);
			//}
		}
	}
}