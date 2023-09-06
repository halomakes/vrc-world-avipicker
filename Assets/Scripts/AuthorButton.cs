
using UdonSharp;
using UnityEngine;

[UdonBehaviourSyncMode(BehaviourSyncMode.None)]
public class AuthorButton : UdonSharpBehaviour
{
    [SerializeField]
    public string AuthorId;

    //public AvatarPicker picker;

    public void OnButtonClicked()
    {
        Debug.Log($"Clicked {AuthorId}");
        var picker = GetComponentInParent<AvatarPicker>();
        Debug.Log($"Picker {picker}");
        picker.ChangeAuthor(AuthorId);
    }
}
