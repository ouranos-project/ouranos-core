﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Martiscoin.AsyncWork;
using Martiscoin.Configuration;
using Martiscoin.Configuration.Settings;
using Martiscoin.Networks;
using Martiscoin.P2P.Peer;
using Martiscoin.P2P.Protocol.Payloads;
using Martiscoin.Utilities;
using Martiscoin.Utilities.Extensions;
using Microsoft.Extensions.Logging;

namespace Martiscoin.P2P
{
    /// <summary>
    /// The connector used to connect to peers specified with the -addnode argument
    /// </summary>
    public sealed class PeerConnectorAddNode : PeerConnector
    {
        private readonly ILogger logger;

        public PeerConnectorAddNode(
            IAsyncProvider asyncProvider,
            IDateTimeProvider dateTimeProvider,
            ILoggerFactory loggerFactory,
            Network network,
            INetworkPeerFactory networkPeerFactory,
            INodeLifetime nodeLifetime,
            NodeSettings nodeSettings,
            ConnectionManagerSettings connectionSettings,
            IPeerAddressManager peerAddressManager,
            ISelfEndpointTracker selfEndpointTracker) :
            base(asyncProvider, dateTimeProvider, loggerFactory, network, networkPeerFactory, nodeLifetime, nodeSettings, connectionSettings, peerAddressManager, selfEndpointTracker)
        {
            this.logger = loggerFactory.CreateLogger(this.GetType().FullName);

            this.Requirements.RequiredServices = NetworkPeerServices.Nothing;
        }

        /// <inheritdoc/>
        protected override void OnInitialize()
        {
            List<IPEndPoint> addNodes = this.ConnectionSettings.RetrieveAddNodes();

            this.MaxOutboundConnections = addNodes.Count;

            // Add the endpoints from the -addnode arg to the address manager.
            foreach (IPEndPoint ipEndpoint in addNodes)
                this.PeerAddressManager.AddPeer(ipEndpoint.MapToIpv6(), IPAddress.Loopback);
        }

        /// <summary>This connector is always started.</summary>
        public override bool CanStartConnect
        {
            get { return true; }
        }

        /// <inheritdoc/>
        protected override void OnStartConnect()
        {
            this.CurrentParameters.PeerAddressManagerBehaviour().Mode = PeerAddressManagerBehaviourMode.AdvertiseDiscover;
        }

        /// <inheritdoc/>
        protected override TimeSpan CalculateConnectionInterval()
        {
            return TimeSpans.Second;
        }

        /// <summary>
        /// Only connect to nodes as specified in the -addnode arg.
        /// </summary>
        public override async Task OnConnectAsync()
        {
            List<IPEndPoint> addNodes = this.ConnectionSettings.RetrieveAddNodes();

            await addNodes.ForEachAsync(this.ConnectionSettings.MaxOutboundConnections, this.NodeLifetime.ApplicationStopping,
                async (ipEndpoint, cancellation) =>
                {
                    if (this.NodeLifetime.ApplicationStopping.IsCancellationRequested)
                        return;

                    PeerAddress peerAddress = this.PeerAddressManager.FindPeer(ipEndpoint);
                    if (peerAddress != null)
                    {
                        this.logger.LogDebug("Attempting connection to {0}.", peerAddress.Endpoint);

                        await this.ConnectAsync(peerAddress).ConfigureAwait(false);
                    }
                }).ConfigureAwait(false);
        }
    }
}