using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[JsonConverter(typeof(StringEnumConverter))]

public enum Department
{
    Sales = 1,
    Marketing,
    Development,
    QA,
    HR,
    SEO
}