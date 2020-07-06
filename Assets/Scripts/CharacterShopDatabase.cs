using UnityEngine;

[CreateAssetMenu (fileName = "CharacterShopDatabase", menuName = "Shopping/Characters shop database")]
public class CharacterShopDatabase : ScriptableObject
{
	public Character[] characters;

	public int CharactersCount {
		get{ return characters.Length; }
	}

	public Character GetCharacter (int index)
	{
		return characters [index];
	}

	public void PurchaseCharacter (int index)
	{
		characters [index].isPurchased = true;
	}
}
