using UnityEngine;

[CreateAssetMenu]
public class ItemSO : ScriptableObject
{
    public string itemName;
    public StatToChange statToChange = new StatToChange();
    public AttributesToChange attributeToChange = new AttributesToChange();
    public int amountToChangeStat;
    public int amountToChangeAttribute;

    public bool UseItem()
    {
        if(statToChange == StatToChange.health)
        {
            //check if player has enough health
            Debug.Log("Health changed by " + amountToChangeStat);
            return true;
        }
        else if(statToChange == StatToChange.mana)
        {
            Debug.Log("Mana changed by " + amountToChangeStat);
            return true;
        }
        else if(statToChange == StatToChange.stamina)
        {
            Debug.Log("Stamina changed by " + amountToChangeStat);
            return true;
        }
        else if(attributeToChange == AttributesToChange.strength)
        {
            Debug.Log("Strength changed by " + amountToChangeAttribute);
            return true;
        }
        else if(attributeToChange == AttributesToChange.intelligence)
        {
            Debug.Log("Intelligence changed by " + amountToChangeAttribute);
            return true;
        }
        return false;
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