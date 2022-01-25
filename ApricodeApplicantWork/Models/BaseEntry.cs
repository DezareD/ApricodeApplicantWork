using System.ComponentModel.DataAnnotations;

namespace ApricodeApplicantWork.Models
{
    /// <summary>
    /// Базовый класс для моделей в базе данных.
    /// </summary>
    public class BaseEntry
    {
        [Key]
        public long Id { get; set; }
    }
}
