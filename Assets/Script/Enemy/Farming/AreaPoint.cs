using UnityEngine;

public class AreaPoint : MonoBehaviour
{
    [SerializeField] private GameObject interactText;
    private bool isPlayerNear= false;

    private void Start() {
    if (interactText != null)
        interactText.SetActive(false);
    else
        Debug.LogError("InteractText belum di-assign di Inspector!", this);
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        isPlayerNear = true;
        interactText.SetActive(true);
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        isPlayerNear = false;
        interactText.SetActive(false);
    }
    public bool IsPlayerNear => isPlayerNear;

    private void Update() {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.F))
        {
            SceneChanges.instance.ChangeScene("Farming");
        }
    }

}
