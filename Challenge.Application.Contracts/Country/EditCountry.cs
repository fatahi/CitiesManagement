using Framework.Application;
using System.ComponentModel.DataAnnotations;

namespace Challenge.Application.Contracts.Country
{
    public class EditCountry : CreateCountry
    {
        public int Id { get; set; }
    }
}
