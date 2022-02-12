using System.ComponentModel;

namespace AuthService.Constants
{
    enum RoleType
    {
        [Description("Tạo lập")]
        Init = 1,
        [Description("Kiểm soát")]
        Review = 2,
        [Description("Duyệt lệnh")]
        Approve = 3
    }
}
