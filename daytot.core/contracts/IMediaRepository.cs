using System;
using System.Collections.Generic;
using System.Linq;

using daytot.core.models;

namespace daytot.core.contracts
{
    public interface IMediaRepository: IBaseRepository<Media>
    {
        /// <summary>
        /// Xóa các Media theo một ReferId & ReferTypeId
        /// </summary>
        /// <param name="referId">Mã tham chiếu</param>
        /// <param name="referTypeId">Loại mã tham chiếu</param>
        void Remove(int referId, int referTypeId);
    }
}
