using UnityEngine;

[CreateAssetMenu]
public class Loot : ScriptableObject
{
   public Sprite lootSprite;
    public string lootName;
    public int dropChange;

    public LootType lootType;


    public Loot(string lootname, int dropChange)
    {
        this.lootName = lootname;
        this.dropChange = dropChange;
    }
}
