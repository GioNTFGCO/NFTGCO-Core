using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace NFTGCO
{

    public class XpSystem : MonoBehaviour
    {
        [Serializable]
        public enum XpModel
        {
            Time,
            Variables
        }

        [SerializeField] private XpModel _xpModel;
        [Header("References")]
        [SerializeField] private XpSystemUi _xpSystemUi;
        [Header("Variable Based Model")]
        //[SerializeField] private float _complexityCoefficient = 1.7f;
        //[SerializeField] private float _xpPerTimeRemaining = 2f;
        [SerializeField] private float _xpPerActiveSec = 0.02f;
        [SerializeField] private float _xpPerPerfectGame = 300f;
        [SerializeField] private int _xpPerPhaseCompleted = 20;
        [Header("[Variables]")]
        [SerializeField] private float _activeTimeSec;
        [SerializeField] private float _activeTimeMins;
        [SerializeField] private float _totalXp;
        [SerializeField] private float _timeRemaining;

        [SerializeField] private float _afkTime;
        [SerializeField] private int _afkMinutes;

        [SerializeField] private GameObject _addedXpObject;
        [SerializeField] private Transform _addedXpRoot;
        [SerializeField] private GameObject _addedCoinsObject;
        [SerializeField] private Transform _addedCoinsRoot;

        private float _lastActiveTimeMins;
        private bool _forceCloseAfkAlert;
        private int _lastGameStage;
        private bool _isPvP;
        private bool _xpPointsAdded = false;

        private void Awake()
        {
            if (NFTGCOStoredManager.Instance.ReceivedArmors)
            {
                NFTGCOGameRequestNFT.OnGetAvailableNFTXpById?.Invoke();
            }
        }
        public void GetTotalXp()
        {
            NFTGCOGlobalData.Instance.SetTotalXP(NFTGCOStoredManager.Instance.CurrentNFTXp);
            //  MarsFPSKit.Kit_IngameMain.instance.totalXP = ForgeStoredSettings.Instance.currentNftXp;
        }
        public void Start()
        {
            //activeTimeSec = PlayerPrefs.GetFloat("activeTimeSec", 0f);

            // if (Photon.Pun.PhotonNetwork.InRoom && Photon.Pun.PhotonNetwork.PlayerList.Length > 1) isPvP = true;
            //  else isPvP = false;
        }
        void Update()
        {
            if (_xpModel == XpModel.Variables)
            {
                //  bool duringGame = MarsFPSKit.Kit_IngameMain.instance.gameModeStage == 1 && !MarsFPSKit.Kit_IngameMain.isPauseMenuOpen;

                //   if (duringGame && !xpPointsAdded &&  timeRemaining <= 0f)
                {
                    //reset xp received from previous level
                    _totalXp = 0f;

                    if (_afkMinutes == 0) _activeTimeSec += Time.deltaTime;
                    _activeTimeMins = MathF.Floor(_activeTimeSec / 60f);

                    if (!Input.anyKey)
                    {
                        _afkTime += Time.deltaTime;
                        if (_afkTime >= 60f)
                        {
                            _afkMinutes++;
                            if (_xpSystemUi.AFKAlertPanel)
                            {
                                _xpSystemUi.AfkAlert(_afkMinutes, _forceCloseAfkAlert);
                            }
                            //reset afk time
                            _afkTime = 0f;
                        }
                        _forceCloseAfkAlert = false;
                    }
                    //   else
                    {
                        _afkMinutes = 0;
                        _afkTime = 0f;
                        if (!_forceCloseAfkAlert) _forceCloseAfkAlert = true;
                    }
                }

                /*if(lastGameStage != MarsFPSKit.Kit_IngameMain.instance.gameModeStage)
                {
                    lastGameStage = MarsFPSKit.Kit_IngameMain.instance.gameModeStage;

                    if (lastGameStage == 3)
                    {
                        StartCoroutine(WaitForWinPanel());
                    }
                }*/
            }
        }

        IEnumerator WaitForWinPanel()
        {
            yield return new WaitForEndOfFrame();

            //if (GameManager.instance.winSound.activeSelf)
            {
                //won level, calculate XP
                float xpActiveTime = _activeTimeMins * _xpPerActiveSec;
                float xpLevelCompleted = _xpPerPerfectGame;
                //     float xpTimeRemaining = Timer.instance.timeInSeconds * xpPerPhaseCompleted;

                //       float total = xpActiveTime + xpLevelCompleted + xpTimeRemaining;
                //    AddXpPoints(total);

                //reset active time
                _activeTimeSec -= (_activeTimeMins * 60f);
                _activeTimeMins = 0f;
            }
            // else
            {
                //failed level, calculate XP using only active time
                float total = _activeTimeMins * _xpPerActiveSec;
                AddXpPoints(total);

                //reset active time
                _activeTimeSec -= _activeTimeMins * 60f;
                _activeTimeMins = 0f;
            }
            PlayerPrefs.SetFloat("activeTimeSec", _activeTimeSec);
        }
        public void CalculateXp(float timeLeft, bool wonPhase, bool wonLastGame, int actorNumber)
        {
            if (!_xpPointsAdded)
            {
                _activeTimeSec = MathF.Round(_activeTimeSec * 10) / 10;

                float xpActiveTime = 0f; float xpWonPhase = 0f; float xpPerfectGame = 0f;
                //if (isPvP) xpActiveTime = activeTimeSec * complexityCoefficient * xpPerActiveMinuteVar_pvp;
                //else xpActiveTime = activeTimeSec * complexityCoefficient * xpPerActiveMinuteVar_pve;
                xpActiveTime = _activeTimeSec * _xpPerActiveSec;

                if (wonPhase)
                {
                    xpWonPhase = _xpPerPhaseCompleted;
                }
                /*  else
                  {
                      for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
                      {
                          if (PhotonNetwork.PlayerList[i].ActorNumber == actorNumber)
                          {
                              ExitGames.Client.Photon.Hashtable table = PhotonNetwork.PlayerList[i].CustomProperties;
                              int number = (int)table["phasesNotSurvived"];
                              int newNumber = number + 1;
                              table["phasesNotSurvived"] = newNumber;
                              PhotonNetwork.PlayerList[i].SetCustomProperties(table);
                              Debug.Log("======== died in how many phases " + newNumber);

                              break;
                          }
                      }
                  }*/

                /*    if (wonLastGame)
                    {
                        for(int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
                        {
                            if(PhotonNetwork.PlayerList[i].ActorNumber == actorNumber)
                            {
                                ExitGames.Client.Photon.Hashtable table = PhotonNetwork.PlayerList[i].CustomProperties;
                                int deaths = (int)table["phasesNotSurvived"];
                                if(deaths == 0) xpPerfectGame = xpPerPerfectGame;
                                break;
                            }
                        }
                    }*/

                /*    float xpTimeRemaining = 0f;
                    if (timeRemaining > 0f) { xpTimeRemaining = timeRemaining * xpPerTimeRemaining; EconomyLog.instance.timeRemaining.text = timeRemaining.ToString("F2"); }
                    else {xpTimeRemaining = timeLeft * xpPerTimeRemaining; EconomyLog.instance.timeRemaining.text = timeLeft.ToString("F2"); }

                    float total = xpActiveTime + xpWonPhase + xpPerfectGame + xpTimeRemaining;
                    if (total > 0f) AddXpPoints(total);*/

                //reset active time
                //activeTimeSec -= activeTimeMins * 60f;
                //activeTimeSec = 0f;
                //activeTimeMins = 0f;

                //PlayerPrefs.SetFloat("activeTimeSec", activeTimeSec);

                _xpPointsAdded = true;
                Debug.Log("===== CALCULATE XP, time left " + timeLeft + "/" + _timeRemaining + ", wonphase " + wonPhase + ", wonthelastgame " + wonLastGame + ", perfectgame " + (bool)(xpPerfectGame > 0f));
            }
        }
        public void AddXpPoints(float addedXp)
        {
            addedXp = Mathf.Round(addedXp * 10) / 10;

            if (NFTGCOStoredManager.Instance.ReceivedArmors)
            {
               
                NFTGCOGameRequestNFT.OnIncreaseNftXp?.Invoke(Convert.ToInt64(Mathf.RoundToInt(addedXp)));
            }
            _totalXp += addedXp;
            //GameObject go = Instantiate(addedXpObject, addedXpRoot, false);
            //go.transform.Find("XpAddedText").GetComponent<Text>().text = "+" + addedXp + " XP";
            StartCoroutine(DestroyObject(null, addedXp));
        }
        IEnumerator DestroyObject(GameObject go, float addedXp)
        {
            //   PlayerPrefs.SetFloat("TotalXp",MarsFPSKit.Kit_IngameMain.instance.totalXP + addedXp);

            yield return new WaitForSeconds(0.5f);
            // Destroy(go);
            do
            {
                //    MarsFPSKit.Kit_IngameMain.instance.AddTotalXp(2f);
                addedXp -= 2f;
                yield return new WaitForEndOfFrame();
            }
            while (addedXp >= 2f);

            //   if(addedXp>0f) MarsFPSKit.Kit_IngameMain.instance.AddTotalXp(addedXp);
        }
    }
}