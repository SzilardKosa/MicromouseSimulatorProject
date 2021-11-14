using AspNetCore.Identity.Mongo.Model;
using MicromouseSimulatorBackend.BLL.Models;
using MicromouseSimulatorBackend.BLL.ServiceInterfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MicromouseSimulatorBackend.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<MongoUser> _userManager;
        private readonly SignInManager<MongoUser> _signInManager;

        public UserService(UserManager<MongoUser> userManager, SignInManager<MongoUser> signInManager)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
        }


        public async Task ChangePassword(ChangePassword change, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                throw new Exception("User doesn't exist!");
            var isPasswordValid = await _signInManager.UserManager.CheckPasswordAsync(user, change.OldPassword);
            if (!isPasswordValid)
                throw new Exception("Invalid old password!");

            var result = await _userManager.ChangePasswordAsync(user, change.OldPassword, change.NewPassword);
            if (!result.Succeeded)
            {
                throw new Exception("Password couldn't be changed! " + result.Errors.First().Description);
            }

        }
    }
}
