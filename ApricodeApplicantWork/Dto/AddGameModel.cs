using System.Collections.Generic;

namespace ApricodeApplicantWork.Dto
{
    public class AddGameModel
    {
        public string Name { get; set; }
        public List<long> GenreIds { get; set; }
        public long CompanyId { get; set; }
    }
}
