using CoreLayer.DTOs;
using CORETest.Utilities;
using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.IServices
{
	public interface IGroupProductService
	{
        IEnumerable<GroupProductDto> GetAllParentGroups();
        IEnumerable<GroupProductDto> GetAllSuperGroups();
        GroupProductDto GetGroupByID(int id);
        OperationResault AddGroupProduct(GroupProductDto groupProductDto);
        OperationResault EditGroupProduct(GroupProductDto groupProductDto);
        OperationResault DeleteGroupProduct(int id);
        GroupProductDto GetGroupBySlug(string slug);
        IEnumerable<GroupProductDto> GetAllchildGroups();
        IEnumerable<GroupProductDto> GetAllGroups();
        Task<IEnumerable<GroupProductDto>> GetAllGroupsasynce();
        IEnumerable<GroupProductDto> GetParentGroups(int id);
        IEnumerable<GroupProductDto> GetGroupsByParentId(int id);
        Task<IEnumerable<GroupProductDto>> GetSubGroupsByID(int Id);
    }
}
