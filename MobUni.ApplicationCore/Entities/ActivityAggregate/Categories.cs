
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace MobUni.ApplicationCore.Entities.ActivityAggregate
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum Categories
    {
        SPOR,
        MUZIK,
        TEKNOLOJI,
        TARIH,
        DIN,
        EDEBIYAT,
        HUKUK
    }
}
