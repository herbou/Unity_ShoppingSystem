using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
using DG.Tweening;

public class CharacterItemUI : MonoBehaviour
{
	[SerializeField] Color itemNotSelectedColor;
	[SerializeField] Color itemSelectedColor;

	[Space (20f)]
	[SerializeField] Image characterImage;
	[SerializeField] TMP_Text characterNameText;
	[SerializeField] Image characterSpeedFill;
	[SerializeField] Image characterPowerFill;
	[SerializeField] TMP_Text characterPriceText;
	[SerializeField] Button characterPurchaseButton;

	[Space (20f)]
	[SerializeField] Button itemButton;
	[SerializeField] Image itemImage;
	[SerializeField] Outline itemOutline;

	//--------------------------------------------------------------
	public void SetItemPosition (Vector2 pos)
	{
		GetComponent <RectTransform> ().anchoredPosition += pos;
	}

	public void SetCharacterImage (Sprite sprite)
	{
		characterImage.sprite = sprite;
	}

	public void SetCharacterName (string name)
	{
		characterNameText.text = name;
	}

	public void SetCharacterSpeed (float speed)
	{
		characterSpeedFill.fillAmount = speed / 100;
	}

	public void SetCharacterPower (float power)
	{
		characterPowerFill.fillAmount = power / 100;
	}

	public void SetCharacterPrice (int price)
	{
		characterPriceText.text = price.ToString ();
	}

	public void SetCharacterAsPurchased ()
	{
		characterPurchaseButton.gameObject.SetActive (false);
		itemButton.interactable = true;

		itemImage.color = itemNotSelectedColor;
	}

	public void OnItemPurchase (int itemIndex, UnityAction<int> action)
	{
		characterPurchaseButton.onClick.RemoveAllListeners ();
		characterPurchaseButton.onClick.AddListener (() => action.Invoke (itemIndex));
	}

	public void OnItemSelect (int itemIndex, UnityAction<int> action)
	{
		itemButton.interactable = true;

		itemButton.onClick.RemoveAllListeners ();
		itemButton.onClick.AddListener (() => action.Invoke (itemIndex));
	}

	public void SelectItem ()
	{
		itemOutline.enabled = true;
		itemImage.color = itemSelectedColor;
		itemButton.interactable = false;
	}

	public void DeselectItem ()
	{
		itemOutline.enabled = false;
		itemImage.color = itemNotSelectedColor;
		itemButton.interactable = true;
	}

	public void AnimateShakeItem ()
	{
		//End all animations first
		transform.DOComplete ();

		transform.DOShakePosition (1f, new Vector3 (8f, 0, 0), 10, 0).SetEase (Ease.Linear);
	}
}
