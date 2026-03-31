using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Events;

public class ResetTrigger : MonoBehaviour
{
    public string playerTag = "Player";
    public Transform startPos;
    [SerializeField] private CinemachineCamera vcam;
    public UnityEvent respawn;

    private Vector3 spawnPoint;

    private void Start()
    {
        if (startPos != null)
        {
            spawnPoint = startPos.position;
        }
    }

    public void RespawnPlayer(GameObject player)
    {
        if (player == null)
        {
            return;
        }

        player.transform.root.position = spawnPoint;

        if (vcam != null)
        {
            vcam.OnTargetObjectWarped(player.transform, spawnPoint);
        }

        Rigidbody body = player.GetComponent<Rigidbody>();
        if (body != null)
        {
            body.linearVelocity = Vector3.zero;
            body.angularVelocity = Vector3.zero;
        }

        PlayerController pc = player.GetComponent<PlayerController>();
        if (pc != null)
        {
            pc.health = pc.maxHealth;
        }

        respawn?.Invoke();
    }

    public void SetSpawnPoint(Vector3 newSpawnPoint)
    {
        spawnPoint = newSpawnPoint;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(playerTag))
        {
            return;
        }

        other.transform.root.position = spawnPoint;

        if (vcam != null)
        {
            vcam.OnTargetObjectWarped(other.transform, spawnPoint);
        }

        Rigidbody body = other.GetComponent<Rigidbody>();
        if (body != null)
        {
            body.linearVelocity = Vector3.zero;
            body.angularVelocity = Vector3.zero;
        }

        respawn?.Invoke();
    }
}
