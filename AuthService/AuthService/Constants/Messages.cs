using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthService.Constants
{
    public static class Messages
    {
        public static string Error405 => "Người dùng không có quyền!";
        public static string Error400 => "Dữ liệu đầu vào không đúng!";
        public static string Error404 => "Yêu cầu không hợp lệ!";
        public static string Successfully => "Thành công!";
        public static string RegisterSuccessfully => "Đăng ký tài khoản thành công!";
        public static string NotFoundUser => "Không tìm thấy user";

    }
}
