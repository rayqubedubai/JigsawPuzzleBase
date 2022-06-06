using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text.RegularExpressions;

public class RegistrationHandler : MonoBehaviour
{

    public GameObject registrationPopup;
    public GameObject keyboard;
    public TMPro.TMP_Text nameText, phoneText, emailText;
    public bool consentAccepted = false;
    public Toggle consent;
    public Button submitButton;
    //public Dropdown orientationDropDown, puzzeTimeDropDown;
    public TMP_Text consentText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void showRegistrationPanel()
    {
        nameText.text = "";
        emailText.text = "";
        phoneText.text = "";
        consent.isOn = false;
        registrationPopup.gameObject.SetActive(true);
        //registrationAnimator.SetBool("Visible", true);
        registrationPopup.GetComponent<TweenPosition>().enabled = true;
        registrationPopup.GetComponent<TweenPosition>().PlayForward();
        StartCoroutine(showKeyboardWithDelay());
    }

    IEnumerator showKeyboardWithDelay()
    {
        yield return new WaitForSeconds(1f);
        keyboard.GetComponent<TweenPosition>().enabled = true;
        keyboard.GetComponent<TweenPosition>().PlayForward();
    }

    public static bool isValidEmail(string inputEmail)
    {
        string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
              @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
              @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
        Regex re = new Regex(strRegex);
        if (re.IsMatch(inputEmail))
            return (true);
        else
            return (false);
    }

    public void onClickSubmit()
    {
        if (!isValidEmail(emailText.text))
        {
            SceneHandler.Instance.warningMsg.text = "Please enter a valid E-Mail";
            SceneHandler.Instance.warningPopup.SetActive(true);
            return;
        }
        else if (nameText.text == "")
        {
            SceneHandler.Instance.warningMsg.text = "Name field can not be empty";
            SceneHandler.Instance.warningPopup.SetActive(true);
            return;
        }
        else if (phoneText.text == "")
        {
            SceneHandler.Instance.warningMsg.text = "Phone field can not be empty";
            SceneHandler.Instance.warningPopup.SetActive(true);
            return;
        }

        SceneHandler.Instance.menuManager.settingsBtn.SetActive(false);
        //SceneHandler.Instance.playerData.init();
        //SceneHandler.Instance.playerData.played_at = "2022-12-20 12:25:20";
        //APIHandler.Instance.sendUserStats(SceneHandler.Instance.playerData);
        SceneHandler.Instance.puzzleBoard.gameObject.SetActive(true);
        SceneHandler.Instance.puzzleBoard.setToCorrectPosition();
        SceneHandler.Instance.playerData.name = nameText.text;
        SceneHandler.Instance.playerData.phone = phoneText.text;
        SceneHandler.Instance.playerData.email = emailText.text;
        StartCoroutine(showPuzzleWithDelay());
    }

    IEnumerator showPuzzleWithDelay()
    {
        keyboard.GetComponent<TweenPosition>().PlayReverse();
        yield return new WaitForSeconds(1);
        registrationPopup.GetComponent<TweenPosition>().PlayReverse();
        yield return new WaitForSeconds(1);
        if (SceneHandler.Instance.isPortrait)
        {
            SceneHandler.Instance.menuManagerPortrait.introHandler.fadeIntroVideo();
        }
        else
        {
            SceneHandler.Instance.menuManager.introHandler.fadeIntroVideo();
        }
        yield return new WaitForSeconds(2);
        SceneHandler.Instance.startGameWithDelay();
        registrationPopup.GetComponent<TweenPosition>().enabled = false;
        //registrationPopup.gameObject.SetActive(false);
    }

    public void onClickInputField(TMPro.TMP_Text selectedInput)
    {
        keyboard.GetComponent<KeyBoardManager>().textBox = selectedInput;
    }

    public void onToggleCheckBox()
    {
        if (consent.isOn)
        {
            consentAccepted = true;
            submitButton.interactable = true;
        }
        else
        {
            consentAccepted = false;
            submitButton.interactable = false;
        }
    }

}
