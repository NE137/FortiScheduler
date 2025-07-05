using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace FortiScheduler
{
    internal class Connection
    {
        // Constructor to initialize the connection with a URL
        public Connection(string ip, int port, string apiKey)
        {
            IP = ip;
            Port = port;
            ApiKey = apiKey;
        }


        // Property to hold the URL of the connection
        public string IP { get; private set; }
        public int Port { get; private set; }
        = 443;
        public string ApiKey { get; private set; }
        = string.Empty;
        public bool IsConnected { get; private set; } = false;
        public bool isUpdated { get; private set; } = false;
        public List<string> Schedules { get; private set; } = new List<string>();


        // Method to connect to the URL
        public async Task ConnectAsync()
        {
            // Simulate a connection attempt
            bool connectionSucessful = false;
            if (!string.IsNullOrEmpty(IP) && Port > 0 && ApiKey != null)
            {
                // Attempt to connect to the fortigate device using the API, checking that the API key is valid and the device is reachable
                try
                {
                    // Ignore SSL certificate errors (not recommended for production code)
                    HttpClientHandler clientHandler = new HttpClientHandler();
                    clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

                    HttpClient client = new HttpClient(clientHandler);

                    if (Port == 443)
                    {
                        // Use HTTPS for port 443
                        client.BaseAddress = new Uri($"https://{IP}");
                    }
                    else if (Port == 80)
                    {
                        // Use HTTP for port 80
                        client.BaseAddress = new Uri($"http://{IP}");
                    }
                    else
                    {
                        // Use HTTPS for other ports
                        client.BaseAddress = new Uri($"https://{IP}:{Port}");
                    }

                    // Set a timeout for the request
                    client.Timeout = TimeSpan.FromSeconds(3);

                    // Apply necessary headers for the request
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {ApiKey}");
                    client.DefaultRequestHeaders.Add("User-Agent", "FortiScheduler/1.0");
                    client.DefaultRequestHeaders.Add("X-Requested-With", "XMLHttpRequest");
                    client.DefaultRequestHeaders.Add("Cache-Control", "no-cache");
                    client.DefaultRequestHeaders.Add("Pragma", "no-cache");
                    client.DefaultRequestHeaders.Add("Connection", "keep-alive");

                    // Check if the API key is valid by making a simple request to the system status endpoint
                    var response = await client.GetAsync("/api/v2/monitor/system/status");
                    connectionSucessful = response.IsSuccessStatusCode;

                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Connection failed: {ex.Message}");
                }


                if (connectionSucessful) { IsConnected = true; } else { IsConnected = false; }
            }
        }

        // Method to receive the schedules from the Fortigate device
        public async Task<List<string>> GetSchedulesAsync()
        {
            if (!IsConnected)
            {
                throw new InvalidOperationException("Not connected to the Fortigate device.");
            }
            List<string> schedules = new List<string>();
            try
            {
                // Create a new HttpClient instance

                HttpClientHandler clientHandler = new HttpClientHandler();
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

                HttpClient client = new HttpClient(clientHandler);

                if (Port == 443)
                {
                    // Use HTTPS for port 443
                    client.BaseAddress = new Uri($"https://{IP}");
                }
                else if (Port == 80)
                {
                    // Use HTTP for port 80
                    client.BaseAddress = new Uri($"http://{IP}");
                }
                else
                {
                    // Use HTTPS for other ports
                    client.BaseAddress = new Uri($"https://{IP}:{Port}");
                }

                // Set a timeout for the request
                client.Timeout = TimeSpan.FromSeconds(3);

                // Apply necessary headers for the request
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {ApiKey}");
                client.DefaultRequestHeaders.Add("User-Agent", "FortiScheduler/1.0");
                client.DefaultRequestHeaders.Add("X-Requested-With", "XMLHttpRequest");
                client.DefaultRequestHeaders.Add("Cache-Control", "no-cache");
                client.DefaultRequestHeaders.Add("Pragma", "no-cache");
                client.DefaultRequestHeaders.Add("Connection", "keep-alive");


                // Set the base address and headers
                client.BaseAddress = new Uri($"https://{IP}:{Port}");

                // Make a GET request to the schedules endpoint
                HttpResponseMessage response = await client.GetAsync("/api/v2/cmdb/firewall.schedule/onetime");

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();

                    JsonNode jsonResponse = JsonNode.Parse(content);
                    if (jsonResponse != null && jsonResponse["results"] is JsonArray results && results.Count > 0)
                    {
                        // Clear the existing schedules
                        schedules.Clear();
                        // Iterate through the results and extract schedule names
                        foreach (var item in results)
                        {
                            if (item is JsonObject schedule && schedule["name"] is JsonValue name)
                            {
                                schedules.Add(name.ToString());
                            }
                        }
                        // Check if any schedules were found
                        if (schedules.Count == 0)
                        {
                            MessageBox.Show("No schedules found.");
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error retrieving schedules: {ex.Message}");
            }
            return schedules;
        }

        // Method to update a schedule on the Fortigate device
        public async Task UpdateScheduleAsync(string scheduleName, DateTime startTime, DateTime endTime)
        {
            
            try
            {
                // Create a new HttpClient instance
                HttpClientHandler clientHandler = new HttpClientHandler();
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

                HttpClient client = new HttpClient(clientHandler);
                if (Port == 443)
                {
                    // Use HTTPS for port 443
                    client.BaseAddress = new Uri($"https://{IP}");
                }
                else if (Port == 80)
                {
                    // Use HTTP for port 80
                    client.BaseAddress = new Uri($"http://{IP}");
                }
                else
                {
                    // Use HTTPS for other ports
                    client.BaseAddress = new Uri($"https://{IP}:{Port}");
                }

                
                var request = new HttpRequestMessage(HttpMethod.Put, $"https://{IP}/api/v2/cmdb/firewall.schedule/onetime/{scheduleName}");
                request.Headers.Add("Accept", "application/json");
                request.Headers.Add("User-Agent", "FortiScheduler/1.0");
                request.Headers.Add("X-Requested-With", "XMLHttpRequest");
                request.Headers.Add("Cache-Control", "no-cache");
                request.Headers.Add("Pragma", "no-cache");
                request.Headers.Add("Connection", "keep-alive");
                request.Headers.Add("Authorization", $"Bearer {ApiKey}");

                var st = startTime.ToString("HH:mm yyyy/MM/dd");
                var et = endTime.ToString("HH:mm yyyy/MM/dd");

                var json = $"{{\"start\": \"{st.Replace("-", "/")}\", \"end\": \"{et.Replace("-", "/")}\"}}";
                var content = new StringContent(json, null, "application/json");

                request.Content = content;
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();

                if (response.IsSuccessStatusCode) this.isUpdated = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating schedule: {ex.Message}");
                this.isUpdated = false;
            }
        }
    }
}
