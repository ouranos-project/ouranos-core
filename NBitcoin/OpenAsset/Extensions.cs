﻿namespace Marscore.NBitcoin.OpenAsset
{
    public static class Extensions
    {
        public static AssetId ToAssetId(this ScriptId id)
        {
            return new AssetId(id);
        }
    }
}