using CoreLayer.DTOs;
using DataLayer.Entities;

namespace CoreLayer.Mappers
{
	public static class AdminMapper
	{
		public static User MapToadmin(AdminDto admin)
		{
			return new User()
			{
				Email = admin.Email,
				FullName = admin.FullName,
				Password = admin.Password,
				PhoneNumber = admin.PhoneNumber,
				Role = admin.Role,
				ActiveCode = admin.ActiveCode,
				IsActive = admin.isActive,
				Id = admin.Id
			};
		}
		public static AdminDto MapToLoginadmin(DataLayer.Entities.User admin)
		{
			return new AdminDto()
			{
				FullName = admin.FullName,
				Email = admin.Email,
				Id = admin.Id,
				Role=admin.Role
			};
		}
	}
}
