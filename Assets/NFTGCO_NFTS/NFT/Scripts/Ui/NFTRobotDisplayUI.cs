using System.Collections;
using System.Collections.Generic;
using NFTGCO;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace NFTCreator
{
    public class NFTRobotDisplayUI : MonoBehaviour
    {
        [SerializeField] private bool _findObjectsOfType = true;
        [SerializeField] private TextMeshProUGUI _xpText;
        [SerializeField] private TextMeshProUGUI _mintTypeText;
        [SerializeField] private TextMeshProUGUI _userName;
        [SerializeField] private Button _prevBtn;
        [SerializeField] private Button _nextBtn;

        private CreateNFTRobot _createNFTRobot;
        private bool setupFirstTime = false;

        public CreateNFTRobot CreateNFTRobot => _createNFTRobot;

        void Start()
        {
            if (_findObjectsOfType)
            {
                _createNFTRobot = FindObjectOfType<CreateNFTRobot>();
            }
            if (NFTGCOStoredManager.Instance.ReceivedArmors)
            {
                NFTGCOGameRequestNFT.OnGetAvailableNFTXpById();
            }

            DelegateButtonCallbacks();
        }

        // Update is called once per frame
        void Update()
        {
            if (!_createNFTRobot) _createNFTRobot = FindObjectOfType<CreateNFTRobot>();

            if (NFTGCOStoredManager.Instance.ReceivedArmors)
            {
                _xpText.text = "<color=green>Xp:</color> " + NFTGCOStoredManager.Instance.CurrentNFTXp.ToString("F0");
                _userName.text = NFTGCOStoredManager.Instance.AccountDTOResponse.username;
                if (NFTGCOStoredManager.Instance.MintTypeAtt[_createNFTRobot.RobotId] == 0)
                {
                    _mintTypeText.text = $"<color=green>Mint type:</color> Genesis NFT";
                }
                else if (NFTGCOStoredManager.Instance.MintTypeAtt[_createNFTRobot.RobotId] == 1)
                {
                    _mintTypeText.text = $"<color=green>Mint type:</color> Recycled NFT";
                }

                if (!setupFirstTime)
                {
                    _createNFTRobot.RobotId = NFTGCOGlobalData.Instance.NFTRobotID;
                    _createNFTRobot.CreateRobotAssets();
                    setupFirstTime = true;
                }
            }
            else
            {
                _xpText.text = "<color=green>Xp:</color> No NFT";
                _mintTypeText.text = $"<color=green>Mint type:</color> No NFT";

                _userName.text = NFTGCOStoredManager.Instance.AccountDTOResponse.username;

            }
        }
        private void DelegateButtonCallbacks()
        {
            _prevBtn.onClick.AddListener(() => PrevBtnPressed());
            _nextBtn.onClick.AddListener(() => NextBtnPressed());
        }
        public void PrevBtnPressed()
        {
            if (NFTGCOStoredManager.Instance.ReceivedArmors)
            {
                _createNFTRobot.RobotId--;
                if (_createNFTRobot.RobotId < 0)
                {
                    _createNFTRobot.RobotId = NFTGCOStoredManager.Instance.StoredResponse.Count - 1;
                }
                NFTGCOGlobalData.Instance.SetNFTRobotID(_createNFTRobot.RobotId);

                _createNFTRobot.CreateRobotAssets();
                setupFirstTime = true;
                
                NFTGCOGameRequestNFT.OnGetAvailableNFTXpById?.Invoke();
            }
        }

        public void NextBtnPressed()
        {
            if (NFTGCOStoredManager.Instance.ReceivedArmors)
            {
                _createNFTRobot.RobotId++;
                if (_createNFTRobot.RobotId >= NFTGCOStoredManager.Instance.StoredResponse.Count)
                {
                    _createNFTRobot.RobotId = 0;
                }
                NFTGCOGlobalData.Instance.SetNFTRobotID(_createNFTRobot.RobotId);


                _createNFTRobot.CreateRobotAssets();
                setupFirstTime = true;
                
                NFTGCOGameRequestNFT.OnGetAvailableNFTXpById?.Invoke();
            }
        }
    }
}