using UnityEngine;
using System;

namespace TelevisionPVM
{
	public class PVMBehaviour : MonoBehaviour
	{
		// Debug Stuff
		// =============================================================================================
		/*
		public readonly string Ruler = "Sprites/ruler.png";
		public readonly float RulerScale = 2.381f;
		public readonly string RulerView = "Sprites/RulerView.png";
		*/

		// Sprites
		// =============================================================================================
		private Sprite GlowSprite;

		private Sprite TvSmallOn;
		private Sprite TvSmallPal;
		private Sprite TvSmallGeometry;
		private Sprite TvSmallColourBars;
		private Sprite TvSmallOff;
		private Sprite TvSmallBroken;
		private Sprite TvSmallShock;
		private Sprite TvSmallFire;
		private Sprite TvSmallWater;
		private Sprite TvSmallExplode;

		private Sprite TvBigOn;
		private Sprite TvBigPal;
		private Sprite TvBigGeometry;
		private Sprite TvBigColourBars;
		private Sprite TvBigOff;
		private Sprite TvBigBroken;
		private Sprite TvBigShock;
		private Sprite TvBigFire;
		private Sprite TvBigWater;
		private Sprite TvBigExplode;

		// TV Properties
		// =============================================================================================
		private readonly float TvSmallScale = 7.20f;     // PVM14 | ~34.0cm tall irl | ~37.5cm tall ingame
		private readonly float TvSmallWeight = 10.0f;    // PVM14 | ~16.7KG irl | 10.0KG ingame

		private readonly float TvBigScale = 6.27f;       // PVM20 | ~46.0cm tall irl | ~49.0cm tall ingame
		private readonly float TvBigWeight = 20.0f;      // PVM20 | ~30.0KG irl | 20.0KG ingame

		private readonly float TvSmallHealth = 35.0f;
		private readonly float TvBigHealth = 50.0f;

		private readonly float SmallDamageThreshold = 24.0f;    // 33.5
		private readonly float BigDamageThreshold = 43.0f;      // 60.0

		// TV Lights
		// =============================================================================================
		private static Color ScreenColour = Color.white; //new Color(187.0f, 207.0f, 223.0f, 0.5f);
		private static Color OnButtonColour = Color.green;
		private static Color RemoteButtonColour = new Color(254.0f, 90.0f, 0.0f, 1.0f);

		private static Vector3 TvSmallScreenPosition = new Vector3(0.0f, 0.0326f, 0.0f);				// new Vector3(0.0f, 0.0324f, 0.0f);
		private static Vector3 TvSmallOnButtonPosition = new Vector3(0.17866f, -0.19230f, 0.0f);		// 0.17866f, -0.19230f
		private static Vector3 TvSmallRemoteButtonPosition = new Vector3(0.17866f, -0.20810f, 0.0f);	// 0.17866f, -0.20810f
				
		private static Vector3 TvSmallButtonScale = new Vector3(0.049f, 0.0245f, 0.0f);
		private static Vector3 TvSmallScreenScale = new Vector3(0.3648f, 0.2857f, 0.0f);				// new Vector3(0.3648f, 0.2857f, 0.0f);

		private static Vector3 TvBigScreenPosition = new Vector3(0.0f, 0.0370f, 0.0f);					// new Vector3(0.0f, 0.0368f, 0.0f);
		private static Vector3 TvBigOnButtonPosition = new Vector3(0.23700f, -0.25769f, 0.0f);			// 0.23700f, -0.25769f
		private static Vector3 TvBigRemoteButtonPosition = new Vector3(0.23700f, -0.27550f, 0.0f);		// 0.23700f, -0.27550f

		private static Vector3 TvBigButtonScale = new Vector3(0.051f, 0.0255f, 0.0f);
		private static Vector3 TvBigScreenScale = new Vector3(0.5369f, 0.4002f, 0.0f);					// new Vector3(0.5369f, 0.4002f, 0.0f);

