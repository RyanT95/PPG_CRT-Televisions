using UnityEngine;

namespace TelevisionPVM
{
	public class TelevisionPVM
	{
		// Properties
		// =============================================================================================
		private static readonly float PVM14Scale = 7.20f;    // PVM14 | ~34.0cm tall irl | ~37.5cm tall ingame
		private static readonly float PVM20Scale = 6.27f;    // PVM20 | ~46.0cm tall irl | ~49.0cm tall ingame
		private static readonly float PS2Scale = 11.80f;     // PS2	 | ~30.0cm tall irl | ~33.0cm tall ingame

		// Thumbnails
		// =============================================================================================
		private static readonly string PVM20View =	"Sprites/PVM20View.png";
		private static readonly string PVM14View =	"Sprites/PVM14View.png";
		private static readonly string PS2View =	"Sprites/PS2View.png";
		//private static readonly string RulerView =		"Sprites/RulerView.png";

		// Sprites
		// =============================================================================================
		private static readonly Sprite GlowSprite =			ModAPI.LoadSprite("Sprites/glowSprite.png");

		// PVM14
		private static readonly Sprite PVM14On =			ModAPI.LoadSprite("Sprites/PVM14/PVM14_ON.png", PVM14Scale, true);
		private static readonly Sprite PVM14Pal =			ModAPI.LoadSprite("Sprites/PVM14/PVM14_ON-PAL.png", PVM14Scale, true);
		private static readonly Sprite PVM14Geometry =		ModAPI.LoadSprite("Sprites/PVM14/PVM14_ON-Geometry.png", PVM14Scale, true);
		private static readonly Sprite PVM14ColourBars =	ModAPI.LoadSprite("Sprites/PVM14/PVM14_ON-Colour-Bars.png", PVM14Scale, true);
		private static readonly Sprite PVM14Off =			ModAPI.LoadSprite("Sprites/PVM14/PVM14_OFF.png", PVM14Scale, true);
		private static readonly Sprite PVM14Broken =		ModAPI.LoadSprite("Sprites/PVM14/PVM14_Destroyed.png", PVM14Scale, true);
		private static readonly Sprite PVM14Shock =			ModAPI.LoadSprite("Sprites/PVM14/PVM14_Destroyed-Shock.png", PVM14Scale, true);
		private static readonly Sprite PVM14Fire =			ModAPI.LoadSprite("Sprites/PVM14/PVM14_Destroyed-Fire.png", PVM14Scale, true);
		private static readonly Sprite PVM14Water =			ModAPI.LoadSprite("Sprites/PVM14/PVM14_Destroyed-Water.png", PVM14Scale, true);
		private static readonly Sprite PVM14Explode =		ModAPI.LoadSprite("Sprites/PVM14/PVM14_Destroyed-Explosion.png", PVM14Scale, true);

		// PVM20
		private static readonly Sprite PVM20On =			ModAPI.LoadSprite("Sprites/PVM20/PVM20_ON.png", PVM20Scale, true);
		private static readonly Sprite PVM20Pal =			ModAPI.LoadSprite("Sprites/PVM20/PVM20_ON-PAL.png", PVM20Scale, true);
		private static readonly Sprite PVM20Geometry =		ModAPI.LoadSprite("Sprites/PVM20/PVM20_ON-Geometry.png", PVM20Scale, true);
		private static readonly Sprite PVM20ColourBars =	ModAPI.LoadSprite("Sprites/PVM20/PVM20_ON-Colour-Bars.png", PVM20Scale, true);
		private static readonly Sprite PVM20Off =			ModAPI.LoadSprite("Sprites/PVM20/PVM20_OFF.png", PVM20Scale, true);
		private static readonly Sprite PVM20Broken =		ModAPI.LoadSprite("Sprites/PVM20/PVM20_Destroyed.png", PVM20Scale, true);
		private static readonly Sprite PVM20Shock =			ModAPI.LoadSprite("Sprites/PVM20/PVM20_Destroyed-Shock.png", PVM20Scale, true);
		private static readonly Sprite PVM20Fire =			ModAPI.LoadSprite("Sprites/PVM20/PVM20_Destroyed-Fire.png", PVM20Scale, true);
		private static readonly Sprite PVM20Water =			ModAPI.LoadSprite("Sprites/PVM20/PVM20_Destroyed-Water.png", PVM20Scale, true);
		private static readonly Sprite PVM20Explode =		ModAPI.LoadSprite("Sprites/PVM20/PVM20_Destroyed-Explosion.png", PVM20Scale, true);

