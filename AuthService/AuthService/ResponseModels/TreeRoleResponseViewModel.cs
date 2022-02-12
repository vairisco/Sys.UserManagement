namespace AuthService.Models
{
    public class TreeRoleResponseViewModel
    {
        public List<NodeRole> NodeRoles { get; set; }
    }

    public class NodeRole
    {
        public string Title { get; set; }
        public string Value { get; set; }
        public string Key { get; set; }
        public List<NodeRole> NodeRoles { get; set; }
    }
}
