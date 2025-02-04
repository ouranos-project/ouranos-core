﻿using System;
using System.Text;
using Martiscoin.NBitcoin.Crypto;

namespace Martiscoin.NBitcoin
{
    public class UnsecureRandom : IRandom
    {
        private Random _Rand = new Random();
        #region IRandom Members

        public void GetBytes(byte[] output)
        {
            lock (this._Rand)
            {
                this._Rand.NextBytes(output);
            }
        }

        #endregion

    }


    public interface IRandom
    {
        void GetBytes(byte[] output);
    }

    public partial class RandomUtils
    {
        public static IRandom Random
        {
            get;
            set;
        }

        public static byte[] GetBytes(int length)
        {
            var data = new byte[length];
            if (Random == null)
                throw new InvalidOperationException("You must set the RNG (RandomUtils.Random) before generating random numbers");
            Random.GetBytes(data);
            PushEntropy(data);
            return data;
        }

        private static void PushEntropy(byte[] data)
        {
            if (additionalEntropy == null || data.Length == 0)
                return;
            int pos = entropyIndex;
            byte[] entropy = additionalEntropy;
            for (int i = 0; i < data.Length; i++)
            {
                data[i] ^= entropy[pos % 32];
                pos++;
            }
            entropy = Hashes.SHA256(data);
            for (int i = 0; i < data.Length; i++)
            {
                data[i] ^= entropy[pos % 32];
                pos++;
            }
            entropyIndex = pos % 32;
        }

        private static volatile byte[] additionalEntropy = null;
        private static volatile int entropyIndex = 0;

        public static void AddEntropy(string data)
        {
            if (data == null)
                throw new ArgumentNullException("data");
            AddEntropy(Encoding.UTF8.GetBytes(data));
        }

        public static void AddEntropy(byte[] data)
        {
            if (data == null)
                throw new ArgumentNullException("data");
            byte[] entropy = Hashes.SHA256(data);
            if (additionalEntropy == null)
                additionalEntropy = entropy;
            else
            {
                for (int i = 0; i < 32; i++)
                {
                    additionalEntropy[i] ^= entropy[i];
                }
                additionalEntropy = Hashes.SHA256(additionalEntropy);
            }
        }

        public static uint GetUInt32()
        {
            return BitConverter.ToUInt32(GetBytes(sizeof(uint)), 0);
        }

        public static int GetInt32()
        {
            return BitConverter.ToInt32(GetBytes(sizeof(int)), 0);
        }
        public static ulong GetUInt64()
        {
            return BitConverter.ToUInt64(GetBytes(sizeof(ulong)), 0);
        }

        public static long GetInt64()
        {
            return BitConverter.ToInt64(GetBytes(sizeof(long)), 0);
        }

        public static void GetBytes(byte[] output)
        {
            if (Random == null)
                throw new InvalidOperationException("You must set the RNG (RandomUtils.Random) before generating random numbers");
            Random.GetBytes(output);
            PushEntropy(output);
        }
    }
}