﻿using System.Net.Sockets;

namespace Code.Scenes.BattleScene.Udp.Connection
{
    public interface IByteArrayHandler
    {
        void HandleBytes(byte[] data);
    }
}