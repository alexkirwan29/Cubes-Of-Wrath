using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Cow.UI;
using Cow;
using Newtonsoft.Json;

public class UploadToCow : MonoBehaviour
{
    public LevelManager level;
    public ProgressBar progBar;
    public Button uploadButton;
    public InputField Title;
    public InputField Description;
    public InputField Author;
    public string uploadUrl;

    WWW www;
    void Start()
    {
        progBar.maxValue = 1f;
    }
    public void Cancel()
    {

    }
    public void BeginUpload()
    {
        uploadButton.interactable = false;
        StartCoroutine(Uploader());
    }
    void Update()
    {
        if (www != null)
            progBar.Value = www.uploadProgress;
        else
            progBar.Value = 0;
    }
    void UploadComplete()
    {
        Debug.Log("Is it done?");
        Debug.Log(www.text);
    }
    IEnumerator Uploader()
    {
        WWWForm form = new WWWForm();
        form.AddField("action", "upload level");
        form.AddField("file", "file");
        form.AddBinaryData("file", LevelIO.GetBytes(level.Level), "test.cowl", "application/octet-stream");

        www = new WWW(uploadUrl,form);
        yield return www;
        if (www.error != null)
        {
            Debug.LogError(www.error);
        }
        else
        {
            UploadComplete();
        }
    }
}