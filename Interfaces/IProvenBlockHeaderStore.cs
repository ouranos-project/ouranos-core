﻿using System.Threading.Tasks;
using Martiscoin.Consensus;
using Martiscoin.Consensus.BlockInfo;
using Martiscoin.Consensus.Chain;
using Martiscoin.Utilities;

namespace Martiscoin.Interfaces
{
    /// <summary>
    /// Cache layer for <see cref="ProvenBlockHeader"/>s.
    /// </summary>
    public interface IProvenBlockHeaderStore : IProvenBlockHeaderProvider
    {
        /// <summary>
        /// Initializes the <see cref="IProvenBlockHeaderStore"/> at the last common header between <paramref name="chainedHeader"/> and <see cref="IProvenBlockHeaderProvider.TipHashHeight"/>.
        /// </summary>
        /// <param name="chainedHeader"><see cref="ChainedHeader"/> consensus tip after <see cref="IConsensusManager"/> initialization.</param>
        /// <returns>Tip at which store was initialized.</returns>
        Task<ChainedHeader> InitializeAsync(ChainedHeader chainedHeader);

        /// <summary>
        /// Adds <see cref="ProvenBlockHeader"/> items to the pending batch. Ready for saving to disk.
        /// </summary>
        /// <param name="provenBlockHeader">A <see cref="ProvenBlockHeader"/> item to add.</param>
        /// <param name="newTip">Hash and height pair that represent the tip of <see cref="IProvenBlockHeaderStore"/>.</param>
        void AddToPendingBatch(ProvenBlockHeader provenBlockHeader, HashHeightPair newTip);

        /// <summary>
        /// Saves pending <see cref="ProvenBlockHeader"/> items to the <see cref="IProvenBlockHeaderRepository"/>, then removes the items from the pending batch.
        /// </summary>
        Task SaveAsync();
    }
}
