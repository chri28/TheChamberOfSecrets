using UnityEngine;
using DG.Tweening;

public class ButtonHoverEffect : MonoBehaviour
{
    [SerializeField] float tweetDuration;

    public void EnterHover(RectTransform buttonRect)
    {
        buttonRect.DOKill(true);
        float rightPosX = findRightPos(buttonRect);
        buttonRect.DOAnchorPosX(rightPosX, tweetDuration).SetUpdate(true);
    }

    public void ExitHover(RectTransform buttonRect)
    {
        buttonRect.DOKill(true);
        float leftPosX = findLeftPos(buttonRect);
        buttonRect.DOAnchorPosX(leftPosX, tweetDuration).SetUpdate(true);
    }

    private int findRightPos(RectTransform buttonRect)
    {
        if (buttonRect.ToString().Contains("Play") || buttonRect.ToString().Contains("Resume") || buttonRect.ToString().Contains("Item1"))
        {
            return -630;
        }
        else if (buttonRect.ToString().Contains("Account") || buttonRect.ToString().Contains("OptionsPause") || buttonRect.ToString().Contains("Item2"))
        {
            return -580;
        }
        else if (buttonRect.ToString().Contains("Options") || buttonRect.ToString().Contains("Home") || buttonRect.ToString().Contains("SignIn") || buttonRect.ToString().Contains("ExitLogin") || buttonRect.ToString().Contains("CloseShop") || buttonRect.ToString().Contains("Bottone"))
        {
            return -530;
        }
        else if (buttonRect.ToString().Contains("Quit") || buttonRect.ToString().Contains("Cancel") || buttonRect.ToString().Contains("CloseShop"))
        {
            return -480;
        }
        else if (buttonRect.ToString().Contains("Level1"))
        {
            return 200;
        }
        else if (buttonRect.ToString().Contains("Level2"))
        {
            return 225;
        }
        else if (buttonRect.ToString().Contains("Level3"))
        {
            return 250;
        }
        else if (buttonRect.ToString().Contains("Level4"))
        {
            return 275;
        }
        else if (buttonRect.ToString().Contains("Level5"))
        {
            return 300;
        }
        else if (buttonRect.ToString().Contains("Level6"))
        {
            return 325;
        }
        else if (buttonRect.ToString().Contains("Close"))
        {
            return 350;
        }
        else if (buttonRect.ToString().Contains("StartAgain"))
            return -275;
        else if (buttonRect.ToString().Contains("Back"))
            return -250;
        return 0;
    }

    private int findLeftPos(RectTransform buttonRect)
    {
        if (buttonRect.ToString().Contains("Play") || buttonRect.ToString().Contains("Resume") || buttonRect.ToString().Contains("Item1"))
        {
            return -680;
        }
        else if (buttonRect.ToString().Contains("Account") || buttonRect.ToString().Contains("OptionsPause") || buttonRect.ToString().Contains("Item2"))
        {
            return -630;
        }
        else if (buttonRect.ToString().Contains("Options") || buttonRect.ToString().Contains("Home") || buttonRect.ToString().Contains("SignIn") || buttonRect.ToString().Contains("ExitLogin") || buttonRect.ToString().Contains("CloseShop") || buttonRect.ToString().Contains("Bottone"))
        {
            return -580;
        }
        else if (buttonRect.ToString().Contains("Quit") || buttonRect.ToString().Contains("Cancel") || buttonRect.ToString().Contains("CloseShop"))
        {
            return -530;
        }
        else if (buttonRect.ToString().Contains("Level1"))
        {
            return 170;
        }
        else if (buttonRect.ToString().Contains("Level2"))
        {
            return 195;
        }
        else if (buttonRect.ToString().Contains("Level3"))
        {
            return 220;
        }
        else if (buttonRect.ToString().Contains("Level4"))
        {
            return 245;
        }
        else if (buttonRect.ToString().Contains("Level5"))
        {
            return 270;
        }
        else if (buttonRect.ToString().Contains("Level6"))
        {
            return 295;
        }
        else if (buttonRect.ToString().Contains("Close"))
        {
            return 320;
        }
        else if (buttonRect.ToString().Contains("StartAgain"))
            return -300;
        else if (buttonRect.ToString().Contains("Back"))
            return -275;
        return 0;
    }
}
