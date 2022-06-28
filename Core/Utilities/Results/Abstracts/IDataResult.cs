using Core.Concrete.Utilities.Results.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Results.Abstracts
{
    public interface IDataResult<Type> : IResult
    {
        Type Data { get; }
    }
}
