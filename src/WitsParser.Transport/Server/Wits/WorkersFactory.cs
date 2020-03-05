#region copyright
// <copyright file="WorkersFactory.cs" company="Calabaraburus">
//   Copyright (c) 2020 Natalchishin Taras
// </copyright>
#endregion
namespace WitsParser.Transport.Server.Wits
{
    using System;
    using System.Collections.Generic;
    using WitsParser.Transport.Server.Wits.Workers;
    using WitsParser.Wits;

    /// <summary>
    /// WITS workers factory
    /// </summary>
    public class WorkersFactory
    {
        /// <summary>
        /// Get WITS worker
        /// </summary>
        /// <param name="level">WITS level</param>
        /// <param name="postProcessors"></param>
        /// <returns>Returns worker</returns>
        public ISimpleWorker GetWitsWorker(WitsLevel level, IEnumerable<IPostProcessing> postProcessors = null)
        {
            switch (level)
            {
                case WitsLevel.Level0:
                    return new WitsLevel0Worker(postProcessors);

                default:
                    throw new Exception("This Wits level not supported: " + level.ToString());
            }
        }
    }
}