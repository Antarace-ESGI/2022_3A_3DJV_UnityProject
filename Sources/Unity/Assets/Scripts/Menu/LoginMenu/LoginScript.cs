using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace Menu.LoginMenu
{
    [Serializable]
    public class LoginRequest
    {
        public string username;
        public string password;

        public LoginRequest(string username, string password)
        {
            this.username = username;
            this.password = password;
        }
    }

    [Serializable]
    public class TokenResponse
    {
        public string token;
    }

    public class LoginScript : MonoBehaviour
    {
        public Button okButton;
        public InputField usernameField;
        public InputField passwordField;
        public bool registerMode;
        public GameObject successPanel;
        public Text feedbackText;

        public const string BaseUrl = "http://localhost:3000/api";

        private void OnEnable()
        {
            okButton.onClick.AddListener(TaskOnClick);
        }

        private void OnDisable()
        {
            okButton.onClick.RemoveListener(TaskOnClick);
        }

        void TaskOnClick()
        {
            StartCoroutine(Login(usernameField.text, passwordField.text));
        }

        IEnumerator Login(string username, string password)
        {
            var loginRequest = new LoginRequest(username, password);
            var json = JsonUtility.ToJson(loginRequest);

            var url = registerMode ? $"{BaseUrl}/account" : $"{BaseUrl}/session";
            UnityWebRequest request = new UnityWebRequest(url);
            
            request.uploadHandler = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(json));
            request.downloadHandler = new DownloadHandlerBuffer();
            request.method = UnityWebRequest.kHttpVerbPOST;
            request.SetRequestHeader("Content-Type", "application/json");
            request.useHttpContinue = false;
            request.redirectLimit = 0;
            request.timeout = 60;

            yield return request.SendWebRequest();
            
            if (request.result != UnityWebRequest.Result.Success)
            {
                feedbackText.text = request.error;
            }
            else
            {
                var tokenResponse = JsonUtility.FromJson<TokenResponse>(request.downloadHandler.text);
                Debug.Log("Got JWT!");
                PlayerPrefs.SetString("token", tokenResponse.token);
                gameObject.SetActive(false);
                successPanel.SetActive(true);
            }
        }
    }
}