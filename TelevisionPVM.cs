using UnityEngine;

namespace TelevisionPVM
{
	public class TelevisionPVM
	{
		private static float TvSmallScale = 7.20f;     // PVM14 | ~34.0cm tall irl | ~37.5cm tall ingame
		private static float TvBigScale = 6.27f;       // PVM20 | ~46.0cm tall irl | ~49.0cm tall ingame

		private static string TvSmallView = "Sprites/PVM14View.png";
		private static string TvBigView = "Sprites/PVM20View.png";

		private static Sprite GlowSprite = ModAPI.LoadSprite("Sprites/glowSprite.png");

		private static Sprite TvSmallOn =			ModAPI.LoadSprite("Sprites/PVM14/PVM14_ON.png", TvSmallScale, true);
		private static Sprite TvSmallPal =			ModAPI.LoadSprite("Sprites/PVM14/PVM14_ON-PAL.png", TvSmallScale, true);
		private static Sprite TvSmallGeometry =		ModAPI.LoadSprite("Sprites/PVM14/PVM14_ON-Geometry.png", TvSmallScale, true);
		private static Sprite TvSmallColourBars =	ModAPI.LoadSprite("Sprites/PVM14/PVM14_ON-Colour-Bars.png", TvSmallScale, true);
		private static Sprite TvSmallOff =			ModAPI.LoadSprite("Sprites/PVM14/PVM14_OFF.png", TvSmallScale, true);
		private static Sprite TvSmallBroken =		ModAPI.LoadSprite("Sprites/PVM14/PVM14_Destroyed.png", TvSmallScale, true);
		private static Sprite TvSmallShock =		ModAPI.LoadSprite("Sprites/PVM14/PVM14_Destroyed-Shock.png", TvSmallScale, true);
		private static Sprite TvSmallFire =			ModAPI.LoadSprite("Sprites/PVM14/PVM14_Destroyed-Fire.png", TvSmallScale, true);
		private static Sprite TvSmallWater =		ModAPI.LoadSprite("Sprites/PVM14/PVM14_Destroyed-Water.png", TvSmallScale, true);
		private static Sprite TvSmallExplode =		ModAPI.LoadSprite("Sprites/PVM14/PVM14_Destroyed-Explosion.png", TvSmallScale, true);

		private static Sprite TvBigOn =				ModAPI.LoadSprite("Sprites/PVM20/PVM20_ON.png", TvBigScale, true);
		private static Sprite TvBigPal =			ModAPI.LoadSprite("Sprites/PVM20/PVM20_ON-PAL.png", TvBigScale, true);
		private static Sprite TvBigGeometry =		ModAPI.LoadSprite("Sprites/PVM20/PVM20_ON-Geometry.png", TvBigScale, true);
		private static Sprite TvBigColourBars =		ModAPI.LoadSprite("Sprites/PVM20/PVM20_ON-Colour-Bars.png", TvBigScale, true);
		private static Sprite TvBigOff =			ModAPI.LoadSprite("Sprites/PVM20/PVM20_OFF.png", TvBigScale, true);
		private static Sprite TvBigBroken =			ModAPI.LoadSprite("Sprites/PVM20/PVM20_Destroyed.png", TvBigScale, true);
		private static Sprite TvBigShock =			ModAPI.LoadSprite("Sprites/PVM20/PVM20_Destroyed-Shock.png", TvBigScale, true);
		private static Sprite TvBigFire =			ModAPI.LoadSprite("Sprites/PVM20/PVM20_Destroyed-Fire.png", TvBigScale, true);
		private static Sprite TvBigWater =			ModAPI.LoadSprite("Sprites/PVM20/PVM20_Destroyed-Water.png", TvBigScale, true);
		private static Sprite TvBigExplode =		ModAPI.LoadSprite("Sprites/PVM20/PVM20_Destroyed-Explosion.png", TvBigScale, true);

		//private static string RulerView = "Sprites/RulerView.png";

