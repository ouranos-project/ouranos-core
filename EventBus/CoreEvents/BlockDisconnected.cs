﻿using Martiscoin.Consensus.Chain;
using Martiscoin.NBitcoin;
using Newtonsoft.Json;

namespace Martiscoin.EventBus.CoreEvents
{
    /// <summary>
    /// Event that is executed when a block is disconnected from a consensus chain.
    /// </summary>
    /// <seealso cref="EventBase" />
    public class BlockDisconnected : EventBase
    {
        [JsonIgnore]
        public ChainedHeaderBlock DisconnectedBlock { get; }

        public uint256 Hash { get; set; }

        public int Height { get; set; }

        public BlockDisconnected(ChainedHeaderBlock disconnectedBlock)
        {
            this.DisconnectedBlock = disconnectedBlock;

            this.Hash = this.DisconnectedBlock.ChainedHeader.HashBlock;

            this.Height = this.DisconnectedBlock.ChainedHeader.Height;
        }
    }
}