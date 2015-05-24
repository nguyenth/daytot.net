using System;
using System.Collections.Generic;
using System.Linq;

using daytot.core.models;

namespace daytot.core.contracts
{
    public interface IMediaServerRepository: IBaseRepository<MediaServer>
    {
        /// <summary>
        /// Lấy một server lưu trữ
        /// </summary>
        /// <returns></returns>
        MediaServer GetServer();
    }
}
