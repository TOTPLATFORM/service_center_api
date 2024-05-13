using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ServiceCenter.Domain.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ContactStatus
{
	Lead , 
	Oppurtienty,
	Cancelled
}
