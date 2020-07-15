using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using NetworkLibrary.NetworkLibrary.Udp;
using UnityEngine;

namespace Code.Common
{
    /// <summary>
    /// Хранит информацию о статистике сети в бою.
    /// </summary>
    public class MatchNetworkStatistics
    {
        private readonly string matchId;
        private readonly string matchInfo;
        private readonly object lockObj = new object();
        /// <summary>
        /// Длина сообщения, количество дейтаграм такой длины
        /// </summary>
        private readonly Dictionary<int,int> datagramLengthAndCount = new Dictionary<int, int>();
        /// <summary>
        /// Тип сообщения, длина сообщений этого типа
        /// </summary>
        private readonly Dictionary<MessageType, long> messageTypeTotalSize = new Dictionary<MessageType, long>();
        
        private readonly List<MessageRecord> allMessages = new List<MessageRecord>(10000);
        
        public MatchNetworkStatistics(string matchId, string matchInfo)
        {
            this.matchId = matchId;
            this.matchInfo = matchInfo;
        }
        
        public void RegisterMessage(int messageLength, MessageType messageType)
        {
            if (messageTypeTotalSize.TryGetValue(messageType, out var value))
            {
                messageTypeTotalSize[messageType] = value + messageLength;
            }
            else
            {
                messageTypeTotalSize.Add(messageType, messageLength);
            }
            
            allMessages.Add(new MessageRecord
            {
                Length = messageLength,
                MessageType = messageType
            });
        }

        public void RegisterDatagram(int datagramLength)
        {
            if (datagramLengthAndCount.TryGetValue(datagramLength, out var value))
            {
                datagramLengthAndCount[datagramLength] = value + 1;
            }
            else
            {
                datagramLengthAndCount.Add(datagramLength, 1);
            }
        }

        /// <summary>
        /// Нарушение Single responsibility
        /// </summary>
        public void Print()
        {
            lock (lockObj)
            {
                string dateTime = DateTime.Now.ToLongTimeString().Replace(':', '_');
                string path = $"networkStatistics_{matchId}_{dateTime}.txt";
                
#if UNITY_ANDROID
                path = Application.persistentDataPath +"/"+ path;
#endif
                using (StreamWriter sw = new StreamWriter(path))
                {
                    sw.WriteLine($"{nameof(matchId)} {matchId}");
                    sw.WriteLine($"{matchInfo}");
                    sw.WriteLine($"Общий размер в байтах = {datagramLengthAndCount.Sum(pair => pair.Key*pair.Value)}");
                    sw.WriteLine();
                    sw.WriteLine();
                    sw.WriteLine("Статистика по типам сообщений (тип и размер всех сообщений типа в байтах)");
                    foreach (KeyValuePair<MessageType, long> pair in messageTypeTotalSize)
                    {
                        sw.WriteLine($"{pair.Key.ToString()} \t\t\t {pair.Value}");
                    }
                
                    List<int> sortedKeys = datagramLengthAndCount.Keys.ToList();
                    sortedKeys.Sort();
                    sw.WriteLine("Статистика по размеру дейтаграм с округлением (размер дейтаграм и их количество) сортировка по размеру");
                    for (int maxLength = 100; maxLength < 2000; maxLength+=100)
                    {
                        int count = sortedKeys.Where(key => maxLength - 100< key&& key <= maxLength)
                            .Select(key=>datagramLengthAndCount[key])
                            .Sum(value=>value);
                        sw.WriteLine($"{maxLength-100}-{maxLength} {count}");
                    }
                
                    sw.WriteLine("Все сообщения");
                    lock (lockObj)
                    {
                        foreach (string record in allMessages.Select(item=>item.ToString()))
                        {
                            sw.WriteLine(record);
                        }    
                    }
                }
            }
        }
    }
}