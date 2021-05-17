using SplitExpenses.BaseEntities;

namespace SplitExpenses.Entities
{
    public class GroupParticipant : BaseEntity
    {
        public int GroupId { get; set; }
        public int ParticipantId { get; set; }
        public bool IsActive { get; set; }
    }
}
