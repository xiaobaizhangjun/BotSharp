﻿using BotSharp.Core.Abstractions;
using BotSharp.Core.Agents;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BotSharp.Core.Engines.SpaCy
{
    public class SpaCyProvider : INlpPipeline
    {
        public IConfiguration Configuration { get; set; }
        public async Task<bool> Train(Agent agent, JObject data, PipeModel meta)
        {
            var client = new RestClient(Configuration.GetSection("SpaCyProvider:Url").Value);
            var request = new RestRequest("load", Method.GET);
            var response = client.Execute<Result>(request);

            meta.Meta = JObject.FromObject(response.Data);

            return response.IsSuccessful;
        }

        public async Task<bool> Predict(Agent agent, JObject data, PipeModel meta)
        {
            return true;
        }

        private class Result
        {
            [JsonProperty("spaCy ver")]
            public string Version { get; set; }
            [JsonProperty("models")]
            public string Models { get; set; }
            [JsonProperty("python ver")]
            public string Python { get; set; }
        }
    }
}
