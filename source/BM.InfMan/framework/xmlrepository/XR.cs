/*
    Infrastructure Manager
	http://bren.cc/infman

	Copyright (c) 2016, Brendon Matheson

    This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public
    License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any
    later version.

    This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied
    warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General Public License for more
    details.

    You should have received a copy of the GNU General Public License along with this program; if not, write to the
    Free Software Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.
*/

namespace cc.bren.infman.framework.xmlrepository
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Xml.Linq;

    public static class XR
    {
        public static IList<TEntity> List<TEntity, TFilter>(
            XmlRepositoryConnection conn,
            XmlQueryMapping<TEntity> mapping,
            TFilter filter) where TFilter : Filter<TEntity>
        {
            if (conn == null) { throw new ArgumentNullException("conn"); }
            if (mapping == null) { throw new ArgumentNullException("mapping"); }
            if (filter == null) { throw new ArgumentNullException("filter"); }

            IList<TEntity> result = new List<TEntity>();

            DirectoryInfo[] entityDirs = XR.CollectionDir(conn).GetDirectories();

            foreach (DirectoryInfo entityDir in entityDirs)
            {
                TEntity entity = XR.LoadEntity(
                    conn,
                    mapping,
                    entityDir);

                if (filter.Matches(entity))
                {
                    result.Add(entity);
                }
            }

            return result;
        }

        public static TEntity Insert<TEntity, TInsert>(
            XmlRepositoryConnection conn,
            XmlInsertMapping<TEntity, TInsert> mapping,
            TInsert request)
        {
            if (conn == null) { throw new ArgumentNullException("conn"); }
            if (mapping == null) { throw new ArgumentNullException("mapping"); }
            if (request == null) { throw new ArgumentNullException("request"); }

            Guid id = Guid.NewGuid();

            TEntity entity = mapping.BuildNew(
                id,
                request);

            XElement xe = mapping.Ser(entity);

            DirectoryInfo entityDir = XR.EntityDir(conn, mapping, entity);
            FileInfo entityFile = XR.EntityFile(conn, entityDir);

            xe.Save(entityFile.FullName);

            return entity;
        }

        public static TEntity LoadEntity<TEntity>(
            XmlRepositoryConnection conn,
            XmlQueryMapping<TEntity> mapping,
            DirectoryInfo entityDir)
        {
            if (conn == null) { throw new ArgumentNullException("conn"); }
            if (mapping == null) { throw new ArgumentNullException("mapping"); }
            if (entityDir == null) { throw new ArgumentNullException("entityDir"); }

            FileInfo entityFile = XR.EntityFile(
                conn,
                entityDir);

            XElement xe = XElement.Load(entityFile.FullName);

            TEntity entity = mapping.Deser(xe);

            return entity;
        }

        private static DirectoryInfo CollectionDir(
            XmlRepositoryConnection conn)
        {
            if (conn == null) { throw new ArgumentNullException("conn"); }

            return conn.StorageRoot.Dir(conn.Name);
        }

        private static DirectoryInfo EntityDir<TEntity>(
            XmlRepositoryConnection conn,
            XmlLoadMapping<TEntity> mapping,
            TEntity entity)
        {
            if (conn == null) { throw new ArgumentNullException("conn"); }
            if (mapping == null) { throw new ArgumentNullException("mapping"); }
            if (entity == null) { throw new ArgumentNullException("entity"); }

            string name = mapping.NameMapper(entity);

            DirectoryInfo result = XR
                .CollectionDir(conn)
                .Dir(name);

            if (!result.Exists)
            {
                result.Create();
                result.Refresh();
            }

            return result;
        }

        private static FileInfo EntityFile(
            XmlRepositoryConnection conn,

            DirectoryInfo entityDir)
        {
            if (conn == null) { throw new ArgumentNullException("conn"); }
            if (entityDir == null) { throw new ArgumentNullException("entityDir"); }

            return entityDir.File(conn.Name + ".xml");
        }
    }
}
