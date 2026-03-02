using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeDoor : MonoBehaviour
{
    [SerializeField] GameObject Door;
    PlayerController _player;

    void OnTriggerEnter2D(Collider2D other)
    {
        var player = other.GetComponentInParent<PlayerController>();
        if (player == null) return;

        _player = player;

        if (Door != null)
            Door.SetActive(true);

        Invoke(nameof(PlayerDeath), 0.3f);
    }

    void PlayerDeath()
    {
        if (_player != null)
            _player.PlayerDeath();
    }
}
