using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Code.Common;
using NetworkLibrary.NetworkLibrary.Http;
using ZeroFormatter;

namespace Code.Scenes.LobbyScene.Scripts
{
    public class AccountDataDownloader
    {
        private readonly ILog log = LogManager.CreateLogger(typeof(AccountDataDownloader));
        
        public async Task<LobbyModel> Load(CancellationToken cts)
        {
            log.Debug("Старт скачивания модели аккаунта");
            HttpClient httpClient = new HttpClient();
            int attemptNumber = 0;
            while (true)
            {
                log.Debug("Номер попытки "+attemptNumber++);
                await Task.Delay(1000, cts);
                try
                {
                    if(!PlayerIdStorage.TryGetServiceId(out string playerServiceId))
                    {
                        log.Warn("Не удалось получить id игрока");
                        continue;
                    }
                    
                    if(!PlayerIdStorage.TryGetUsername(out string username))
                    {
                        log.Warn("Не удалось получить username игрока");
                        continue;
                    }

                    HttpResponseMessage response;
                    using (MultipartFormDataContent formData = new MultipartFormDataContent())
                    {
                        formData.Add(new StringContent(playerServiceId), nameof(playerServiceId));
                        formData.Add(new StringContent(username), nameof(username));
                        response = await httpClient.PostAsync(NetworkGlobals.InitializeLobbyUrl, formData, cts);
                    }

                                
                    if (!response.IsSuccessStatusCode)
                    {
                        log.Warn("Статус ответа "+response.StatusCode);
                        continue;
                    }

                    string base64String = await response.Content.ReadAsStringAsync();
                    if (base64String.Length==0)
                    {
                        log.Warn("Ответ пуст ");
                        continue;
                    }

                    byte[] data = Convert.FromBase64String(base64String);
                    log.Debug("Длина ответа в байтах "+data.Length);
                    LobbyModel result = ZeroFormatterSerializer.Deserialize<LobbyModel>(data);
                    log.Debug("Десериализация прошла нормально");
                    return result;
                }
                catch (Exception e)
                {
                    UiSoundsManager.Instance().PlayError();
                    log.Error("Упало при скачивании модели "+e.Message +" "+e.StackTrace);
                }
            }
        }
    }
}