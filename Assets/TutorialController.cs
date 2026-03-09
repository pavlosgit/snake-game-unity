/**
 * File: TutorialController.cs
 *
 *   Handles tutorial buttons, which allow us to show different things on the same page depending on the button pressed
 *
 * Version 1
 * Authors: Lawend Mardini, Filip Eriksson, Pavlos Papadopoulos
 */

using UnityEngine;
using UnityEngine.UI;

public class TutorialController : MonoBehaviour
{
    // These allow us to assign infomration to the methods through the unity inspector
    public GameObject tutorialInfoButton1;
    public GameObject tutorialInfoButton2;

    //What these buttons do, is essentially hide and show information. So when button1 is clicked, the information of button1 is shown, information of button2 is hidden. 
    //When button2 is clicked, the information of button 2 is shown and the information of button 1 is hidden.

    // We hide and show by making them active and inactive

    // When first tutorial button is clices, This method is called
    public void OnButton1Click()
    {
        tutorialInfoButton1.SetActive(true);
        tutorialInfoButton2.SetActive(false);
    }

    // When second tutorial button is clices, This method is called
    public void OnButton2Click()
    {
        tutorialInfoButton1.SetActive(false);
        tutorialInfoButton2.SetActive(true);
    }
}
