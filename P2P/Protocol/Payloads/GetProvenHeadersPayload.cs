﻿using Martiscoin.NBitcoin;

namespace Martiscoin.P2P.Protocol.Payloads
{
    /// <inheritdoc />
    /// <summary>
    /// Get proven headers payload which requests proven headers using a similar mechanism as
    /// the getheaders protocol message.
    /// </summary>
    /// <seealso cref="T:Martiscoin.P2P.Protocol.Payloads.Payload" />
    [Payload("getprovhdr")]
    public class GetProvenHeadersPayload : GetHeadersPayload
    {
        public GetProvenHeadersPayload()
        {
            this.HashStop = uint256.Zero;
        }

        public GetProvenHeadersPayload(BlockLocator locator)
        {
            this.BlockLocator = locator;
            this.HashStop = uint256.Zero;
        }

        /// <inheritdoc />
        public override void ReadWriteCore(BitcoinStream stream)
        {
            stream.ReadWrite(ref this.blockLocator);
            stream.ReadWrite(ref this.hashStop);
        }
    }
}
