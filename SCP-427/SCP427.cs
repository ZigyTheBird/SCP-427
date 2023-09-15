// -----------------------------------------------------------------------
// <copyright file="SCP427.cs" company="Zigy">
// GNU License
// </copyright>
// -----------------------------------------------------------------------

namespace SCP_427
{
    using System;

    using Exiled.API.Enums;
    using Exiled.API.Features;
    using Exiled.CustomItems.API;
    using SCP_427.Events;

    public class SCP427 : Plugin<Config>
    {
        private SCP914Handlers mapHandlers = null!;

        public static SCP427 Instance { get; private set; } = null!;

        /// <inheritdoc />
        public override string Author { get; } = "Zigy";

        /// <inheritdoc />
        public override string Name { get; } = "Scp427";

        /// <inheritdoc />
        public override string Prefix { get; } = "Scp0427";

        /// <inheritdoc/>
        public override PluginPriority Priority { get; } = PluginPriority.Medium;

        public override Version RequiredExiledVersion { get; } = new Version(8, 2, 0);

        public override void OnEnabled()
        {
            Instance = this;
            this.mapHandlers = new SCP914Handlers();

            Exiled.Events.Handlers.Scp914.UpgradingInventoryItem += this.mapHandlers.OnScp914UpgradingInventoryItem;
            Exiled.Events.Handlers.Scp914.UpgradingPickup += this.mapHandlers.OnScp914UpgradingItem;

            this.Config.Scp427ItemConfig.Register();

            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            Exiled.Events.Handlers.Scp914.UpgradingInventoryItem -= this.mapHandlers.OnScp914UpgradingInventoryItem;
            Exiled.Events.Handlers.Scp914.UpgradingPickup -= this.mapHandlers.OnScp914UpgradingItem;

            this.Config.Scp427ItemConfig.Unregister();

            this.mapHandlers = null!;
            Instance = null!;

            base.OnDisabled();
        }

        public override void OnReloaded()
        {
        }
    }
}