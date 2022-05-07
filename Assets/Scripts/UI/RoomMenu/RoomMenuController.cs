using System.Collections;
using System.Collections.Generic;
using Events.ScriptableObjects;
using Photon.Pun;
using Photon.Realtime;
using SceneManagement.ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;

namespace UI.RoomMenu
{
    public class RoomMenuController : MonoBehaviour
    {
        [SerializeField]
        private LoadSceneEventChannelSO loadSceneEvent;
        [SerializeField]
        private GameSceneSO RaceTrackScene;
        [SerializeField]
        private InputField nameInput;
        [SerializeField]
        private InputField roomNameInput;
        [SerializeField]
        private Button joinRoomButton;
        [SerializeField]
        private Button goToGameButton;

        [SerializeField]
        private Button goBackButton;

        private void JoinRoomEvent()
        {
            if (PhotonNetwork.IsConnected)
            {
                var roomName = roomNameInput.text;
                PhotonNetwork.LocalPlayer.NickName = nameInput.text;
                Debug.Log("PhotonNetwork.IsConnected! | Trying to Create/Join Room " + roomName);
                RoomOptions roomOptions = new RoomOptions();
                TypedLobby typedLobby = new TypedLobby(roomName, LobbyType.Default);
                PhotonNetwork.JoinOrCreateRoom(roomName, roomOptions, typedLobby);
            }
        }

        private void GoToGameEvent()
        {
            PhotonNetwork.LoadLevel("MultiplayerDemo");
        }

        private void OnEnable()
        {
            joinRoomButton.onClick.AddListener(() => JoinRoomEvent());
            goToGameButton.onClick.AddListener(() => GoToGameEvent());
            goBackButton.onClick.AddListener(() => loadSceneEvent.RaiseEvent(RaceTrackScene, true));
        }

        private void OnDisable()
        {
            joinRoomButton.onClick.RemoveAllListeners();
            goToGameButton.onClick.RemoveAllListeners();
            goBackButton.onClick.RemoveAllListeners();
        }
    }
}