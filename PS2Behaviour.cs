using UnityEngine;

namespace TelevisionPVM
{
	public class PS2Behaviour : ConsoleBehaviour
	{
		// Sprites
		// =============================================================================================
		protected SpriteRenderer DVDButtonSprite;
		protected LightSprite DVDButtonLight;

		// Console Buttons
		// =============================================================================================
		protected static Color DVDButtonColour;
		protected static Vector3 DVDButtonPosition;
		protected GameObject DVDButton;

		// ********************************
		// Constructor
		// ********************************
		public PS2Behaviour()
		{
			Scale = 11.80f;     // PS2	 | ~30.0cm tall irl | ~33.0cm tall ingame
			Weight = 2.20f;
			Health = 5.0f;
			DamageThreshold = 2.0f;

			OnButtonColour = Color.green;
			DVDButtonColour = Color.blue;

			OnButtonPosition = new Vector3(0.1854f, 0.043f, 0.0f);
			DVDButtonPosition = new Vector3(0.1854f, 0.008f, 0.0f);
			ButtonScale = new Vector3(0.049f, 0.0245f, 0.0f);
		}

		// ********************************
		// Awake
		// ********************************
		protected override void Awake()
		{
			base.Awake();

			// DVD button sprite
			DVDButton = new GameObject("DVD Button Sprite");
			DVDButton.transform.SetParent(this.transform);
			DVDButton.transform.localPosition = DVDButtonPosition;
			DVDButton.transform.localScale = ButtonScale;
			DVDButtonSprite = DVDButton.AddComponent<SpriteRenderer>();
			DVDButtonSprite.sprite = GlowSprite;
			DVDButtonSprite.sharedMaterial = ModAPI.FindMaterial("VeryBright");
			DVDButtonSprite.color = DVDButtonColour;
			DVDButtonSprite.enabled = false;

			// DVD button light
			DVDButtonLight = ModAPI.CreateLight(transform, Color.red);
			DVDButtonLight.Color = DVDButtonColour;
			DVDButtonLight.Brightness = 12.0f;
			DVDButtonLight.Radius = 0.01f;
			DVDButtonLight.transform.localPosition = DVDButtonPosition;
			DVDButtonLight.SpriteRenderer.enabled = false;
		}

		// ********************************
		// Device turned on
		// ********************************
		protected override void DeviceOn()
		{
			base.DeviceOn();

			DVDButtonSprite.enabled = true;
			DVDButtonLight.SpriteRenderer.enabled = true;
		}

		// ********************************
		// Device turned off
		// ********************************
		protected override void DeviceOff()
		{
			base.DeviceOff();

			DVDButtonSprite.enabled = false;
			DVDButtonLight.SpriteRenderer.enabled = false;
		}
	}
}