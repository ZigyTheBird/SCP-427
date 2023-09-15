// -----------------------------------------------------------------------
// <copyright file="SCP914Handlers.cs" company="Zigy">
// GNU License
// </copyright>
// -----------------------------------------------------------------------

namespace SCP_427.Events
{
    using System;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Exiled.API.Features.Pickups;
    using Exiled.CustomItems;
    using Exiled.Events.EventArgs.Scp914;
    using SCP_427.Items;
    using UnityEngine;

    using Random = UnityEngine.Random;

    internal class SCP914Handlers
    {
        public void OnScp914UpgradingItem(UpgradingPickupEventArgs ev)
        {
            if (ev.KnobSetting == Scp914.Scp914KnobSetting.Fine && Random.Range(1, 100) <= 10 && ev.IsAllowed && ev.Pickup.Type == ItemType.SCP500)
            {
                bool spawnSuccessful = SCP427Item.TrySpawn(62, ev.OutputPosition, out _);
                if (spawnSuccessful)
                {
                    ev.Pickup.Destroy();
                }
            }
        }

        public void OnScp914UpgradingInventoryItem(UpgradingInventoryItemEventArgs ev)
        {
            if (ev.KnobSetting == Scp914.Scp914KnobSetting.Fine && Random.Range(1, 100) <= 20 && ev.IsAllowed && ev.Item.Type == ItemType.SCP500)
            {
                ev.Item.Destroy();
                SCP427Item.TryGive(ev.Player, 62);
            }
        }
    }
}
