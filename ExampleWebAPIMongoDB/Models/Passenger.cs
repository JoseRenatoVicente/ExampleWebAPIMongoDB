using ExampleWebAPIMongoDB.Models.Base;
using ExampleWebAPIMongoDB.Models.Enums;
using System;

namespace ExampleWebAPIMongoDB.Models
{
    public class Passenger : EntityBase
    {
        public string Cpf { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public Sex Sex { get; set; }
        public DateTime BirthDate { get; set; }
        public string Email { get; set; }
        public Address Address { get; set; }
    }
}
