using CoreLayer.DTOs;
using CORETest.Utilities;
using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.IServices
{
	public interface IProductCommentService
	{
		Task<List<Productcomment>> GetCommentProduct(int productID);
		Task<OperationResault> Addcomment(ProductcommentDto comment);
	}
}