		// Getters
		// =============================================================================================
		public float GetTvSmallScale	{ get { return TvSmallScale; } }
		public float GetTvSmallWeight	{ get { return TvSmallWeight; } }
		public float GetTvBigScale		{ get { return TvBigScale; } }
		public float GetTvBigWeight		{ get { return TvBigWeight; } }

		// Setters
		// =============================================================================================
		public void SetGlowSprite(Sprite S)					{ this.GlowSprite = S; }

		public void SetTvSmallOnSprite(Sprite S)			{ this.TvSmallOn = S; }
		public void SetTvSmallPalSprite(Sprite S)			{ this.TvSmallPal = S; }
		public void SetTvSmallGeometrySprite(Sprite S)		{ this.TvSmallGeometry = S; }
		public void SetTvSmallColourBarsSprite(Sprite S)	{ this.TvSmallColourBars = S; }
		public void SetTvSmallOffSprite(Sprite S)			{ this.TvSmallOff = S; }
		public void SetTvSmallBrokenSprite(Sprite S)		{ this.TvSmallBroken = S; }
		public void SetTvSmallShockSprite(Sprite S)			{ this.TvSmallShock = S; }
		public void SetTvSmallFireSprite(Sprite S)			{ this.TvSmallFire = S; }
		public void SetTvSmallWaterSprite(Sprite S)			{ this.TvSmallWater = S; }
		public void SetTvSmallExplodeSprite(Sprite S)		{ this.TvSmallExplode = S; }

		public void SetTvBigOnSprite(Sprite S)				{ this.TvBigOn = S; }
		public void SetTvBigPalSprite(Sprite S)				{ this.TvBigPal = S; }
		public void SetTvBigGeometrySprite(Sprite S)		{ this.TvBigGeometry = S; }
		public void SetTvBigColourBarsSprite(Sprite S)		{ this.TvBigColourBars = S; }
		public void SetTvBigOffSprite(Sprite S)				{ this.TvBigOff = S; }
		public void SetTvBigBrokenSprite(Sprite S)			{ this.TvBigBroken = S; }
		public void SetTvBigShockSprite(Sprite S)			{ this.TvBigShock = S; }
		public void SetTvBigFireSprite(Sprite S)			{ this.TvBigFire = S; }
		public void SetTvBigWaterSprite(Sprite S)			{ this.TvBigWater = S; }
		public void SetTvBigExplodeSprite(Sprite S)			{ this.TvBigExplode = S; }

		// Cached Values
		// =============================================================================================
		private float _tempHealth;
		private float _tempInitialHealth;

		private Vector3 _tempScreenPosition = new Vector3(0, 0, 0);
		private Vector3 _tempScreenScale = new Vector3(0, 0, 0);

		private Vector3 _tempOnButtonPosition = new Vector3(0, 0, 0);
		private Vector3 _tempRemoteButtonPosition = new Vector3(0, 0, 0);
		private Vector3 _tempButtonScale = new Vector3(0, 0, 0);

		// Everything Else
		// =============================================================================================
		private bool Broken = false;
		private bool Activated = false;

		private PhysicalBehaviour Phys;	

		private GameObject Screen;
		private GameObject OnButton;
		private GameObject RemoteButton;

		private SpriteRenderer TvSprite;
		//private SpriteRenderer ScreenSprite;
		private SpriteRenderer OnButtonSprite;
		private SpriteRenderer RemoteButtonSprite;

		private LightSprite ScreenLight;
		private LightSprite RemoteButtonLight;
		private LightSprite OnButtonLight;
		
		// ********************************
		// Start
		// ********************************
		private void Start()
		{			
			TvSprite = gameObject.GetComponent<SpriteRenderer>();
			
			InvokeRepeating("Flicker", 0.1f, 0.08f);	// Call the Flicker() function every 0.08 seconds
		}

