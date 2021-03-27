using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    private void Awake()
    {
        //AutomaticallySyncScene는 방의 모든 클라이언트가 마스터 클라이언트와 동일한 레벨을 로드해야 하는지 정의합니다.
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    // Start is called before the first frame update
    void Start()
    {

        PhotonNetwork.GameVersion = "1.0";

    }

    public void StartNetwork()
    {
        //ConnectUsingSettings은 즉시 온라인 상태로 만들어줌
        PhotonNetwork.ConnectUsingSettings();
    }

    //포톤 서버에 접속
    public override void OnConnectedToMaster()
    {
        Debug.Log("aa");
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 2;

        PhotonNetwork.CreateRoom("room", roomOptions);
    }

    //방에 입장한 후에 플레이어 생성
    public override void OnJoinedRoom()
    {
        //PhotonNetwork.Instantiate("cube", Vector3.zero, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
