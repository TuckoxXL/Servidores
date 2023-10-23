using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerNetwork : NetworkBehaviour
{
    [SerializeField] float movespeed = 3f;
    [SerializeField] private Transform spawnObjectPrefab;
    private Transform spawnObjectoTransform;

    private void Update()
    {
        if (!IsOwner) return;

        Vector3 moveDir = Vector3.zero;

        if (Input.GetKey(KeyCode.W)) moveDir.z = 1f;
        if (Input.GetKey(KeyCode.S)) moveDir.z = -1f;
        if (Input.GetKey(KeyCode.A)) moveDir.x = -1f;
        if (Input.GetKey(KeyCode.D)) moveDir.x = 1f;

        if (Input.GetKeyDown(KeyCode.T))
        {
            TestServerRpc();
        }

        transform.Translate(moveDir * movespeed * Time.deltaTime);
    }

    [ServerRpc]
    private void TestServerRpc()
    {
        spawnObjectoTransform = Instantiate(spawnObjectPrefab, transform.position, Quaternion.identity);
        spawnObjectoTransform.GetComponent<NetworkObject>().Spawn(true);
    }
}
