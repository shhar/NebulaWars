using System;
using NetworkLibrary.NetworkLibrary.Http;
using Plugins.submodules.SharedCode.Logger;

namespace Code.Scenes.LobbyScene.ECS.AccountData.AccountDataChangingHandlers
{
    public static class AccountInfoExtensions
    {
        public static void CheckAccountData(this AccountDto accountInfoArg, ILog log)
        {
            log.Info(nameof(CheckAccountData));
            if (accountInfoArg == null)
            {
                log.Error("accountInfoArg is null");
            }
            else
            {
                if (accountInfoArg.Username != null)
                {
                    log.Info(nameof(CheckAccountData)+" Username "+accountInfoArg.Username);
                }
                else
                {
                    log.Error(nameof(CheckAccountData)+" Username is null ");
                }
                
                if (accountInfoArg.Warships != null)
                {
                    if (accountInfoArg.Warships.Count > 0)
                    {
                        foreach (var warshipCopy in accountInfoArg.Warships)
                        {
                            log.Info(warshipCopy.WarshipName);
                            if (warshipCopy.PowerLevel == 0)
                            {
                                log.Error("Нулевой уровень");
                                throw new Exception("Нулевой уровень");
                            }
                        }    
                    }
                    else
                    {
                        log.Error("Warships count = 0");
                    }
                }
                else
                {
                    log.Error("Warships is null");
                }
            }
        }
    }
}