using OWML.Common;
using OWML.ModHelper;
using UnityEngine;

namespace TheWildsExtended
{
	public class TheWildsExtended : ModBehaviour
	{
		private void Awake()
		{

		}

		private void Start()
		{
			var newHorizons = ModHelper.Interaction.TryGetModApi<INewHorizons>("xen.NewHorizons");
			newHorizons.LoadConfigs(this);

			newHorizons.GetStarSystemLoadedEvent().AddListener((name) =>
			{
				if (name != "SolarSystem") return;
				var planet = newHorizons.GetPlanet("Icarus");
				planet.AddComponent<InfantShover>();
			});
		}
	}
	public class InfantShover : MonoBehaviour
	{
		public OWRigidbody owrb;

		public float drag = 50f;

		public void Start()
		{
			owrb = this.GetAttachedOWRigidbody();
		}

		public void FixedUpdate()
		{
			owrb.AddAcceleration(-owrb.GetVelocity().normalized * drag * Time.fixedDeltaTime);
		}
	}
}