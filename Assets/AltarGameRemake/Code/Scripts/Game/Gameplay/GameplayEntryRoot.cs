using UnityEngine;

public class GameplayEntryRoot : MonoBehaviour
{
    [SerializeField] private GameObject _sceneRootBinder;


    public void Run()
    {
        Debug.Log("Gameplay scene loaded");
    }
}
