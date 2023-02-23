using UnityEngine;
using UnityEngine.UI;
using System;

public class UIManager : MonoBehaviour
{
  private float mileage;

  [Header("Mileage Props")]
  [SerializeField] private Text mileageText;
  [SerializeField] private float stepsPadding;
  [SerializeField] private float stepsMultiplier;

  [Header("Pause Menu Props")]
  [SerializeField] private GameObject pauseMenu;
  [Range(0.01f, 1f)]
  [SerializeField] private float pauseMenuWidthFactor = 1f;
  [Range(0.01f, 1f)]
  [SerializeField] private float pauseMenuHeightFactor = 1f;


  // Used for scroll velocity
  [Header("Background Manager Instance")]
  [SerializeField] private BackgroundManager bgm;

  // UIManager RectTransform componenet
  private RectTransform rt;

  void Start()
  {
    bgm = bgm.GetComponent<BackgroundManager>();
    rt = GetComponent<RectTransform>();

    InitMileage();
    InitStartScreen();
  }

  void Update()
  {
    mileage += bgm.GetScrollVelocity() * Time.deltaTime * stepsMultiplier;
    SetText();
  }

  void SetText()
  {
    // Update mileage
    mileageText.text = String.Format("{0:#} steps", mileage);
  }

  void InitMileage()
  {
    stepsPadding = 50f;
    stepsMultiplier = 3.0f;
    mileage = 0f;
    mileageText = mileageText.GetComponent<Text>();
    RectTransform mileageRT = mileageText.GetComponent<RectTransform>();
    mileageRT.anchoredPosition = new Vector2(-stepsPadding, mileageRT.anchoredPosition.y - mileageRT.rect.height);
  }

  void InitStartScreen()
  {
    RectTransform pauseMenuRT = pauseMenu.GetComponent<RectTransform>();

    // Get Width and Height
    float width = rt.rect.width * pauseMenuWidthFactor;
    float height = rt.rect.height * pauseMenuHeightFactor;

    // Set
    pauseMenuRT.sizeDelta = new Vector2(width, height);
  }
}
