#region copyright
// <copyright file="IHost.cs" company="Calabaraburus">
//   Copyright (c) 2020 Natalchishin Taras
// </copyright>
#endregion
namespace WitsParser.Transport.Server
{
    /// <summary>
    /// Defines WITS host
    /// </summary>
    public interface IHost
    {
        /// <summary>
        /// Start
        /// </summary>
        void Start();

        /// <summary>
        /// Stop
        /// </summary>
        void Stop();

        /// <summary>
        /// Close
        /// </summary>
        void Close();
    }
}