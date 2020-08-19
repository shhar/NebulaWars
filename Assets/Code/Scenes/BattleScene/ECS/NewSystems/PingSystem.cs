﻿using System;
using Code.Scenes.BattleScene.Udp.Experimental;
using Entitas;
using Plugins.submodules.SharedCode.Logger;

namespace Code.Scenes.BattleScene.ECS.NewSystems
{
    /// <summary>
    /// Раз в n миллисекунд отправляет сообщение на игровой сервер для того, чтобы обновить ip адрес.
    /// </summary>
    public class PingSystem:IExecuteSystem
    {
        private DateTime nextPingTime;
        private readonly UdpSendUtils udpSendUtils;
        private readonly ILog log = LogManager.CreateLogger(typeof(PingSystem));
        
        public PingSystem(UdpSendUtils udpSendUtils)
        {
            this.udpSendUtils = udpSendUtils;
        }
        
        public void Execute()
        {
            DateTime now = DateTime.UtcNow;
            if (nextPingTime < now)
            {
                udpSendUtils.SendPingMessage();
                nextPingTime = now + TimeSpan.FromSeconds(0.5f);
            }
        }
    }
}