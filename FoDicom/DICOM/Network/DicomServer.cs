﻿// Copyright (c) 2012-2015 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.Network
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;

    using Dicom.Log;

    /// <summary>
    /// DICOM server class.
    /// </summary>
    /// <typeparam name="T">DICOM service that the server should manage.</typeparam>
    public class DicomServer<T> : IDisposable
        where T : DicomService, IDicomServiceProvider
    {
        #region FIELDS

        private bool disposed;

        private readonly int port;

        private readonly string certificateName;

        private readonly CancellationTokenSource cancellationSource;

        private readonly List<T> clients;

        #endregion

        #region CONSTRUCTORS

        /// <summary>
        /// Initializes an instance of <see cref="DicomServer{T}"/>, that starts listening for connections in the background.
        /// </summary>
        /// <param name="port">Port to listen to.</param>
        /// <param name="certificateName">Certificate name for authenticated connections.</param>
        /// <param name="options">Service options.</param>
        /// <param name="logger">Logger.</param>
        public DicomServer(int port, string certificateName = null, DicomServiceOptions options = null, Logger logger = null)
        {
            this.port = port;
            this.certificateName = certificateName;
            this.cancellationSource = new CancellationTokenSource();
            this.clients = new List<T>();

            this.Options = options;
            this.Logger = logger ?? LogManager.GetLogger("Dicom.Network");
            this.IsListening = false;
            this.Exception = null;

            Task.Factory.StartNew(
                this.OnTimerTickAsync,
                this.cancellationSource.Token,
                TaskCreationOptions.LongRunning,
                TaskScheduler.Default);

            Task.Factory.StartNew(
                this.ListenAsync,
                this.cancellationSource.Token,
                TaskCreationOptions.LongRunning,
                TaskScheduler.Default);

            this.disposed = false;
        }

        #endregion

        #region PROPERTIES

        /// <summary>
        /// Gets the logger used by <see cref="DicomServer{T}"/>
        /// </summary>
        public Logger Logger { get; private set; }

        /// <summary>
        /// Gets the options to control behavior of <see cref="DicomService"/> base class.
        /// </summary>
        public DicomServiceOptions Options { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the server is actively listening for client connections.
        /// </summary>
        public bool IsListening { get; private set; }

        /// <summary>
        /// Gets the exception that was thrown if the server failed to listen.
        /// </summary>
        public Exception Exception { get; private set; }

        #endregion

        #region METHODS

        /// <summary>
        /// Stop server from further listening.
        /// </summary>
        public void Stop()
        {
            if (!this.cancellationSource.IsCancellationRequested)
            {
                this.cancellationSource.Cancel();
            }
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
        }

        /// <summary>
        /// Execute the disposal.
        /// </summary>
        /// <param name="disposing">True if called from <see cref="Dispose"/>, false otherwise.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (this.disposed)
            {
                return;
            }

            if (disposing)
            {
                this.Stop();
                this.cancellationSource.Dispose();
                this.clients.Clear();
            }

            this.disposed = true;
        }

        /// <summary>
        /// Create an instance of the DICOM service class.
        /// </summary>
        /// <param name="stream">Network stream.</param>
        /// <returns>An instance of the DICOM service class.</returns>
        protected virtual T CreateScp(Stream stream)
        {
            return (T)Activator.CreateInstance(typeof(T), stream, this.Logger);
        }

        ////不支持.net4
        ///// <summary>
        ///// Listen indefinitely for network connections on the specified port.
        ///// </summary>
        //private async void ListenAsync()
        //{
        //    try
        //    {
        //        var noDelay = this.Options != null ? this.Options.TcpNoDelay : DicomServiceOptions.Default.TcpNoDelay;

        //        var listener = NetworkManager.CreateNetworkListener(this.port);
        //        await listener.StartAsync().ConfigureAwait(false);
        //        this.IsListening = true;

        //        while (!this.cancellationSource.IsCancellationRequested)
        //        {
        //            var networkStream =
        //                await
        //                listener.AcceptNetworkStreamAsync(this.certificateName, noDelay, this.cancellationSource.Token)
        //                    .ConfigureAwait(false);

        //            if (networkStream != null)
        //            {
        //                var scp = this.CreateScp(networkStream.AsStream());
        //                if (this.Options != null)
        //                {
        //                    scp.Options = this.Options;
        //                }

        //                this.clients.Add(scp);
        //            }
        //        }

        //        listener.Stop();
        //        this.IsListening = false;
        //        this.Exception = null;
        //    }
        //    catch (OperationCanceledException)
        //    {
        //        this.Logger.Info("Listening manually terminated");

        //        this.IsListening = false;
        //        this.Exception = null;
        //    }
        //    catch (Exception e)
        //    {
        //        this.Logger.Error("Exception listening for clients, {@error}", e);

        //        this.Stop();
        //        this.IsListening = false;
        //        this.Exception = e;
        //    }
        //}

        ////不支持.net4
        ///// <summary>
        ///// Remove no longer used client connections.
        ///// </summary>
        //private async void OnTimerTickAsync()
        //{
        //    while (!this.cancellationSource.IsCancellationRequested)
        //    {
        //        try
        //        {
        //            await Task.Delay(1000, this.cancellationSource.Token).ConfigureAwait(false);
        //            this.clients.RemoveAll(client => !client.IsConnected);
        //        }
        //        catch (OperationCanceledException)
        //        {
        //            this.Logger.Info("Disconnected client cleanup manually terminated.");
        //        }
        //        catch (Exception e)
        //        {
        //            this.Logger.Warn("Exception removing disconnected clients, {@error}", e);
        //        }
        //    }
        //}

        #endregion
    }
}
