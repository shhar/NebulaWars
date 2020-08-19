﻿using Plugins.submodules.SharedCode.Logger;
using Plugins.submodules.SharedCode.NetworkLibrary.Udp.PlayerToServer;

namespace Code.Scenes.BattleScene.Udp.Experimental
{
    public class InputModelValidator
    {
        private readonly ILog log = LogManager.CreateLogger(typeof(InputModelValidator));
        public void Validate(InputMessageModel model)
        {
            if (model.TickNumber == 0)
            {
                log.Error("Пустой номер тика");
            }
        }
    }
}