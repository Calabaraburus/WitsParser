#region copyright
// <copyright file="ISimpleWorker.cs" company="Calabaraburus">
//   Copyright (c) 2020 Natalchishin Taras
// </copyright>
#endregion
namespace WitsParser.Transport.Server
{
    using System.Net.Sockets;

    /// <summary>
    /// Defines service worker
    /// </summary>
    public interface ISimpleWorker
    {
        /// <summary>
        /// Start worker
        /// </summary>
        /// <param name="client">Tcp client</param>
        void Start(TcpClient client);

        /// <summary>
        /// Stop worker
        /// </summary>
        void Stop();
    }
}