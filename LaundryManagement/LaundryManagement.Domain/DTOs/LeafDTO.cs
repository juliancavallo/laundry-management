
namespace LaundryManagement.Domain.DTOs
{
    public class LeafDTO : ComponentDTO
    {
        public override IList<ComponentDTO> Children
        {
            get { throw new NotImplementedException(); }
        }
        public override void AddChildren(ComponentDTO dto) => throw new NotImplementedException();
        public override void RemoveChildren(ComponentDTO dto) => throw new NotImplementedException();
    }
}
