using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropDownHandler : MonoBehaviour
{

    Dropdown dropdownMenu;
    private Text m_Text;
    public static string newType;
    private List<Dropdown.OptionData> menuOptions;
    private int menuIndex;

    // Start is called before the first frame update
    void Start()
    {
        //Fetch the Dropdown GameObject
        dropdownMenu = GetComponent<Dropdown>();

        menuIndex = dropdownMenu.GetComponent<Dropdown>().value;

        menuOptions = dropdownMenu.GetComponent<Dropdown>().options;

        //Add listener for when the value of the Dropdown changes, to take action
        dropdownMenu.onValueChanged.AddListener(delegate {
            DropdownValueChanged(dropdownMenu);
        });

        //Initialise the Text to say the first value of the Dropdown
        m_Text.text = menuOptions[0].text;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void DropdownValueChanged(Dropdown change)
    {
        newType = menuOptions[menuIndex].text;
    }
}
