#region copyright

// <copyright file="ISimpleWorker.cs" company="Calabaraburus">
//   Copyright (c) 2020 Natalchishin Taras
// </copyright>

#endregion copyright

namespace WitsParser.Transport.Server
{
    /// <summary>
    /// Defines  data postprocessor
    /// </summary>
    public interface IPostProcessing
    {
        /// <summary>
        /// Porcess data
        /// </summary>
        /// <param name="data">Data</param>
        void ProcessData(byte[] data);
    }
}