		// ********************************
		// Awake
		// ********************************
		private void Awake()
		{
			this.Phys = this.GetComponent<PhysicalBehaviour>();

			ChangeType();
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
			OnButton.transform.localPosition = _tempOnButtonPosition;
			OnButton.transform.localScale = _tempButtonScale;
			OnButtonSprite = OnButton.AddComponent<SpriteRenderer>();
			OnButtonSprite.sprite = GlowSprite;
			OnButtonSprite.sharedMaterial = ModAPI.FindMaterial("VeryBright");
			OnButtonSprite.color = OnButtonColour;
			OnButtonSprite.enabled = false;

			// Remote button sprite
			RemoteButton = new GameObject("Remote Button Sprite");
			RemoteButton.transform.SetParent(this.transform);
			RemoteButton.transform.localPosition = _tempRemoteButtonPosition;
			RemoteButton.transform.localScale = _tempButtonScale;
			RemoteButtonSprite = RemoteButton.AddComponent<SpriteRenderer>();
			RemoteButtonSprite.sprite = GlowSprite;
			RemoteButtonSprite.sharedMaterial = ModAPI.FindMaterial("VeryBright");
			RemoteButtonSprite.color = RemoteButtonColour;
			RemoteButtonSprite.enabled = false;

			// Screen light
			ScreenLight = ModAPI.CreateLight(transform, Color.red);
			ScreenLight.Color = ScreenColour;
			ScreenLight.Brightness = 0.5f;
			ScreenLight.Radius = 0.8f;
			ScreenLight.transform.localPosition = _tempScreenPosition;
			ScreenLight.SpriteRenderer.enabled = false;
			
			// On button light
			OnButtonLight = ModAPI.CreateLight(transform, Color.red);
			OnButtonLight.Color = OnButtonColour;
			OnButtonLight.Brightness = 10.0f; //12
			OnButtonLight.Radius = 0.01f;
			OnButtonLight.transform.localPosition = _tempOnButtonPosition;
			OnButtonLight.SpriteRenderer.enabled = false;

			// Remote button light
			RemoteButtonLight = ModAPI.CreateLight(transform, Color.red);
			RemoteButtonLight.Color = RemoteButtonColour;
			RemoteButtonLight.Brightness = 10.0f; //12
			RemoteButtonLight.Radius = 0.01f;
			RemoteButtonLight.transform.localPosition = _tempRemoteButtonPosition;
			RemoteButtonLight.SpriteRenderer.enabled = false;
		}

		// ********************************
		// Update
		// ********************************
		private void Update()
		{
			UpdateDamage();
		}

		// ********************************
		// Evaluate various damage types
		// ********************************
		private void UpdateDamage()
		{
			float deltaTime = Time.deltaTime;

			if (this._tempHealth <= 0.0f && !this.Broken)
			{
				Break("standard");
				ChangeSprite("Broken");
			}

			// TV electrocuted
			if (this.Phys.charge > 90.0f && !this.Broken)
			{
				this._tempHealth -= deltaTime * (_tempInitialHealth / 2.0f);

				//ModAPI.Notify("HP:" + this._tempHealth);
				//ModAPI.Notify("CRG: " + this.Phys.charge);

				if (this._tempHealth < 0.0f && this.Activated)
				{
					Break("electric");
					ChangeSprite("Shock");
				}
			}

			// TV wet
			if (this.Phys.Wetness > 0.0f && this.Activated && !this.Broken)
			{
				this._tempHealth -= deltaTime * _tempInitialHealth;

				//ModAPI.Notify("HP:" + this._tempHealth);
				//ModAPI.Notify("WTN: " + this.Phys.Wetness);

				if (this._tempHealth < 0.0f && this.Activated)
				{
					Break("water");
					ChangeSprite("Water");
				}
			}

			// TV burning
			if (this.Phys.burnIntensity > 0.5f && !this.Broken)
			{
				this._tempHealth -= deltaTime * (_tempInitialHealth / 3.5f);

				//ModAPI.Notify("HP:" + this._tempHealth);
				//ModAPI.Notify("BRN: " + this.Phys.burnIntensity);

				if (this._tempHealth < 0.0f)
				{
					Break("burn");
					ChangeSprite("Fire");
				}
			}
		}

