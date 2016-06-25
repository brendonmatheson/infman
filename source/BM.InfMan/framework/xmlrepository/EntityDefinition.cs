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

using System;
namespace cc.bren.infman.framework.xmlrepository
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Xml.Linq;

    public class EntityDefinition<TEntity, TInsert, TFilter> where TFilter : cc.bren.infman.framework.Filter<TEntity>
    {
        private DirectoryInfo _storageRoot;
        Func<TEntity, string> _nameMapper;
        private Func<Guid, TInsert, TEntity> _buildNew;
        private Func<TEntity, XElement> _ser;
        private Func<XElement, TEntity> _deser;

        public EntityDefinition(
            string name,
            DirectoryInfo storageRoot,
            Func<TEntity, string> nameMapper,
            Func<Guid, TInsert, TEntity> buildNew,
            Func<TEntity, XElement> ser,
            Func<XElement, TEntity> deser)
        {
            if (name == null) { throw new ArgumentNullException("name"); }
            if (storageRoot == null) { throw new ArgumentNullException("storageRoot"); }
            if (nameMapper == null) { throw new ArgumentNullException("nameMapper"); }
            if (buildNew == null) { throw new ArgumentNullException("buildNew"); }
            if (ser == null) { throw new ArgumentNullException("ser"); }
            if (deser == null) { throw new ArgumentNullException("deser"); }

            this.Name = name;
            _storageRoot = storageRoot;
            _nameMapper = nameMapper;
            _buildNew = buildNew;
            _ser = ser;
            _deser = deser;
        }

        public string Name { get; private set; }

        public IList<TEntity> List(TFilter filter)
        {
            if (filter == null) { throw new ArgumentNullException("filter"); }

            IList<TEntity> result = new List<TEntity>();

            DirectoryInfo[] entityDirs = this.CollectionDir().GetDirectories();

            foreach(DirectoryInfo entityDir in entityDirs)
            {
                TEntity entity = this.LoadEntity(entityDir);

                if (filter.Matches(entity))
                {
                    result.Add(entity);
                }
            }

            return result;
        }

        public TEntity Insert(TInsert insert)
        {
            if (insert == null) { throw new ArgumentNullException("insert"); }

            Guid id = Guid.NewGuid();

            TEntity entity = _buildNew(
                id,
                insert);

            XElement xe = _ser(entity);

            DirectoryInfo entityDir = this.EntityDir(entity);
            FileInfo entityFile = this.EntityFile(entityDir);

            xe.Save(entityFile.FullName);

            return entity;
        }

        private TEntity LoadEntity(DirectoryInfo entityDir)
        {
            if (entityDir == null) { throw new ArgumentNullException("entityDir"); }

            FileInfo entityFile = this.EntityFile(entityDir);

            XElement xe = XElement.Load(entityFile.FullName);

            TEntity entity = _deser(xe);

            return entity;
        }

        private DirectoryInfo CollectionDir()
        {
            return _storageRoot.Dir(this.Name);
        }

        private DirectoryInfo EntityDir(TEntity entity)
        {
            if (entity == null) { throw new ArgumentNullException("entity"); }

            string name = _nameMapper(entity);

            DirectoryInfo result = this
                .CollectionDir()
                .Dir(name);

            if (!result.Exists)
            {
                result.Create();
                result.Refresh();
            }

            return result;
        }

        private FileInfo EntityFile(DirectoryInfo entityDir)
        {
            if (entityDir == null) { throw new ArgumentNullException("entityDir"); }

            return entityDir.File(this.Name + ".xml");
        }
    }
}
