using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class PlayerBase : MonoBehaviourPun
{
    public MazeGenerator maze;
    int c;
    bool flag = false;
    public CharacterController cc;
    private void Start()
    {
        if (photonView && !photonView.IsMine)
        {
            cc.enabled = false;
            GetComponent<Camera>().enabled = false;
            GetComponent<AudioListener>().enabled = false;
            MutilplayerManager.Instance.players.Add(this);
        }
    }
    private void Update()
    {
        if (!photonView || photonView.IsMine)
        {
            transform.Rotate(0, Input.GetAxis("MoveX"), 0);
            cc.Move((Input.GetAxis("MoveY") * transform.forward).normalized * Time.deltaTime * 10);
            if (cc.velocity.magnitude > 0)
            {
                cc.enabled = false;
                flag = false;
                c = Mathf.RoundToInt(transform.position.x)-    maze.xCurrent ;
                if (c != 0)
                {
                    maze.xCurrent += (int)Mathf.Sign(c);
                    flag = true;
                }
                c = Mathf.RoundToInt(transform.position.z)-    maze.yCurrent ;
                if (c != 0)
                {
                    maze.yCurrent += (int)Mathf.Sign(c);
                    flag = true;
                }
                if (flag)
                {
                    maze.Generate();
                }
                cc.enabled = true;
            }
        }
    }
}
