using UnityEngine;

namespace TelevisionPVM
{
	public class TelevisionPVM
	{
		private static string TvSmallView = "Sprites/PVM14View.png";
		private static string TvBigView = "Sprites/PVM20View.png";
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

						sprite.sprite = ModAPI.LoadSprite(pvm.GetTvSmallOff, pvm.GetTvSmallScale, true);

						collider.size = sprite.sprite.bounds.size;
						collider.offset = sprite.sprite.bounds.center;

						behaviour.Properties = ModAPI.FindPhysicalProperties("Flammable metal");
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

						sprite.sprite = ModAPI.LoadSprite(pvm.GetTvBigOff, pvm.GetTvBigScale, true);

						collider.size = sprite.sprite.bounds.size;
						collider.offset = sprite.sprite.bounds.center;

						behaviour.Properties = ModAPI.FindPhysicalProperties("Flammable metal");
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