using System.Security.AccessControl;
using UdonSharp;
using UnityEngine;
using UnityEngine.UI;
using VRC.SDK3.Components;
using VRC.SDK3.Data;

[UdonBehaviourSyncMode(BehaviourSyncMode.None)]
public class AvatarPicker : UdonSharpBehaviour
{
    [SerializeField]
    [TextArea]
    private string avatarDefinitions;

    [SerializeField]
    private Text errorMessage;

    [SerializeField]
    private AuthorButton[] authorButtons;

    [SerializeField]
    private float buttonGutter = 0.02f;

    [SerializeField]
    private VRCAvatarPedestal[] pedestals;

    [SerializeField]
    private Text HeaderText;

    [SerializeField]
    private Text PageText;

    private DataDictionary avatars;

    private int page = 0;
    private string author = null;
    private int pages = 1;

    void Start()
    {
        DeserializeList();
    }

    private void DeserializeList()
    {
        if (VRCJson.TryDeserializeFromJson(avatarDefinitions, out var deserializedJson) && deserializedJson.TokenType == TokenType.DataDictionary)
        {
            var dictionary = deserializedJson.DataDictionary;
            avatars = dictionary;
        }
        else
        {
            ShowError("Couldn't deserialize avatar list");
        }
    }

    private void ShowError(string message)
    {
        errorMessage.text = message;
        errorMessage.enabled = true;
        Debug.Log($"ERROR: {errorMessage}");
    }

    private void HideError()
    {
        errorMessage.enabled = false;
    }

    public void ChangeAuthor(string authorId)
    {
        Debug.Log($"Resetting for {authorId}");
        author = authorId;
        page = 0;
        UpdatePedestals();
    }

    public void ChangePage(bool isForward)
    {
        if (isForward)
        {
            if (page == pages - 1)
                return;
            page++;
        }
        else
        {
            if (page == 0)
                return;
            page--;
        }

        UpdatePedestals();
    }

    public void UpdatePedestals()
    {
        if (!avatars.TryGetValue(new DataToken(author), TokenType.DataList, out var listToken))
        {
            ShowError($"No avatars found for author {author}");
            return;
        }
        var pageSize = pedestals.Length;
        var offset = pageSize * page;
        pages = (listToken.DataList.Count + pageSize - 1) / pageSize;
        Debug.Log($"Showing avatars for {author} starting at {offset}");
        for (var i = 0; i < pageSize; i++)
        {
            var pedestal = pedestals[i];
            if (listToken.DataList.TryGetValue(offset + i, TokenType.String, out var avatarIdToken))
            {
                pedestal.SwitchAvatar(avatarIdToken.String);
                pedestal.gameObject.SetActive(true);
            }
            else // no avatar this time
            {
                pedestal.gameObject.SetActive(false);
            }
        }
        
        UpdateLabels();
    }

    private void UpdateLabels()
    {
        if (HeaderText != null)
            HeaderText.text = $"{author}{(author.EndsWith("s") ? "'" : "'s")} Avatars";
        if (PageText != null)
            PageText.text = $"Page {page + 1} / {pages}";
    }
}