		// ********************************
		// Evaluate activation of TV
		// ********************************
		private void UpdateActivation()
		{
			//this.ScreenSprite.enabled = this.Activated && !this.Broken;

			if (this.Activated && !this.Broken)
				this.TvOn();

			else
				this.TvOff();

			if (!this.Broken)
				return;

			ChangeSprite("Explode");	// Other damage, i.e. guns and bombs
		}

		// ********************************
		// Flicker screen light
		// ********************************
		private void Flicker()
		{
			ScreenLight.Brightness = UnityEngine.Random.Range(0.3f, 0.5f);
		}

		// ********************************
		// TV breaks
		// ********************************
		private void Break(string damageType = "standard")
		{
			if (this._tempHealth > 0.0f && this.Broken)
				this._tempHealth = 0.0f;

			if (this.Broken)
				return;

			this.Activated = false;
			this.Broken = true;

			switch (damageType)
			{
				case "standard":
					{
						ModAPI.CreateParticleEffect("BrokenElectronicsSpark", this.transform.position);
						break;
					}

				case "electric":
					{
						ModAPI.CreateParticleEffect("FuseBlown", this.transform.position);
						break;
					}

				case "water":
					{
						ModAPI.CreateParticleEffect("Spark", this.transform.position);
						break;
					}

				case "burn":
					{
						ModAPI.CreateParticleEffect("FuseBlown", this.transform.position);
						break;
					}

				case "explode":
					{
						ModAPI.CreateParticleEffect("FuseBlown", this.transform.position);
						break;
					}

				default:
					{
						ModAPI.Notify("Damage Type: " + damageType + " not found.");
						break;
					}
			}

			this.UpdateActivation();
		}

		// ********************************
		// TV turned on
		// ********************************
		private void TvOn()
		{
			ChangeSprite("On");
			this.Activated = true;
			//ScreenSprite.enabled = true;
			OnButtonSprite.enabled = true;
			RemoteButtonSprite.enabled = true;

			ScreenLight.SpriteRenderer.enabled = true;
			OnButtonLight.SpriteRenderer.enabled = true;
			RemoteButtonLight.SpriteRenderer.enabled = true;
		}

		// ********************************
		// TV turned off
		// ********************************
		private void TvOff()
		{
			ChangeSprite("Off");
			this.Activated = false;
			//ScreenSprite.enabled = false;
			OnButtonSprite.enabled = false;
			RemoteButtonSprite.enabled = false;

			ScreenLight.SpriteRenderer.enabled = false;
			OnButtonLight.SpriteRenderer.enabled = false;
			RemoteButtonLight.SpriteRenderer.enabled = false;
		}

		// ********************************
		// TV is used in game
		// ********************************
		private void Use()
		{
			if (!base.enabled || this.Broken) 
				return;
			
			this.Activated = !this.Activated;
			
			this.UpdateActivation();

			//ModAPI.Notify("HP: " + this._tempHealth);
			//ModAPI.Notify("CRG: " + this.Phys.charge);
			//ModAPI.Notify("BRN: " + this.Phys.burnIntensity);
			//ModAPI.Notify("WTN: " + this.Phys.Wetness);
		}

