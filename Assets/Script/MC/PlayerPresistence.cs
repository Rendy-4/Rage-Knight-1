using UnityEngine;

public class PlayerPresistence : MonoBehaviour
{
    private static PlayerPresistence instance;

    private void Awake() {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }    
}

