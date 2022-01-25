namespace ApricodeApplicantWork.Models
{
    /// <summary>
    /// Модель дял хранения данных о игре
    /// </summary>
    public class Game : BaseEntry
    {
        public string Name { get; set; }
        public long CompanyId { get; set; }
    }
}
