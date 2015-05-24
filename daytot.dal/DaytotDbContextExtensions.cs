using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace daytot.dal
{
    public static class DaytotDbContextExtensions
    {
        /// <summary>
        /// Lấy danh sách giá trị của khóa chính của một Entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static object[] GetEntityKey<T>(this DbContext context, T entity)
        {

            var oc = ((IObjectContextAdapter)context).ObjectContext;
            var keys = oc.MetadataWorkspace.GetEntityContainer(oc.DefaultContainerName, System.Data.Entity.Core.Metadata.Edm.DataSpace.CSpace)
                         .BaseEntitySets
                         .First(meta => meta.ElementType.Name == entity.GetType().Name)
                         .ElementType
                         .KeyMembers
                         .Select(k => k.Name)
                         .ToList();


            var propertys =
                entity.GetType()
                     .GetProperties().Where(prop => keys.Contains(prop.Name));
            return propertys.ToList().Select(property => property.GetValue(entity, null)).ToArray();
        }
    }
}
