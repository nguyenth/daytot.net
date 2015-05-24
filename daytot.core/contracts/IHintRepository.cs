using System;
using System.Collections.Generic;
using System.Linq;

using daytot.core.models;

namespace daytot.core.contracts
{
    public interface IHintRepository: IBaseRepository<Hint>
    {
        /// <summary>
        /// Tạo mới hoặc cập nhật một gợi ý
        /// </summary>
        /// <param name="in_hint"></param>
        void AddOrEditHint(Hint in_hint);
    }
}
