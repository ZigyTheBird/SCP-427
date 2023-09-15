// -----------------------------------------------------------------------
// <copyright file="Config.cs" company="Zigy">
// GNU License
// </copyright>
// -----------------------------------------------------------------------

namespace SCP_427
{
    using System;
    using System.ComponentModel;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Exiled.API.Interfaces;
    using SCP_427.Items;

    public class Config : IConfig
    {
        /// <inheritdoc/>
        public bool IsEnabled { get; set; } = true;

        /// <inheritdoc/>
        public bool Debug { get; set; } = false;

        [Description("Configs for the SCP-427 item")]
        public SCP427Item Scp427ItemConfig { get; set; } = new();
    }
}
