using Microsoft.AspNetCore.Mvc;
using System;

namespace Hackney.Shared.Person.Boundary.Request
{
    public class PersonQueryObject
    {
        [FromRoute(Name = "id")]
        public Guid Id { get; set; }
    }
}
