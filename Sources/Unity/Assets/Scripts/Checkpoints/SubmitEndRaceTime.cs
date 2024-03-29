﻿using System;
using System.Collections;
using Menu.LoginMenu;
using UnityEngine;
using UnityEngine.Networking;

namespace Checkpoints
{
    public class SubmitEndRaceTime
    {
        [Serializable]
        public class TimeRequest
        {
            public int time;
            public string vehicle;
            public string track;

            public TimeRequest(int time, string vehicle, string track)
            {
                this.time = time;
                this.vehicle = vehicle;
                this.track = track;
            }
        }
        
        /// <summary>
        /// Sends a request to save the given time in seconds.
        /// </summary>
        /// <param name="time"></param>
        /// <param name="vehicle">Name of the vehicle used to make this time</param>
        /// <returns></returns>
        public static IEnumerator SendTime(int time, string vehicle, string track)
        {
            var loginRequest = new TimeRequest(time, vehicle, track);
            var json = JsonUtility.ToJson(loginRequest);
            var token = PlayerPrefs.GetString("token");

            UnityWebRequest request = new UnityWebRequest($"{LoginScript.BaseUrl}/submitTime");
            
            request.uploadHandler = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(json));
            request.downloadHandler = new DownloadHandlerBuffer();
            request.method = UnityWebRequest.kHttpVerbPOST;
            request.SetRequestHeader("Content-Type", "application/json");
            request.SetRequestHeader("Authorization", $"Bearer {token}");
            request.useHttpContinue = false;
            request.redirectLimit = 0;
            request.timeout = 60;

            yield return request.SendWebRequest();

            Debug.Log(request.result != UnityWebRequest.Result.Success ? request.error : "Registered time!");
        }
    }
}