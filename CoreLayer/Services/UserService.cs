using CoreLayer.DTOs;
using CoreLayer.IServices;
using CoreLayer.Mappers;
using CORETest.Utilities;
using DataLayer;
using DataLayer.Context;
using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Services
{
	public class UserService : IUserService
	{
		private ShopContext _shopcontext;
		public UserService(ShopContext shopContext)
		{
			_shopcontext = shopContext;

		}
		public AdminDto LoginUsers(AdminDto loginViewModel)
		{
			var pass = loginViewModel.Password.EncodeToMd5();
			var user = _shopcontext.Users.SingleOrDefault(u => u.PhoneNumber == loginViewModel.PhoneNumber && u.Password == pass);
			if (user != null)
			{
				return AdminMapper.MapToLoginadmin(user);
			}
			return null;
		}

		public OperationResault SigninUsers(AdminDto signinViewModel)
		{
			var pass = signinViewModel.Password.EncodeToMd5();
			signinViewModel.Password = pass;
			if (!_shopcontext.Users.Any(u => u.PhoneNumber == signinViewModel.PhoneNumber))
			{
				var user = AdminMapper.MapToadmin(signinViewModel);
				_shopcontext.Users.Add(user);
				_shopcontext.SaveChanges();
				return OperationResault.Success();
			}
			return OperationResault.Error("کاربری با این نام قبلا ثبت شده");
		}
		public List<UserDto> GetAllUsers()
		{
			var users = _shopcontext.Users.AsQueryable().Select(u => new UserDto()
			{
				Id = u.Id,
				Name = u.FullName,
				RoleName = u.Role

			}).ToList();
			return (users);
		}

		public DataLayer.Entities.User GetUser(string PhoneNumber)
		{
			return _shopcontext.Users.SingleOrDefault(u=>u.PhoneNumber==PhoneNumber || u.Email==PhoneNumber);
		}

		public OperationResault ActivateUser(User user)
		{
			try
			{
				_shopcontext.Users.Update(user);
				_shopcontext.SaveChanges();
				return OperationResault.Success();
			}
			catch (Exception ex)
			{
				return OperationResault.Error(ex.Message);
			}
		}
	}
}
