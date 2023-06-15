using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    public int points = 1;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Add points to the player's score
            GameManager.Instance.AddPoints(points);

            // Destroy the pickup object
            Destroy(gameObject);
        }
    }
}
