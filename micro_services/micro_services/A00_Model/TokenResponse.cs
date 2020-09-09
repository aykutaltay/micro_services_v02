using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace micro_services.A00_Model
{
    public class TokenResponse
    {
        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("score")]
        public decimal Score { get; set; }

        [JsonProperty("action")]
        public string Action { get; set; }

        [JsonProperty("error-codes")]
        public List<string> ErrorCodes { get; set; }
    }
}
