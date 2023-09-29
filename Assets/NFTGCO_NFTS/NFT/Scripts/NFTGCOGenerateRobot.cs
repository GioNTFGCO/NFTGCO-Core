using System.Collections;
using System.Collections.Generic;
using NFTGCO;
using NFTGCO.Helpers;
using UnityEngine;
using UnityEngine.Serialization;

namespace NFTCreator
{

    public class NFTGCOGenerateRobot : MonoBehaviour
    {
        [Header("Testing area")]
        [SerializeField] private CreateNFTRobot _avatarRobot;
        [NFTGCO.Helpers.SearchableEnum]
        [SerializeField] private NFTTokenAttributeEnum _typeOfAttribute;

        private string _buttonMessage;

        [Space]
        [SerializeField] private NFTGCO.Helpers.InspectorButton ShowNextRobotButton = new NFTGCO.Helpers.InspectorButton(nameof(ShowNextRobot));
        [SerializeField] private NFTGCO.Helpers.InspectorButton GenerateRandomRobotArmorButton = new NFTGCO.Helpers.InspectorButton(nameof(GenerateRandomRobotArmor));
        [SerializeField] private NFTGCO.Helpers.InspectorButton GenerateRandomRobotAccessoryButton = new NFTGCO.Helpers.InspectorButton(nameof(GenerateRandomRobotAccessory));
        [SerializeField] private NFTGCO.Helpers.InspectorButton GenerateRandomRobotAuraButton = new NFTGCO.Helpers.InspectorButton(nameof(GenerateRandomRobotAura));

        
        private void ShowNextRobot()
        {
            ShowMessage("Show Next NFT Assets");
            if (NFTGCOStoredManager.Instance.StoredResponse == null)
                return;

            _avatarRobot.RobotId++;
            if (_avatarRobot.RobotId >= NFTGCOStoredManager.Instance.StoredResponse.Count)
                _avatarRobot.RobotId = 0;

            _avatarRobot.CreateRobotAssets();
        }
        private void GenerateRandomRobotArmor()
        {
            ShowMessage("Generate Random Armor");
            
            if (_typeOfAttribute.GetAttribute<NFTTokenAttributeEnumAttribute>().Name != "Armor")
                return;
            
            _avatarRobot.GenerateRandomSocketAttribute(_typeOfAttribute);
        }
        private void GenerateRandomRobotAccessory()
        {
            ShowMessage($"Generate Random Accessory: {_typeOfAttribute}");
            if (_typeOfAttribute.GetAttribute<NFTTokenAttributeEnumAttribute>().Name != "Accessory")
                return;

            _avatarRobot.GenerateRandomAccessoryAsset(_typeOfAttribute);
        }
        private void GenerateRandomRobotAura()
        {
            ShowMessage("Generate Random Aura");
            _avatarRobot.GenerateRandomAuraAsset();
        }

        private void ShowMessage(string message)
        {
            _buttonMessage = message;
        }

#if UNITY_EDITOR
        private void OnGUI()
        {
            GUI.skin.label.fontSize = 30;
            GUI.Label(new Rect(25, 25, Screen.width, Screen.height), $"{_buttonMessage}");
        }
#endif
    }
}