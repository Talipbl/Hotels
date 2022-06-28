using Core.Concrete.Utilities.Results.Abstracts;
using Core.Utilities.Results.Abstracts;
using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstracts
{
    public interface IHotelService
    {
        IDataResult<List<Hotel>> Read(string path);
        IResult WriteAsJson(string path, List<Hotel> hotels);
        IResult WriteAsXml(string path, List<Hotel> hotels);
    }
}