		// ********************************
		// Create right click menu options
		// ********************************
		private void CreateContextMenuOptions()
		{
			this.Phys.ContextMenuOptions.Buttons.Add(new ContextMenuButton("turnOff", "Turn Off", "Turn TV Off", () =>
			{
				if (!this.Broken && this.Activated)
				{
					//ModAPI.Notify("Turned Off");

					this.Activated = false;
					this.UpdateActivation();
				}
			}));

			this.Phys.ContextMenuOptions.Buttons.Add(new ContextMenuButton("turnOn", "Turn On", "Turn TV On", () =>
			{
				if (!this.Broken && !this.Activated)
				{
					//ModAPI.Notify("No Input");

					this.Activated = true;
					this.UpdateActivation();
				}
			}));

			this.Phys.ContextMenuOptions.Buttons.Add(new ContextMenuButton("geometry", "Geometry", "Geometry Test Screen", () =>
			{
				if (!this.Broken && this.Activated)
				{
					//ModAPI.Notify("Geometry");
					ChangeSprite("Geometry");
				}
			}));

			this.Phys.ContextMenuOptions.Buttons.Add(new ContextMenuButton("colourBars", "Colour Bars", "SMPTE Colour Bars Screen", () =>
			{
				if (!this.Broken && this.Activated)
				{
					//ModAPI.Notify("Colour Bars");
					ChangeSprite("ColourBars");
				}
			}));

			this.Phys.ContextMenuOptions.Buttons.Add(new ContextMenuButton("pal", "PAL", "PAL Input Screen", () =>
			{
				if (!this.Broken && this.Activated)
				{
					//ModAPI.Notify("PAL");
					ChangeSprite("Pal");
				}
			}));
		}

		// ********************************
		// Changes displayed sprite of TV
		// ********************************
		private void ChangeSprite(string pvmState)
		{
			// Small TV
			if (this.name == "PVM 14M4E")
			{
				switch (pvmState)
				{
					case "Off":
						this.GetComponent<SpriteRenderer>().sprite = TvSmallOff;
						ScreenLight.Color = Color.clear;
						break;

					case "On":
						this.GetComponent<SpriteRenderer>().sprite = TvSmallOn;
						ScreenLight.Color = new Color(187.0f, 207.0f, 223.0f, 0.5f);
						break;

					case "Geometry":
						this.GetComponent<SpriteRenderer>().sprite = TvSmallGeometry;
						ScreenLight.Color = Color.white;
						break;

					case "ColourBars":
						this.GetComponent<SpriteRenderer>().sprite = TvSmallColourBars;
						ScreenLight.Color = Color.yellow;
						break;

					case "Pal":
						this.GetComponent<SpriteRenderer>().sprite = TvSmallPal;
						ScreenLight.Color = Color.grey;
						break;

					case "Broken":
						this.GetComponent<SpriteRenderer>().sprite = TvSmallBroken;
						break;

					case "Shock":
						this.GetComponent<SpriteRenderer>().sprite = TvSmallShock;
						break;

					case "Fire":
						this.GetComponent<SpriteRenderer>().sprite = TvSmallFire;
						break;

					case "Water":
						this.GetComponent<SpriteRenderer>().sprite = TvSmallWater;
						break;

					case "Explode":
						this.GetComponent<SpriteRenderer>().sprite = TvSmallExplode;
						break;

					default:
						ModAPI.Notify("Sprite not found! Loading default sprite.");
						this.GetComponent<SpriteRenderer>().sprite = TvSmallOff;
						ScreenLight.Color = Color.grey;
						break;
				}

			}

			// Big TV
			if (this.name == "PVM 20M4E")
			{
				switch (pvmState)
				{
					case "Off":
						this.GetComponent<SpriteRenderer>().sprite = TvBigOff;
						ScreenLight.Color = Color.clear;
						break;

					case "On":
						this.GetComponent<SpriteRenderer>().sprite = TvBigOn;
						ScreenLight.Color = new Color(187.0f, 207.0f, 223.0f, 0.5f);
						break;

					case "Geometry":
						this.GetComponent<SpriteRenderer>().sprite = TvBigGeometry;
						ScreenLight.Color = Color.white;
						break;

					case "ColourBars":
						this.GetComponent<SpriteRenderer>().sprite = TvBigColourBars;
						ScreenLight.Color = Color.yellow;
						break;

					case "Pal":
						this.GetComponent<SpriteRenderer>().sprite = TvBigPal;
						ScreenLight.Color = Color.grey;
						break;

					case "Broken":
						this.GetComponent<SpriteRenderer>().sprite = TvBigBroken;
						break;

					case "Shock":
						this.GetComponent<SpriteRenderer>().sprite = TvBigShock;
						break;

					case "Fire":
						this.GetComponent<SpriteRenderer>().sprite = TvBigFire;
						break;

					case "Water":
						this.GetComponent<SpriteRenderer>().sprite = TvBigWater;
						break;

					case "Explode":
						this.GetComponent<SpriteRenderer>().sprite = TvBigExplode;
						break;

					default:
						ModAPI.Notify("Sprite not found! Loading default sprite.");
						this.GetComponent<SpriteRenderer>().sprite = TvBigOff;
						ScreenLight.Color = Color.grey;
						break;
				}
			}
		}