		// PS2
		private static readonly Sprite PS2On =				ModAPI.LoadSprite("Sprites/PS2/PS2_ON.png", PS2Scale, true);
		private static readonly Sprite PS2Off =				ModAPI.LoadSprite("Sprites/PS2/PS2_OFF.png", PS2Scale, true);
		private static readonly Sprite PS2Broken =			ModAPI.LoadSprite("Sprites/PS2/PS2_Destroyed.png", PS2Scale, true);
		private static readonly Sprite PS2Shock =			ModAPI.LoadSprite("Sprites/PS2/PS2_Destroyed-Shock.png", PS2Scale, true);
		private static readonly Sprite PS2Fire =			ModAPI.LoadSprite("Sprites/PS2/PS2_Destroyed-Fire.png", PS2Scale, true);
		private static readonly Sprite PS2Water =			ModAPI.LoadSprite("Sprites/PS2/PS2_Destroyed-Water.png", PS2Scale, true);
		private static readonly Sprite PS2Explode =			ModAPI.LoadSprite("Sprites/PS2/PS2_Destroyed-Explosion.png", PS2Scale, true);

		// ********************************
		// Main
		// ********************************
		public static void Main()
		{
			// ===============
			// PVM 14
			// ===============
			ModAPI.Register(
				new Modification()
				{
					OriginalItem = ModAPI.FindSpawnable("Metal Cube"),
					NameOverride = "PVM 14M4E",
					NameToOrderByOverride = "Text Display PVM",
					DescriptionOverride = "Professional Video Monitor with 14 inch HR Trinitron display.",
					CategoryOverride = ModAPI.FindCategory("Machinery"),
					ThumbnailOverride = ModAPI.LoadSprite(PVM14View, 5f),

					AfterSpawn = (Instance) =>
					{
						// NOTE: 
						// Using Instance.FixColliders(); or manually deleting the Collider2D and adding a new one
						// in order to fix the size of the collider breaks something in the game object, causing the 
						// Tesla coil to not work when near the modified object. I've seen this in a lot of mods, so
						// it's not a local issue. I've used the below method to keep the original collider, by simply
						// setting its size to that of the modified sprite.

						var pvm14 = Instance.GetOrAddComponent<PVM14Behaviour>();
						var sprite = Instance.GetComponent<SpriteRenderer>();
						var collider = Instance.GetComponent<BoxCollider2D>();
						var behaviour = Instance.GetOrAddComponent<PhysicalBehaviour>();

						sprite.sprite = PVM14Off;

						pvm14.SetGlowSprite(GlowSprite);
						pvm14.SetOnSprite(PVM14On);
						pvm14.SetPalSprite(PVM14Pal);
						pvm14.SetGeometrySprite(PVM14Geometry);
						pvm14.SetColourBarsSprite(PVM14ColourBars);
						pvm14.SetOffSprite(PVM14Off);
						pvm14.SetBrokenSprite(PVM14Broken);
						pvm14.SetShockSprite(PVM14Shock);
						pvm14.SetFireSprite(PVM14Fire);
						pvm14.SetWaterSprite(PVM14Water);
						pvm14.SetExplodeSprite(PVM14Explode);

						collider.size = sprite.sprite.bounds.size;
						collider.offset = sprite.sprite.bounds.center;

						behaviour.Properties = ModAPI.FindPhysicalProperties("Flammable metal");
						behaviour.Properties.HeatTransferSpeedMultiplier = 0;   // Disable glowing red hot when on fire
						behaviour.TrueInitialMass = pvm14.GetWeight;
						behaviour.InitialMass = pvm14.GetWeight;
						behaviour.rigidbody.mass = pvm14.GetWeight;
					}
				}
			);

			// ===============
			// PVM 20
			// ===============
			ModAPI.Register(
				new Modification()
				{
					OriginalItem = ModAPI.FindSpawnable("Metal Cube"),
					NameOverride = "PVM 20M4E",
					NameToOrderByOverride = "Text Display PVM",
					DescriptionOverride = "Professional Video Monitor with 20 inch HR Trinitron display.",
					CategoryOverride = ModAPI.FindCategory("Machinery"),
					ThumbnailOverride = ModAPI.LoadSprite(PVM20View, 5f),

					AfterSpawn = (Instance) =>
					{
						// NOTE: 
						// Using Instance.FixColliders(); or manually deleting the Collider2D and adding a new one
						// in order to fix the size of the collider breaks something in the game object, causing the 
						// Tesla coil to not work when near the modified object. I've seen this in a lot of mods, so
						// it's not a local issue. I've used the below method to keep the original collider, by simply
						// setting its size to that of the modified sprite.

						var pvm20 = Instance.GetOrAddComponent<PVM20Behaviour>();
						var sprite = Instance.GetComponent<SpriteRenderer>();
						var collider = Instance.GetComponent<BoxCollider2D>();
						var behaviour = Instance.GetOrAddComponent<PhysicalBehaviour>();

						sprite.sprite = PVM20Off;

						pvm20.SetGlowSprite(GlowSprite);
						pvm20.SetOnSprite(PVM20On);
						pvm20.SetPalSprite(PVM20Pal);
						pvm20.SetGeometrySprite(PVM20Geometry);
						pvm20.SetColourBarsSprite(PVM20ColourBars);
						pvm20.SetOffSprite(PVM20Off);
						pvm20.SetBrokenSprite(PVM20Broken);
						pvm20.SetShockSprite(PVM20Shock);
						pvm20.SetFireSprite(PVM20Fire);
						pvm20.SetWaterSprite(PVM20Water);
						pvm20.SetExplodeSprite(PVM20Explode);

						collider.size = sprite.sprite.bounds.size;
						collider.offset = sprite.sprite.bounds.center;

						behaviour.Properties = ModAPI.FindPhysicalProperties("Flammable metal");
						behaviour.Properties.HeatTransferSpeedMultiplier = 0;   // Disable glowing red hot when on fire
						behaviour.TrueInitialMass = pvm20.GetWeight;
						behaviour.InitialMass = pvm20.GetWeight;
						behaviour.rigidbody.mass = pvm20.GetWeight;
					}
				}
			);

			// ===============
			// PS2
			// ===============
			ModAPI.Register(
				new Modification()
				{
					OriginalItem = ModAPI.FindSpawnable("Metal Cube"),
					NameOverride = "Playstation 2",
					NameToOrderByOverride = "Text Display Playstation 2",
					DescriptionOverride = "Sony Playstation 2",
					CategoryOverride = ModAPI.FindCategory("Machinery"),
					ThumbnailOverride = ModAPI.LoadSprite(PS2View, 5f),

					AfterSpawn = (Instance) =>
					{
						// NOTE: 
						// Using Instance.FixColliders(); or manually deleting the Collider2D and adding a new one
						// in order to fix the size of the collider breaks something in the game object, causing the 
						// Tesla coil to not work when near the modified object. I've seen this in a lot of mods, so
						// it's not a local issue. I've used the below method to keep the original collider, by simply
						// setting its size to that of the modified sprite.

						var ps2 = Instance.GetOrAddComponent<PS2Behaviour>();
						var sprite = Instance.GetComponent<SpriteRenderer>();
						var collider = Instance.GetComponent<BoxCollider2D>();
						var behaviour = Instance.GetOrAddComponent<PhysicalBehaviour>();

						sprite.sprite = PS2Off;

						ps2.SetGlowSprite(GlowSprite);
						ps2.SetOnSprite(PS2On);
						ps2.SetOffSprite(PS2Off);
						ps2.SetBrokenSprite(PS2Broken);
						ps2.SetShockSprite(PS2Shock);
						ps2.SetFireSprite(PS2Fire);
						ps2.SetWaterSprite(PS2Water);
						ps2.SetExplodeSprite(PS2Explode);

						collider.size = sprite.sprite.bounds.size;
						collider.offset = sprite.sprite.bounds.center;

						behaviour.Properties = ModAPI.FindPhysicalProperties("Plastic");
						behaviour.TrueInitialMass = ps2.GetWeight;
						behaviour.InitialMass = ps2.GetWeight;
						behaviour.rigidbody.mass = ps2.GetWeight;
					}
				}
			);
			/*

						// ===============
						// Measuring stick
						// ===============

						ModAPI.Register(
							new Modification()
							{
								OriginalItem = ModAPI.FindSpawnable("Wooden Pole"),
								NameOverride = "Measuring Stick",
								NameToOrderByOverride = "_Measuring Pole",
								DescriptionOverride = "The centre of each green notch is 10cm \nThe centre of each orange notch is 5cm \nEach red square represents 1cm^2",
								CategoryOverride = ModAPI.FindCategory("Misc."),
								ThumbnailOverride = ModAPI.LoadSprite(RulerView, 5f),

								AfterSpawn = (Instance) =>
								{
									var pvm = Instance.GetOrAddComponent<PVM14Behaviour>();
									var sprite = Instance.GetComponent<SpriteRenderer>();
									var collider = Instance.GetComponent<BoxCollider2D>();
									var behaviour = Instance.GetOrAddComponent<PhysicalBehaviour>();

									sprite.sprite = ModAPI.LoadSprite(pvm.Ruler, pvm.RulerScale);

									collider.size = sprite.sprite.bounds.size;
									collider.offset = sprite.sprite.bounds.center;

									behaviour.Properties = ModAPI.FindPhysicalProperties("Wood");
									behaviour.TrueInitialMass = 10f;
									behaviour.InitialMass = 10f;
									behaviour.rigidbody.mass = 10f;
								}
							}
						);
			*/

		}
	}
}