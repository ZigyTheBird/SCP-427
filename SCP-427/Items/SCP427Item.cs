// -----------------------------------------------------------------------
// <copyright file="SCP427Item.cs" company="Zigy">
// GNU License
// </copyright>
// -----------------------------------------------------------------------

namespace SCP_427.Items
{
    using System.Collections.Generic;

    using Exiled.API.Features;
    using Exiled.API.Features.Attributes;
    using Exiled.API.Features.Items;
    using Exiled.API.Features.Spawn;
    using Exiled.CustomItems.API.Features;
    using Exiled.Events.EventArgs.Player;
    using Exiled.Events.EventArgs.Scp244;
    using UnityEngine;

    [CustomItem(ItemType.SCP244b)]
    public class SCP427Item : CustomItem
    {
        private readonly Dictionary<Player, float> cooldownTimes = new Dictionary<Player, float>();

        public override uint Id { get; set; } = 62;

        public override string Name { get; set; } = "SCP 427";

        public override string Description { get; set; } = "A small, spherical, ornately carved locket that can heal you.";

        public override float Weight { get; set; } = 0.75F;

        #nullable enable
        public override SpawnProperties? SpawnProperties { get; set; } = new SpawnProperties();

        public override Vector3 Scale { get; set; } = new Vector3(0.6F, 0.6F, 0.6F);

        public override ItemType Type { get; set; } = ItemType.SCP244b;

        protected override void SubscribeEvents()
        {
            Exiled.Events.Handlers.Player.UsingItemCompleted += this.OnUsedItem;
            Exiled.Events.Handlers.Player.UsingItem += this.OnUsingItem;
            Exiled.Events.Handlers.Player.PickingUpItem += this.OnPickingUpItem;
            Exiled.Events.Handlers.Player.DroppedItem += this.OnDroppedItem;
            Exiled.Events.Handlers.Player.ChangedItem += this.OnChangedItem;
            Exiled.Events.Handlers.Scp244.OpeningScp244 += this.OnOpeningScp244;

            base.SubscribeEvents();
        }

        protected override void UnsubscribeEvents()
        {
            Exiled.Events.Handlers.Player.UsingItemCompleted -= this.OnUsedItem;
            Exiled.Events.Handlers.Player.UsingItem -= this.OnUsingItem;
            Exiled.Events.Handlers.Player.PickingUpItem -= this.OnPickingUpItem;
            Exiled.Events.Handlers.Player.DroppedItem -= this.OnDroppedItem;
            Exiled.Events.Handlers.Player.ChangedItem -= this.OnChangedItem;
            Exiled.Events.Handlers.Scp244.OpeningScp244 -= this.OnOpeningScp244;

            base.UnsubscribeEvents();
        }

        private void OnUsedItem(UsingItemCompletedEventArgs ev)
        {
            if (this.Check(ev.Item))
            {
                ev.Item.Destroy();
                SCP427Item.TryGive(ev.Player, 62, false);
                if (!this.cooldownTimes.ContainsKey(ev.Player))
                {
                    this.cooldownTimes.Add(ev.Player, Time.time - 120);
                }

                float cooldownTime = 0F;
                this.cooldownTimes.TryGetValue(ev.Player, out cooldownTime);
                if (Time.time > cooldownTime + 120)
                {
                    ev.Player.Heal(100, false);
                    ev.Player.EnableEffect(Exiled.API.Enums.EffectType.Vitality, 10);
                    this.cooldownTimes[ev.Player] = Time.time;
                }
                else
                {
                    Broadcast broadcast = new Broadcast();
                    broadcast.Duration = 2;
                    broadcast.Content = "You must wait " + Mathf.RoundToInt(120 - (Time.time - cooldownTime)) + " seconds before using SCP 427 again.";
                    broadcast.Show = true;
                    broadcast.Type = global::Broadcast.BroadcastFlags.Normal;
                    ev.Player.Broadcast(broadcast, true);
                }
            }
        }

        private void OnUsingItem(UsingItemEventArgs ev)
        {
            if (this.Check(ev.Item))
            {
                if (!this.cooldownTimes.ContainsKey(ev.Player))
                {
                    this.cooldownTimes.Add(ev.Player, Time.time - 120);
                }

                float cooldownTime = 0F;
                this.cooldownTimes.TryGetValue(ev.Player, out cooldownTime);
                if (!(Time.time > cooldownTime + 120))
                {
                    Broadcast broadcast = new Broadcast();
                    broadcast.Duration = 2;
                    broadcast.Content = "You must wait " + Mathf.RoundToInt(120 - (Time.time - cooldownTime)) + " seconds before using SCP 427 again.";
                    broadcast.Show = true;
                    broadcast.Type = global::Broadcast.BroadcastFlags.Normal;
                    ev.Player.Broadcast(broadcast, true);
                }
            }
        }

        private void OnOpeningScp244(OpeningScp244EventArgs ev)
        {
            if (ev.Pickup != null)
            {
                if (this.Check(ev.Pickup))
                {
                    SCP427Item.TrySpawn(62, ev.Pickup.Position, out _);
                    ev.Pickup.UnSpawn();
                    ev.Pickup.Destroy();
                }
            }
        }

        private void OnPickingUpItem(PickingUpItemEventArgs ev)
        {
            if (ev.Pickup != null)
            {
                if (this.Check(ev.Pickup))
                {
                    if (!this.cooldownTimes.ContainsKey(ev.Player))
                    {
                        this.cooldownTimes.Add(ev.Player, Time.time - 120);
                    }

                    float cooldownTime = 0F;
                    this.cooldownTimes.TryGetValue(ev.Player, out cooldownTime);
                    if (!(Time.time > cooldownTime + 120))
                    {
                        Broadcast broadcast = new Broadcast();
                        broadcast.Duration = 2;
                        broadcast.Content = "You must wait " + Mathf.RoundToInt(120 - (Time.time - cooldownTime)) + " seconds before using SCP 427 again.";
                        broadcast.Show = true;
                        broadcast.Type = global::Broadcast.BroadcastFlags.Normal;
                        ev.Player.Broadcast(broadcast, true);
                    }
                }
            }
        }

        private void OnDroppedItem(DroppedItemEventArgs ev)
        {
            if (ev.Pickup != null)
            {
                if (this.Check(ev.Pickup))
                {
                    SCP427Item.TrySpawn(62, ev.Pickup.Position, out _);
                    ev.Pickup.Destroy();
                }
            }
        }

        private void OnChangedItem(ChangedItemEventArgs ev)
        {
            if (this.Check(ev.Item))
            {
                if (!this.cooldownTimes.ContainsKey(ev.Player))
                {
                    this.cooldownTimes.Add(ev.Player, Time.time - 120);
                }

                float cooldownTime = 0F;
                this.cooldownTimes.TryGetValue(ev.Player, out cooldownTime);
                if (!(Time.time > cooldownTime + 120))
                {
                    Broadcast broadcast = new Broadcast();
                    broadcast.Duration = 2;
                    broadcast.Content = "You must wait " + Mathf.RoundToInt(120 - (Time.time - cooldownTime)) + " seconds before using SCP 427 again.";
                    broadcast.Show = true;
                    broadcast.Type = global::Broadcast.BroadcastFlags.Normal;
                    ev.Player.Broadcast(broadcast, true);
                }
            }
        }
    }
}
