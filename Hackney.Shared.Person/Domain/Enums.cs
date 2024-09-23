using System.Text.Json.Serialization;

namespace Hackney.Shared.Person.Domain
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Title
    {
        Dr,
        Master,
        Miss,
        Mr,
        Mrs,
        Ms,
        Mx,
        Other,
        Rabbi,
        Reverend
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum PersonType
    {
        Tenant,
        HouseholdMember,
        Leaseholder,
        Freeholder,
        Occupant,
        HousingOfficer,
        HousingAreaManager
    }
}
