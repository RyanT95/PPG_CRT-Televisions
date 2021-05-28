using UnityEngine;
using System;

namespace TelevisionPVM
{
	public class DeviceBehaviour : MonoBehaviour
	{
		// Properties
		// =============================================================================================
		protected float Scale;
		protected float Weight;
		protected float Health;
		protected float DamageThreshold;

		// Sprites
		// =============================================================================================
		protected SpriteRenderer DeviceSprite;

		protected Sprite GlowSprite;
		protected Sprite OnSprite;
		protected Sprite OffSprite;
		protected Sprite BrokenSprite;
		protected Sprite ShockSprite;
		protected Sprite FireSprite;
		protected Sprite WaterSprite;
		protected Sprite ExplodeSprite;

		// Getters
		// =============================================================================================
		public float GetScale	{ get { return Scale; } }
		public float GetWeight	{ get { return Weight; } }

		// Setters
		// =============================================================================================
		public void SetGlowSprite(Sprite S)		{ this.GlowSprite = S; }

		public void SetOnSprite(Sprite S)		{ this.OnSprite = S; }
		public void SetOffSprite(Sprite S)		{ this.OffSprite = S; }
		public void SetBrokenSprite(Sprite S)	{ this.BrokenSprite = S; }
		public void SetShockSprite(Sprite S)	{ this.ShockSprite = S; }
		public void SetFireSprite(Sprite S)		{ this.FireSprite = S; }
		public void SetWaterSprite(Sprite S)	{ this.WaterSprite = S; }
		public void SetExplodeSprite(Sprite S)	{ this.ExplodeSprite = S; }

		// Everything Else
		// =============================================================================================
		protected bool Broken;
		protected bool Activated;
		protected PhysicalBehaviour Phys;

		protected readonly string[] ValidParticleEffects = { "BlasterImpact", "FuseBlown", "MetalHit", "Disintegration", "BlasterImpactHole", "Spark", "WoodHit", "PinkExplosion", "EnormousExplosion", "BloodExplosion", "BigZap", "BigExplosion", "HugeZap", "Ricochet", "BrokenElectronicsSpark", "Explosion", "RedBarrelExplosion", "Flash", "IonExplosion", "Vapor" };

		// ********************************
		// Constructor
		// ********************************
		public DeviceBehaviour()
		{
			Broken = false;
			Activated = false;

			Scale = 10.0f;
			Weight = 5.0f;
			Health = 10.0f;
			DamageThreshold = 1.0f;
		}

		// ********************************
		// Start
		// ********************************
		protected virtual void Start()
		{
			DeviceSprite = gameObject.GetComponent<SpriteRenderer>();
		}

		// ********************************
		// Awake
		// ********************************
		protected virtual void Awake()
		{
			this.Phys = this.GetComponent<PhysicalBehaviour>();
		}

		// ********************************
		// Update
		// ********************************
		protected virtual void Update()
		{
			UpdateDamage();
		}

		// ********************************
		// Evaluate various damage types
		// ********************************
		protected virtual void UpdateDamage()
		{
			float deltaTime = Time.deltaTime;

			if (this.Health <= 0.0f && !this.Broken)
			{
				Break("BrokenElectronicsSpark");
				ChangeSprite(BrokenSprite);
			}

			// Device electrocuted
			if (this.Phys.charge > 90.0f && !this.Broken)
			{
				this.Health -= deltaTime * (Health / 2.0f);

				//ModAPI.Notify("HP:" + this.Health);
				//ModAPI.Notify("CRG: " + this.Phys.charge);

				if (this.Health < 0.0f && this.Activated)
				{
					Break("FuseBlown");
					ChangeSprite(ShockSprite);
				}
			}

			// Device wet
			if (this.Phys.Wetness > 0.0f && this.Activated && !this.Broken)
			{
				this.Health -= deltaTime * Health;

				//ModAPI.Notify("HP:" + this.Health);
				//ModAPI.Notify("WTN: " + this.Phys.Wetness);

				if (this.Health < 0.0f && this.Activated)
				{
					Break("Spark");
					ChangeSprite(WaterSprite);
				}
			}

			// Device burning
			if (this.Phys.burnIntensity > 0.5f && !this.Broken)
			{
				this.Health -= deltaTime * (Health / 3.5f);

				//ModAPI.Notify("HP:" + this.Health);
				//ModAPI.Notify("BRN: " + this.Phys.burnIntensity);

				if (this.Health < 0.0f)
				{
					Break("FuseBlown");
					ChangeSprite(FireSprite);
				}
			}
		}
		// ********************************
		// Evaluate activation of Device
		// ********************************
		protected virtual void UpdateActivation()
		{
			if (this.Activated && !this.Broken)
				this.DeviceOn();

			else
				this.DeviceOff();

			if (!this.Broken)
				return;

			ChangeSprite(ExplodeSprite);    // Other damage, i.e. guns and bombs
		}

