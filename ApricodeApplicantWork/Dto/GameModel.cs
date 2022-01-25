using ApricodeApplicantWork.Models;
using System.Collections.Generic;

namespace ApricodeApplicantWork.Dto
{
    public class GameModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public Studio Studio { get; set; }
        public List<Genre> Genres { get; set; }
    }
}
