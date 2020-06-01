using System;
using System.Threading.Tasks;
using MVC.Blog.DTO;

namespace MVC.Blog.IBLL
{
    public interface IUserManager
    {
        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task Register(string email, string password);
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        bool Login(string email, string password, out Guid userId);
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="email"></param>
        /// <param name="oldPwd"></param>
        /// <param name="newPwd"></param>
        /// <returns></returns>
        Task ChangePassword(string email, string oldPwd, string newPwd);
        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="email"></param>
        /// <param name="siteName"></param>
        /// <param name="imagePath"></param>
        /// <returns></returns>
        Task ChangeUserInformation(string email, string siteName, string imagePath);
        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        Task<UserInformationDto> GetUserByEmail(string email);
    }
}