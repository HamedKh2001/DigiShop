using CoreLayer.DTOs;
using CORETest.Utilities;
using DataLayer.Entities;
using System.Collections.Generic;

namespace CoreLayer.IServices
{
	public interface IUserService
	{
		OperationResault SigninUsers(AdminDto signinViewModel);
		AdminDto LoginUsers(AdminDto loginViewModel);
		List<UserDto> GetAllUsers();
		User GetUser(string PhoneNumber);
		OperationResault ActivateUser(User user);
	}
}
