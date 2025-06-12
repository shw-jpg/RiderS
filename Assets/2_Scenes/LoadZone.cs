using UnityEngine;

public class LoadZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("RestartZone"))
        {
            GameManager.Instance.GameRestart();
        }
    }
}