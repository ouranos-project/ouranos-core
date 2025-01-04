﻿using System.Net;
using Marscore.P2P.Protocol;

namespace Marscore.EventBus.CoreEvents.Peer
{
    /// <summary>
    /// A peer message has been received and parsed
    /// </summary>
    /// <seealso cref="EventBase" />
    public class PeerMessageReceived : PeerEventBase
    {
        public Message Message { get; }

        public int MessageSize { get; }

        public PeerMessageReceived(IPEndPoint peerEndPoint, Message message, int messageSize) : base(peerEndPoint)
        {
            this.Message = message;
            this.MessageSize = messageSize;
        }
    }
}