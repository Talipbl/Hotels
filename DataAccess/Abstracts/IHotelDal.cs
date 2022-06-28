using Core.Concrete.Utilities.Results.Abstracts;
using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstracts
{
    public interface IHotelDal
    {
        IResult Read();
        IResult WriteAsJson(string path, List<Hotel> hotels);
        IResult WriteAsXml(string path, List<Hotel> hotels);
    }
}
