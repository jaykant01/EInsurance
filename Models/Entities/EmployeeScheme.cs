using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EInsurance.Models.Entities
{
    public class EmployeeScheme
    {
        [Key]
        public int EmployeeeSchemeID { get; set; }

        public int EmployeeId { get; set; }

        [ForeignKey("EmployeeId")]
        public Employee? Employee { get; set; }

        public int SchemeID { get; set; }

        [ForeignKey("SchemeID")]
        public Scheme? Scheme { get; set; }
    }
}
