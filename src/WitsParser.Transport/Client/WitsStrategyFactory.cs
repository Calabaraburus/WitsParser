#region copyright
// <copyright file="WitsStrategyFactory.cs" company="Calabaraburus">
//   Copyright (c) 2020 Natalchishin Taras
// </copyright>
#endregion
namespace WitsParser.Transport.Client
{
    using NLog;
    using System;
    using WitsParser.Transport.Client.WitsStrategies;
    using WitsParser.Wits;

    /// <summary>
    /// Implements factory for WITS data sending strategies
    /// </summary>
    public class WitsStrategyFactory
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Get strategy
        /// </summary>
        /// <param name="level">Wits level</param>
        /// <param name="client">Client</param>
        /// <returns>Returns strategy</returns>
        public IWitsStrategy GetStartegy(WitsLevel level, IClient client)
        {
            switch (level)
            {
                case WitsLevel.Level0:
                    return new WitsLevel0Strategy(client);

                default:
                    var ex = new NotImplementedException(level.ToString() + " not implemented");
                    _logger.Log(LogLevel.Error, ex);
                    throw ex;
            }
        }
    }
}