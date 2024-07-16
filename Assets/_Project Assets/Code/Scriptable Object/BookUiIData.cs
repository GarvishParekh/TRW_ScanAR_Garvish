using UnityEngine;

[CreateAssetMenu(fileName = "Book Ui Data", menuName = "Scriptable / Book Ui Data")]
public class BookUiIData : ScriptableObject
{
    public BookUiInformation[] trwBooks;
}

[System.Serializable]
public class BookUiInformation
{
    public string bookDisplayName;
    public Sprite bookCoverSprite;
}