		// ********************************
		// Device breaks
		// ********************************
		protected virtual void Break(string damageType = "BrokenElectronicsSpark")
		{
			if (this.Health > 0.0f && this.Broken)
				this.Health = 0.0f;

			if (this.Broken)
				return;

			this.Activated = false;
			this.Broken = true;

			if (Array.Exists(ValidParticleEffects, element => element == damageType))
				ModAPI.CreateParticleEffect(damageType, this.transform.position);
			else
				ModAPI.CreateParticleEffect("BrokenElectronicsSpark", this.transform.position);

			this.UpdateActivation();
		}

		// ********************************
		// Device turned on
		// ********************************
		protected virtual void DeviceOn()
		{
			this.Activated = true;
		}

		// ********************************
		// Device turned off
		// ********************************
		protected virtual void DeviceOff()
		{
			this.Activated = false;
		}

		// ********************************
		// Device is used in game
		// ********************************
		protected virtual void Use()
		{
			//ModAPI.Notify("Scale: " + Scale);
			//ModAPI.Notify("Weight: " + Weight);
			//ModAPI.Notify("Health: " + Health);
			//ModAPI.Notify("DT: " + DamageThreshold);

			if (!base.enabled || this.Broken)
				return;

			this.Activated = !this.Activated;

			this.UpdateActivation();
		}

		// ********************************
		// Create right click menu options
		// ********************************
		protected virtual void CreateContextMenuOptions()
		{
			this.Phys.ContextMenuOptions.Buttons.Add(new ContextMenuButton("turnOff", "Turn Off", "Turn Device Off", () =>
			{
				if (!this.Broken && this.Activated)
				{
					this.Activated = false;
					this.UpdateActivation();
				}
			}));

			this.Phys.ContextMenuOptions.Buttons.Add(new ContextMenuButton("turnOn", "Turn On", "Turn Device On", () =>
			{
				if (!this.Broken && !this.Activated)
				{
					this.Activated = true;
					this.UpdateActivation();
				}
			}));
		}

		// ********************************
		// Changes displayed sprite of device
		// ********************************
		protected virtual void ChangeSprite(Sprite sprite)
		{
			if (sprite != null)
				this.DeviceSprite.sprite = sprite;
			else
				this.DeviceSprite.sprite = OffSprite;
		}

		// ********************************
		// Device collides with object
		// ********************************
		protected virtual void OnCollisionEnter2D(Collision2D collision)
		{
			//ModAPI.Notify("Impulse: " + collision.contacts[0].normalImpulse);

			if ((double)collision.contacts[0].normalImpulse <= (double)this.DamageThreshold)
				return;

			if (this.Health > 0.0f)
				this.Health -= (collision.contacts[0].normalImpulse);
		}

		// ********************************
		// Device hit by EMP
		// ********************************
		protected virtual void OnEMPHit()
		{
			ModAPI.CreateParticleEffect("BigZap", this.transform.position);
			this.Break("FuseBlown");
		}

		// ********************************
		// Device is shot
		// ********************************
		protected virtual void Shot(global::Shot shot)
		{
			this.Break("BrokenElectronicsSpark");
		}

		// ********************************
		// Device hit by shrapnel
		// ********************************
		protected virtual void OnFragmentHit(float force)
		{
			// Note, depending on distance from explosion multiple fragments may hit.

			// For smaller explosives
			if (force < 2)
				this.Health -= (force * 250) / 21;

			// For larger explosives
			else
				this.Health -= (force * 100) / 21;

			if (this.Health <= 0)
			{
				this.Break("FuseBlown");
			}
		}
	}
}