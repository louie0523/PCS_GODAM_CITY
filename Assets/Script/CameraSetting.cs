using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSetting : MonoBehaviour
{
    public Transform Player;

    private void Update()
    {
        this.transform.position = Player.transform.position + new Vector3(-4f, 8f, 0);
        this.transform.LookAt(Player.transform.position + new Vector3(0, 1f, 0));
    }
}