		// ********************************
		// Changes values based on TV size
		// ********************************
		private void ChangeType()
		{
			// Small TV
			if (this.name == "PVM 14M4E")
			{
				_tempHealth = TvSmallHealth;
				_tempInitialHealth = TvSmallHealth;

				_tempScreenPosition = TvSmallScreenPosition;
				_tempScreenScale = TvSmallScreenScale;

				_tempOnButtonPosition = TvSmallOnButtonPosition;
				_tempRemoteButtonPosition = TvSmallRemoteButtonPosition;
				_tempButtonScale = TvSmallButtonScale;
			}

			// Big TV
			if (this.name == "PVM 20M4E")
			{
				_tempHealth = TvBigHealth;
				_tempInitialHealth = TvBigHealth;

				_tempScreenPosition = TvBigScreenPosition;
				_tempScreenScale = TvBigScreenScale;

				_tempOnButtonPosition = TvBigOnButtonPosition;
				_tempRemoteButtonPosition = TvBigRemoteButtonPosition;
				_tempButtonScale = TvBigButtonScale;
			}
		}

		// ********************************
		// TV collides with object
		// ********************************
		private void OnCollisionEnter2D(Collision2D collision)
		{
			//ModAPI.Notify("Normal Impulse: " + Math.Round(collision.GetContact(0).normalImpulse));

			if (this.name == "PVM 14M4E" && (double)collision.contacts[0].normalImpulse <= (double)this.SmallDamageThreshold)
				return;

			if (this.name == "PVM 20M4E" && (double)collision.contacts[0].normalImpulse <= (double)this.BigDamageThreshold)
				return;

			if (this._tempHealth > 0.0f)
				this._tempHealth -= (collision.contacts[0].normalImpulse);
		}

		// ********************************
		// TV hit by EMP
		// ********************************
		private void OnEMPHit()
		{
			ModAPI.CreateParticleEffect("BigZap", this.transform.position);
			this.Break("electric");
		}

		// ********************************
		// TV is shot
		// ********************************
		private void Shot(global::Shot shot)
		{
			this.Break("standard");
		}

		// ********************************
		// TV hit by shrapnel
		// ********************************
		private void OnFragmentHit(float force)
		{
			//ModAPI.Notify("OnFragmentHit() Hit! " + force);

			// Note, depending on distance from explosion multiple fragments may hit.

			// For smaller explosives
			// Grenade force is 0.8 so one piece of shrapnel will take off ~9.5 hp. So it will take ~3.7 pieces to break small TV & ~2.5 for big TV.
			if (force < 2)
				this._tempHealth -= (force * 250) / 21;

			// For larger explosives
			// TNT force is 6 so one piece of shrapnel will take off ~28.6 hp. So it will take ~1.2 pieces to break small TV and ~1.8 for big TV.
			else
				this._tempHealth -= (force * 100) / 21;

			if (this._tempHealth <= 0)
			{ 
				this.Break("explode");
				//this.Broken = true;
			} 
		}

		// ********************************
		// DEBUG DRAW
		// ********************************
		private void OnWillRenderObject()
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