		public static void Main()
		{
			// ********************************
			// PVM 14
			// ********************************
			ModAPI.Register(
				new Modification()
				{
					OriginalItem = ModAPI.FindSpawnable("Metal Cube"),
					NameOverride = "PVM 14M4E",
					NameToOrderByOverride = "Text Display PVM",
					DescriptionOverride = "Professional Video Monitor with 14 inch HR Trinitron display.",
					CategoryOverride = ModAPI.FindCategory("Machinery"),
					ThumbnailOverride = ModAPI.LoadSprite(TvSmallView, 5f),

					AfterSpawn = (Instance) =>
					{
						// NOTE: 
						// Using Instance.FixColliders(); or manually deleting the Collider2D and adding a new one
						// in order to fix the size of the collider breaks something in the game object, causing the 
						// Tesla coil to not work when near the modified object. I've seen this in a lot of mods, so
						// it's not a local issue. I've used the below method to keep the original collider, by simply
						// setting its size to that of the modified sprite.

						var pvm = Instance.GetOrAddComponent<PVMBehaviour>();
						var sprite = Instance.GetComponent<SpriteRenderer>();
						var collider = Instance.GetComponent<BoxCollider2D>();
						var behaviour = Instance.GetOrAddComponent<PhysicalBehaviour>();

						sprite.sprite = TvSmallOff;
						
						pvm.SetGlowSprite(GlowSprite);

						pvm.SetTvSmallOnSprite(TvSmallOn);
						pvm.SetTvSmallPalSprite(TvSmallPal);
						pvm.SetTvSmallGeometrySprite(TvSmallGeometry);
						pvm.SetTvSmallColourBarsSprite(TvSmallColourBars);
						pvm.SetTvSmallOffSprite(TvSmallOff);
						pvm.SetTvSmallBrokenSprite(TvSmallBroken);
						pvm.SetTvSmallShockSprite(TvSmallShock);
						pvm.SetTvSmallFireSprite(TvSmallFire);
						pvm.SetTvSmallWaterSprite(TvSmallWater);
						pvm.SetTvSmallExplodeSprite(TvSmallExplode);

						collider.size = sprite.sprite.bounds.size;
						collider.offset = sprite.sprite.bounds.center;

						behaviour.Properties = ModAPI.FindPhysicalProperties("Flammable metal");
						behaviour.Properties.HeatTransferSpeedMultiplier = 0;	// Disable glowing red hot when on fire
						behaviour.TrueInitialMass = pvm.GetTvSmallWeight;
						behaviour.InitialMass = pvm.GetTvSmallWeight;
						behaviour.rigidbody.mass = pvm.GetTvSmallWeight;
					}
				}
			);

			// ********************************
			// PVM 20
			// ********************************
			ModAPI.Register(
				new Modification()
				{
					OriginalItem = ModAPI.FindSpawnable("Metal Cube"),
					NameOverride = "PVM 20M4E",
					NameToOrderByOverride = "Text Display PVM",
					DescriptionOverride = "Professional Video Monitor with 20 inch HR Trinitron display.",
					CategoryOverride = ModAPI.FindCategory("Machinery"),
					ThumbnailOverride = ModAPI.LoadSprite(TvBigView, 5f),

					AfterSpawn = (Instance) =>
					{
						// NOTE: 
						// Using Instance.FixColliders(); or manually deleting the Collider2D and adding a new one
						// in order to fix the size of the collider breaks something in the game object, causing the 
						// Tesla coil to not work when near the modified object. I've seen this in a lot of mods, so
						// it's not a local issue. I've used the below method to keep the original collider, by simply
						// setting its size to that of the modified sprite.
						
						var pvm = Instance.GetOrAddComponent<PVMBehaviour>();
						var sprite = Instance.GetComponent<SpriteRenderer>();
						var collider = Instance.GetComponent<BoxCollider2D>();
						var behaviour = Instance.GetOrAddComponent<PhysicalBehaviour>();

						sprite.sprite = TvBigOff;

						pvm.SetTvBigOnSprite(TvBigOn);
						pvm.SetTvBigPalSprite(TvBigPal);
						pvm.SetTvBigGeometrySprite(TvBigGeometry);
						pvm.SetTvBigColourBarsSprite(TvBigColourBars);
						pvm.SetTvBigOffSprite(TvBigOff);
						pvm.SetTvBigBrokenSprite(TvBigBroken);
						pvm.SetTvBigShockSprite(TvBigShock);
						pvm.SetTvBigFireSprite(TvBigFire);
						pvm.SetTvBigWaterSprite(TvBigWater);
						pvm.SetTvBigExplodeSprite(TvBigExplode);

						collider.size = sprite.sprite.bounds.size;
						collider.offset = sprite.sprite.bounds.center;

						behaviour.Properties = ModAPI.FindPhysicalProperties("Flammable metal");
						behaviour.Properties.HeatTransferSpeedMultiplier = 0;	// Disable glowing red hot when on fire
						behaviour.TrueInitialMass = pvm.GetTvBigWeight;
						behaviour.InitialMass = pvm.GetTvBigWeight;
						behaviour.rigidbody.mass = pvm.GetTvBigWeight;
					}
				}
			);

			// ********************************
			// Measuring stick
			// ********************************
			/*
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
						var pvm = Instance.GetOrAddComponent<PVMBehaviour>();
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