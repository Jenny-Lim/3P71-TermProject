using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * This handles the Pawn Promotion dropdown menu.
 *
 * @author Patrick Leonard (7008113), Jenny Lim (6978118)
 * @version 1.0 (2022-19-12)
 */
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
    } // Start


    /**
     * When the value of the dropdown is changed, a piece is assigned a new type.
     *
     * @param change The changed dropdown.
     */
    void DropdownValueChanged(Dropdown change)
    {
        newType = menuOptions[menuIndex].text;
    } // DropdownValueChanged

} // DropDownHandler
