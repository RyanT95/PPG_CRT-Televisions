using UnityEngine;

namespace TelevisionPVM
{
	public class ConsoleBehaviour : DeviceBehaviour
	{
		// Sprites
		// =============================================================================================
		protected SpriteRenderer OnButtonSprite;
		protected LightSprite OnButtonLight;

		// Console Buttons
		// =============================================================================================
		protected static Color OnButtonColour;
		protected static Vector3 OnButtonPosition;
		protected static Vector3 ButtonScale;
		protected GameObject OnButton;

		// ********************************
		// Start
		// ********************************
		protected override void Start()
		{
			base.Start();

			OnButtonColour = Color.green;
			OnButtonPosition = new Vector3(0.0f, 0.0f, 0.0f);
			ButtonScale = new Vector3(0.049f, 0.0245f, 0.0f);
		}

		// ********************************
		// Awake
		// ********************************
		protected override void Awake()
		{
			base.Awake();

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

			// On button light
			OnButtonLight = ModAPI.CreateLight(transform, Color.red);
			OnButtonLight.Color = OnButtonColour;
			OnButtonLight.Brightness = 10.0f;
			OnButtonLight.Radius = 0.01f;
			OnButtonLight.transform.localPosition = OnButtonPosition;
			OnButtonLight.SpriteRenderer.enabled = false;
		}

		// ********************************
		// Device turned on
		// ********************************
		protected override void DeviceOn()
		{
			base.DeviceOn();

			ChangeSprite(OnSprite);
			OnButtonSprite.enabled = true;
			OnButtonLight.SpriteRenderer.enabled = true;
		}

		// ********************************
		// Device turned off
		// ********************************
		protected override void DeviceOff()
		{
			base.DeviceOff();

			ChangeSprite(OffSprite);
			OnButtonSprite.enabled = false;
			OnButtonLight.SpriteRenderer.enabled = false;
		}
	}
}