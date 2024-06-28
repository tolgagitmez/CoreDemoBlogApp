using CoreDemo.Models;
using Microsoft.AspNetCore.Mvc;

namespace CoreDemo.ViewComponents
{
	public class CommentList:ViewComponent
	{
		public IViewComponentResult Invoke()
		{
			var commentvalues = new List<UserComment>
			{
				new UserComment
				{
					ID = 1,
					Username = "Test"
				},
				new UserComment
				{
					ID= 2,
					Username= "Test2"
				},
				new UserComment
				{
					 ID=3,
					 Username="Test3"
				}
			};
			return View(commentvalues);
		}
	}
}
