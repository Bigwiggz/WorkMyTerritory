using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkMyTerritory.Models.ModelInterfaces
{
    public interface INewRegisterRepository<T1, T2> where T1 : class where T2 : class
    {
        void InsertAsync(T1 obj1, T2 obj2);
    }

}