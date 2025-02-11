using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangingLanguage : MonoBehaviour
{
    public Text settingsTitleText;
    public Text volumeSettText;
    public Text langSettText;
    public Text gameOverText;
    public Text combText;
    public Text collectionsText;
    public Text simpleCollDicesText;
    public Button engLangBtn;
    public Button rusLangBtn;
    public Button mainMenuBtn;
    public Button combinationsBtn;
    public Button[] backBtns;
    public Button[] mainBtns;
    public Button[] collectionsBtns;
    
    public Sprite[] engBtnSprite;
    public Sprite[] rusBtnSprite;
    public Sprite[] backBtnSprite;
    public Sprite[] combinationsBtnsSprites;
    public Sprite[] mainMenuBtnSprite;
    public Sprite[] mainBtnsSpritesEng;
    public Sprite[] collectionBtnsSpritesEng;
    public Sprite[] mainBtnsSpritesRus;
    public Sprite[] collectionBtnsSpritesRus;
    

    
    public void SetEngLang()
    {
        settingsTitleText.text = "Settings";
        volumeSettText.text = "Volume";
        langSettText.text = "Language";
        gameOverText.text = "Game Over!";
        combText.text = "Combinations";
        collectionsText.text = "Collections";
        simpleCollDicesText.text = "Common Dices";
        Image engBtnImage = engLangBtn.GetComponent<Image>();
        engBtnImage.sprite = engBtnSprite[0];
        Image rusBtnImage = rusLangBtn.GetComponent<Image>();
        rusBtnImage.sprite = rusBtnSprite[0];
        Image mainMenuBtnImage = mainMenuBtn.GetComponent<Image>();
        mainMenuBtnImage.sprite = mainMenuBtnSprite[0];
        Image combBtnImage = combinationsBtn.GetComponent<Image>();
        combBtnImage.sprite = combinationsBtnsSprites[0];
        for (int i = 0; i < backBtns.Length; i++)
        {
            Image backBtnImage = backBtns[i].GetComponent<Image>();
            backBtnImage.sprite = backBtnSprite[0];
        }

        for (int i = 0; i < mainBtns.Length; i++)
        {
            Image mainBtnImage = mainBtns[i].GetComponent<Image>();
            mainBtnImage.sprite = mainBtnsSpritesEng[i];
        }
        for (int i = 0; i < collectionsBtns.Length; i++)
        {
            Image collectionsBtnImage = collectionsBtns[i].GetComponent<Image>();
            collectionsBtnImage.sprite = collectionBtnsSpritesEng[i];
        }
    }

    public void SetRusLang()
    {
        settingsTitleText.text = "Настройки";
        volumeSettText.text = "Громкость";
        langSettText.text = "Язык";
        gameOverText.text = "Игра окончена!";
        combText.text = "Комбинации";
        collectionsText.text = "Коллекции";
        simpleCollDicesText.text = "Обычные кости";
        Image engBtnImage = engLangBtn.GetComponent<Image>();
        engBtnImage.sprite = engBtnSprite[1];
        Image rusBtnImage = rusLangBtn.GetComponent<Image>();
        rusBtnImage.sprite = rusBtnSprite[1];
        Image mainMenuBtnImage = mainMenuBtn.GetComponent<Image>();
        mainMenuBtnImage.sprite = mainMenuBtnSprite[1];
        Image combBtnImage = combinationsBtn.GetComponent<Image>();
        combBtnImage.sprite = combinationsBtnsSprites[1];
        for (int i = 0; i < backBtns.Length; i++)
        {
            Image backBtnImage = backBtns[i].GetComponent<Image>();
            backBtnImage.sprite = backBtnSprite[1];
        }

        for (int i = 0; i < mainBtns.Length; i++)
        {
            Image mainBtnImage = mainBtns[i].GetComponent<Image>();
            mainBtnImage.sprite = mainBtnsSpritesRus[i];
        }
        for (int i = 0; i < collectionsBtns.Length; i++)
        {
            Image collectionsBtnImage = collectionsBtns[i].GetComponent<Image>();
            collectionsBtnImage.sprite = collectionBtnsSpritesRus[i];
        }
    }
}

