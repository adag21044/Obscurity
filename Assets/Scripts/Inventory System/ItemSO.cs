using UnityEngine;

[CreateAssetMenu]
public class ItemSO : ScriptableObject
{
    public string itemName;
    public StatToChange statToChange = new StatToChange();
    public AttributesToChange attributeToChange = new AttributesToChange();
    public int amountToChangeStat;
    public int amountToChangeAttribute;

    public void UseItem()
    {
        if(statToChange == StatToChange.health)
        {
            Debug.Log("Health changed by " + amountToChangeStat);
        }
        else if(statToChange == StatToChange.mana)
        {
            Debug.Log("Mana changed by " + amountToChangeStat);
        }
        else if(statToChange == StatToChange.stamina)
        {
            Debug.Log("Stamina changed by " + amountToChangeStat);
        }
        else if(attributeToChange == AttributesToChange.strength)
        {
            Debug.Log("Strength changed by " + amountToChangeAttribute);
        }
        else if(attributeToChange == AttributesToChange.intelligence)
        {
            Debug.Log("Intelligence changed by " + amountToChangeAttribute);
        }
    }

    public enum StatToChange
    {
        none,
        health,
        mana,
        stamina
    }

    public enum AttributesToChange
    {
        none,
        strength,
        intelligence
    }
}