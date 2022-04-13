using System;
using Events;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseScreen : MonoBehaviour
{
    private IDisposable _subscription;
    
    private void Awake()
    {
        _subscription = EventStreams.UserInterface.Subscribe<EndGameEvent>(Show);
        gameObject.SetActive(false);
    }

    private void Show(EndGameEvent endGameEvent)
    {
        gameObject.SetActive(true);
    }

    [UsedImplicitly]
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void OnDestroy()
    {
        _subscription?.Dispose();
    }
}
