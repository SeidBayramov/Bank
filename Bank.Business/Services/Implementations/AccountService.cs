using Bank.Business.Exceptions.Account;
using Bank.Business.Exceptions.Common;
using Bank.Business.Helpers;
using Bank.Business.Services.Interface;
using Bank.Business.ViewModels.Account;
using Bank.Core.Entities.Account;
using Bank.DAL.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Business.Services.Implementations
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly LinkGenerator _linkGenerator;

        public AccountService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager, AppDbContext context, LinkGenerator linkGenerator, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _context = context;
            _linkGenerator = linkGenerator;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<string>> SendConfirmEmailAddress(AppUser user)
        {
            Random random = new Random();
            var data = new List<string>();


            string token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            string pincode = $"{random.Next(1000, 10000)}";

            SendConfirmationService.SendEmailMessage(toUser: user.Email, webUser: user.Name, pincode: pincode);

            data.Add(token);
            data.Add(pincode);
            data.Add(user.Id);

            return data;
        }
        public async Task<bool> ConfirmEmailAddress(ConfrimEmailVm vm, string userId, string token, string pincode)
        {
            var postPincode = $"{vm.Number1}{vm.Number2}{vm.Number3}{vm.Number4}";

            if (pincode == postPincode)
            {
                var user = await _userManager.FindByIdAsync(userId);
                await _userManager.ConfirmEmailAsync(user, token);

                return true;
            }

            return false;
        }

        public async Task CreateRoles()
        {
            foreach (var item in Enum.GetValues(typeof(UserRole)))
            {
                if (!await _roleManager.RoleExistsAsync(item.ToString()))
                {
                    await _roleManager.CreateAsync(new IdentityRole(item.ToString()));
                }
            }
        }

        public async Task Login(LoginVm vm)
        {
            var exists = vm.LoginName == null || vm.Password == null;

            if (exists) throw new ObjectParamsNullException("Object parameters is required!", nameof(vm.LoginName));

            var user = await _userManager.FindByEmailAsync(vm.LoginName);
            if (user is null)
            {
                user = await _userManager.FindByNameAsync(vm.LoginName);

                if (user is null) throw new UserNotFoundException("Username/Email or Password is not valid!", nameof(vm.LoginName));
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, vm.Password, true);

            if (!result.Succeeded)
            {
                throw new UserNotFoundException("Username/Email or Password is not valid!", nameof(vm.Password));
            }

            await _signInManager.SignInAsync(user, true);

        }

        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<List<string>> Register(RegisterVm vm)
        {

            if (vm.UserName is null || vm.Name is null || vm.Surname is null ||
                vm.Email is null || vm.Password is null || vm.ConfirmdPassword is null)
            {
                throw new ObjectParamsNullException("Object parameters is required!", nameof(vm.UserName));
            }

            var usedEmail = await _userManager.FindByEmailAsync(vm.Email);

            if (usedEmail is null)
            {
                AppUser newUser = new()
                {
                    Name = vm.Name,
                    Surname = vm.Surname,
                    Email = vm.Email,
                    UserName = vm.UserName
                };

                var result = await _userManager.CreateAsync(newUser, vm.Password);

                if (!result.Succeeded)
                {
                    foreach (var item in result.Errors)
                    {
                        throw new UserRegistrationException($"{item.Description}", nameof(item));
                    }
                }
                await _userManager.AddToRoleAsync(newUser, UserRole.Moderator.ToString());

                return await SendConfirmEmailAddress(newUser);

            }
            else
            {
                throw new UsedEmailException("This email address used before, try another!", nameof(vm.Email));
            }
        }

        //public async Task Subscription(SubscribeVM vm)
        //{
        //    var existsEmail = await _context.Subscribes.FirstOrDefaultAsync(x => x.EmailAddress == vm.EmailAddress);

        //    if (existsEmail is null)
        //    {
        //        Subscribe newSubscription = new()
        //        {
        //            EmailAddress = vm.EmailAddress,
        //            CreatedDate = DateTime.Now,
        //            UpdatedDate = DateTime.Now,
        //        };

        //        await _context.Subscribes.AddAsync(newSubscription);
        //        await _context.SaveChangesAsync();
        //        SendMessageService.SendEmailMessage(toUser: vm.EmailAddress, webUser: "Amado Team", pincode: "1000");
        //    }
        //    else
        //    {
        //        throw new UsedEmailException("This email address used before, try another!", nameof(vm.EmailAddress));
        //    }
        //}
    }
}