using MicromouseSimulatorBackend.BLL.Models;
using System.ComponentModel.DataAnnotations;

namespace MicromouseSimulatorBackend.API.DTOs
{
    public class AlgorithmDTO
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [RegularExpression(@"^Python$|^C$|^C\+\+$", ErrorMessage = "The Programming Language must be either 'Python', 'C' or 'C++' only.")]
        public string Language { get; set; }
        [Required]
        public string CodeText { get; set; }

        // Because of the way JSON Deserialization works (first an object is created, then
        // its properties set), we need a default constructor.
        public AlgorithmDTO()
        {

        }
        public AlgorithmDTO(Algorithm algorithm)
        {
            this.Id = algorithm.Id;
            this.Name = algorithm.Name;
            this.Language = algorithm.Language;
            this.CodeText = algorithm.CodeText;
        }

        public Algorithm ToEntity()
        {
            return new Algorithm()
            {
                Id = this.Id,
                Name = this.Name,
                Language = this.Language,
                CodeText = this.CodeText,
            };
        }
    }
}
