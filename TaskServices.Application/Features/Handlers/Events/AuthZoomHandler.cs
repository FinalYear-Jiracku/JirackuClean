using AutoMapper;
using Google.Apis.Auth.OAuth2;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using TaskServices.Application.Features.Commands.Events;
using TaskServices.Application.Interfaces;
using TaskServices.Application.Interfaces.IServices;
using TaskServices.Domain.Entities;

namespace TaskServices.Application.Features.Handlers.Events
{
    public class AuthZoomHandler : IRequestHandler<AuthZoomCommand, string>
    {
        
        private readonly HttpClient _httpClient;
        public static string ClientId = "spqBcTkS4egeysQDEQuBg";
        public static string ClientSecret = "QY7hib4KelNSnzQyUt2t9iteovruDwwd";
        public const string TokenEndpoint = "https://zoom.us/oauth/token";

        public AuthZoomHandler(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<string> Handle(AuthZoomCommand command, CancellationToken cancellationToken)
        {
            var joinUrl = "";
            string base64Credentials = Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes($"{ClientId}:{ClientSecret}"));
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64Credentials);

            var parameters = new Dictionary<string, string>
            {
                { "grant_type", "authorization_code" },
                { "code", command.Code },
                { "redirect_uri", "http://localhost:3000/meeting" },
            };

            var response = await _httpClient.PostAsync(TokenEndpoint, new FormUrlEncodedContent(parameters));

            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                var json = JObject.Parse(responseBody);
                var token = json["access_token"]?.Value<string>();
                var createMeetingEndpoint = "https://api.zoom.us/v2/users/me/meetings";
                var meetingInfo = new
                {
                    topic = "hellooo",
                    type = 2,
                    start_time = "2023-08-12T02:38:42.791Z",
                    duration = "3",
                    settings = new
                    {
                        host_video = true,
                        participant_video = true,
                        mute_upon_entry = true,
                        watermark = true,
                        audio = "voip",
                        auto_recording = "cloud"
                    }
                };
                var createMeetingContent = new StringContent(JsonConvert.SerializeObject(meetingInfo), Encoding.UTF8, "application/json");

                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
                    var createMeetingResponse = await client.PostAsync(createMeetingEndpoint, createMeetingContent);
                    if (createMeetingResponse.IsSuccessStatusCode)
                    {
                        var createMeetingResponseBody = await createMeetingResponse.Content.ReadAsStringAsync();
                        var meetingJson = JObject.Parse(createMeetingResponseBody);
                        joinUrl = meetingJson["join_url"].Value<string>();
                    }
                    else
                    {
                        Console.WriteLine($"Error creating meeting: {createMeetingResponse.StatusCode}");
                    }
                    return joinUrl;
                }
            }
            else
            {
                Console.WriteLine($"Error: {response.StatusCode}");
            }
         
            return joinUrl;
        }
      
    }
}
