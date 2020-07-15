using Code.Common;
using NetworkLibrary.NetworkLibrary.Http;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Scenes.LobbyScene.Scripts
{
    public class WarshipPurchaseConfirmationWindowController
    {
        private readonly ILog log =
            LogManager.CreateLogger(typeof(WarshipPurchaseConfirmationWindowController));

        public void Spawn(ProductModel productModel, Transform parent)
        {
            GameObject skinPrefab = Resources
                .Load<GameObject>("Prefabs/LobbyShop/PurchasesConfirmation/WarshipContent");
            GameObject skinContent = Object.Instantiate(skinPrefab, parent, false);
            FillData(skinContent, productModel);
            AddListeners(skinContent, productModel);
        }

        private void FillData(GameObject skinContent, ProductModel productModel)
        {
            //заспавнить корабль
            GameObject warshipPrefab = Resources.Load<GameObject>(productModel.WarshipModel.PrefabPath);
            GameObject warship = Object.Instantiate(warshipPrefab, skinContent.transform, false);
            warship.transform.localPosition = new Vector3(-140, -25, -200);
            warship.transform.localScale = new Vector3(110, 110, 110);

            //установить название корабля
            Text skinName = skinContent.transform.Find("Text_Name").GetComponent<Text>();
            skinName.text = productModel.Name;

            //установить описание корабля
            Text description = skinContent.transform.Find("Text_Description").GetComponent<Text>();
            description.text = productModel.WarshipModel.Description;

            //установить цену
            Text cost = skinContent.transform.Find("Button_Buy/Text_Cost").GetComponent<Text>();
            cost.text = productModel.CostString;

            //TODO сделать установку типа валюты
        }

        private void AddListeners(GameObject skinContent, ProductModel productModel)
        {
            //устновить слушатель на кнопку покупки
        }
    }
}