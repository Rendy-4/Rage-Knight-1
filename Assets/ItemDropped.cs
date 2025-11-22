using UnityEngine;

public class ItemDropped : MonoBehaviour
{
    public string itemName;
    public int amount;

    private void OnTriggerEnter2D(Collider2D collision) {
       PlayerCurency curency = collision.GetComponent<PlayerCurency>();

       if(curency != null)
        {
            curency.AddCoins(amount);
            Destroy(gameObject);
        }
    }
   
}
