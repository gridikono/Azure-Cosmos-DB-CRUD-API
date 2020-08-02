using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ToDoApp.Models
{
    public class Item
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "isComplete")]
        public bool Completed { get; set; }

       [JsonProperty(PropertyName = "createdDate")]
        public DateTime CreatedDate { get; set; }

        [JsonProperty(PropertyName = "CreatedBy")]
        public String CraetedBy { get; set; }

        [JsonProperty(PropertyName = "editedDate")]
        public DateTime EditedDate { get; set; }

        [JsonProperty(PropertyName = "editedBy")]
        public String EditedBy { get; set; }
    }
}