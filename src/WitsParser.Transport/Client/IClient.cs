#region copyright
// <copyright file="IClient.cs" company="Calabaraburus">
//   Copyright (c) 2020 Natalchishin Taras
// </copyright>
#endregion
namespace WitsParser.Transport.Client
{
    using System;

    /// <summary>
    /// Defines data client. Defines data transfer methods
    /// </summary>
    public interface IClient
    {
        /// <summary>
        /// Event should be raise when connected to server
        /// </summary>
        event EventHandler Connected;

        /// <summary>
        /// Return true, if connected to server, false otherwise
        /// </summary>
        bool IsConnected { get; }

        /// <summary>
        /// Start asynch data send
        /// </summary>
        /// <param name="buffer">Buffer</param>
        /// <param name="callback">Callback</param>
        /// <param name="state">State object</param>
        /// <returns>Return async result</returns>
        IAsyncResult BeginSend(byte[] buffer, AsyncCallback callback, object state);

        /// <summary>
        /// Stop async send
        /// </summary>
        /// <param name="asyncResult">Async result</param>
        /// <returns>Returns </returns>
        int EndSend(IAsyncResult asyncResult);

        /// <summary>
        /// Connect to server
        /// </summary>
        void Connect();

        /// <summary>
        /// Close connection
        /// </summary>
        void Close();
    }
}