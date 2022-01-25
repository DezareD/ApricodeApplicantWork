namespace ApricodeApplicantWork.Models
{
    /// <summary>
    /// Модель для хранения данных о играх связанных с жанром. Связь: многие ко многим
    /// </summary>
    public class GameGenre : BaseEntry
    {
        public long GameId { get; set; }
        public long GenreId { get; set; }
    }
}
