using TMPro;
using UnityEngine;

public enum HouseType
{
    Home,
    Work,
    Tavern
}
public class House : MonoBehaviour
{
    [SerializeField] private HouseType type;
    [SerializeField] private GameObject roof;
    [SerializeField] private TextMeshPro text;

    void Start()
    {
        ChangeHouseColor(type);
    }

    public void SetHouseType(HouseType houseType)
    {
        type = houseType;
        ChangeHouseColor(type);
    }

    public HouseType GetHouseType()
    {
        return type;
    }

    private void ChangeHouseColor(HouseType houseType)
    {
        Color color = new Color();
        string houseText;
        switch (houseType)
        {
            case HouseType.Home:
                color = Color.red;
                houseText = "Home";
                break;
            case HouseType.Work:
                color = Color.green;
                houseText = "Work";
                break;
            case HouseType.Tavern:
                color = Color.blue;
                houseText = "Tavern";
                break;
            default:
                houseText = " ";
                break;
        }
        roof.GetComponent<MeshRenderer>().material.color = color;
        text.text = houseText;
    }
}