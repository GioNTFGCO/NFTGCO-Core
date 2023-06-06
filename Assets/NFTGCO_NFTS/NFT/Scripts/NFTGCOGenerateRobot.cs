using System.Collections;
using System.Collections.Generic;
using Forge;
using NFTGCO.Helpers;
using UnityEngine;
namespace NFTCreator
{

    public class NFTGCOGenerateRobot : MonoBehaviour
    {
        [Header("Testing area")]
        [SerializeField] private string _robotToGenerate;
        [SerializeField] private NFTGCO.Helpers.UDictionary<string, CreateNFTRobot> _createNFTRobots;
        [NFTGCO.Helpers.SearchableEnum]
        [SerializeField] private NFTTokenAttributeEnum _typeOfAccessory;

        private string _buttonMessage;

        [Space]
        [SerializeField] private NFTGCO.Helpers.InspectorButton EnableRobotButton = new NFTGCO.Helpers.InspectorButton("EnableRobot");
        [SerializeField] private NFTGCO.Helpers.InspectorButton ShowNextRobotButton = new NFTGCO.Helpers.InspectorButton("ShowNextRobot");
        [SerializeField] private NFTGCO.Helpers.InspectorButton GenerateRandomRobotArmorButton = new NFTGCO.Helpers.InspectorButton("GenerateRandomRobotArmor");
        [SerializeField] private NFTGCO.Helpers.InspectorButton GenerateRandomRobotAccessoryButton = new NFTGCO.Helpers.InspectorButton("GenerateRandomRobotAccessory");
        [SerializeField] private NFTGCO.Helpers.InspectorButton GenerateRandomRobotAuraButton = new NFTGCO.Helpers.InspectorButton("GenerateRandomRobotAura");

        private void Awake()
        {
            EnableRobot();
        }

        private void Start() {
            //ForgeGameRequestsManager.OnGetGameState?.Invoke();
        }

        private void EnableRobot()
        {
            ShowMessage($"Enable robot: {_robotToGenerate}");
            foreach (var item in _createNFTRobots)
            {
                item.Value.gameObject.SetActive(false);
            }
            _createNFTRobots[_robotToGenerate].gameObject.SetActive(true);
        }
        private void ShowNextRobot()
        {
            ShowMessage("Show Next NFT Assets");
            if (ForgeStoredSettings.Instance.StoredResponse == null)
                return;

            _createNFTRobots[_robotToGenerate].RobotId++;
            if (_createNFTRobots[_robotToGenerate].RobotId >= ForgeStoredSettings.Instance.StoredResponse.Count)
                _createNFTRobots[_robotToGenerate].RobotId = 0;

            _createNFTRobots[_robotToGenerate].CreateRobotAssets();
        }
        private void GenerateRandomRobotArmor()
        {
            ShowMessage("Generate Random Robot Assets from inventory.");
            _createNFTRobots[_robotToGenerate].CreateRobotAssets(true);
        }
        private void GenerateRandomRobotAccessory()
        {
            ShowMessage($"Generate Random Accessory: {_typeOfAccessory}");
            if (_typeOfAccessory.GetAttribute<NFTTokenAttributeEnumAttribute>().Name != "Accessory")
                return;

            _createNFTRobots[_robotToGenerate].GenerateRandomAccessoryAsset(_typeOfAccessory);
        }
        private void GenerateRandomRobotAura()
        {
            ShowMessage("Generate Random Aura");
            _createNFTRobots[_robotToGenerate].GenerateRandomAuraAsset();
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