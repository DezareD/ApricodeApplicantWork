using System.Collections.Generic;

namespace ApricodeApplicantWork.Dto
{
    public class UpdateGameModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public List<long> GenreIds { get; set; }
        public long CompanyId { get; set; }
    }
}
