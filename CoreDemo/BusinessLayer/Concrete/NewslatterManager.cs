using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
	public class NewslatterManager : INewslatterService
	{

		INewslatterDal _newslatterDal;

		public NewslatterManager(INewslatterDal newslatterDal)
		{
			_newslatterDal = newslatterDal;
		}

		public void AddNewsletter(Newslatter newslatter)
		{
			_newslatterDal.Insert(newslatter);
		}
	}
}
