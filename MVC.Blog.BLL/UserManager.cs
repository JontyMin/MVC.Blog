using System;
using System.Data.Entity;
using System.Linq;
using MVC.Blog.IBLL;
using System.Threading.Tasks;
using MVC.Blog.DTO;
using MVC.Blog.IDAL;
using MVC.Blog.DAL;
using MVC.Blog.Model;

namespace MVC.Blog.BLL
{
    public class UserManager : IUserManager
    {
       /// <summary>
       /// 更改密码
       /// </summary>
       /// <param name="email">邮箱</param>
       /// <param name="oldPwd">旧密码</param>
       /// <param name="newPwd">新密码</param>
       /// <returns></returns>
        public async Task ChangePassword(string email, string oldPwd, string newPwd)
        {
            using (IUserService userService= new UserService())
            {
                if (await userService.GetAll().AnyAsync(x=>x.Email==email && x.Password==oldPwd))
                {
                    var user = await userService.GetAll().FirstAsync(x => x.Email == email);
                    user.Password = newPwd;
                    await userService.EditAsync(user);
                }
            }
        }
        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="email">邮箱</param>
        /// <param name="siteName">网站名</param>
        /// <param name="imagePath">头像</param>
        /// <returns></returns>
        public async Task ChangeUserInformation(string email, string siteName, string imagePath)
        {
            using (IUserService userService = new UserService())
            {
                var user = await userService.GetAll().FirstAsync(x => x.Email == email);
                user.SiteName = siteName;
                user.ImagePath = imagePath;
                await userService.EditAsync(user);
            }
        }
        /// <summary>
        /// 根据邮箱获取用户
        /// </summary>
        /// <param name="email">邮箱</param>
        /// <returns></returns>
        public async Task<UserInformationDto> GetUserByEmail(string email)
        {
            using (IUserService userService = new UserService())
            {
                if (await userService.GetAll().AnyAsync(x => x.Email == email))
                {
                    return await userService.GetAll().Where(x => x.Email == email).Select(x => 
                        new UserInformationDto()
                    {
                        Id=x.Id,
                        Email = x.Email,
                        FansCount = x.FansCount,
                        ImagePath = x.ImagePath,
                        SiteName = x.SiteName,
                        FocusCount = x.FocusCount
                    }).FirstAsync();
                }
                else
                {
                    throw new ArgumentException("邮箱地址异常");
                }
            }
        }
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="email">邮箱</param>
        /// <param name="password">密码</param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool Login(string email, string password,out Guid userId)
        {
            using (IUserService userService=new UserService())
            {
                var user = userService.GetAll().FirstOrDefaultAsync(x => x.Email == email && x.Password == password);
                user.Wait();
                var data = user.Result;
                if (data == null)
                {
                    userId=new Guid();
                    return false;
                }
                userId = data.Id;
                return true;
            }
        }
        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="email">邮箱</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        public async Task Register(string email, string password)
        {
            using (IUserService userService=new UserService())
            {
                await userService.CreateAsync(new User()
                {
                    Email = email,
                    Password = password,
                    SiteName = "默认的小站",
                    ImagePath = "default.png"
                });
            }
        }
    }
}