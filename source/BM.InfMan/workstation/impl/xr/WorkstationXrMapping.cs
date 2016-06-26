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

namespace cc.bren.infman.workstation.impl.xr
{
    using System;
    using System.Xml.Linq;
    using cc.bren.infman.framework.xr;

    public class WorkstationXrMapping : XrMapping<WorkstationEntity, WorkstationInsert>
    {
        public string MapName(WorkstationEntity entity)
        {
            if (entity == null) { throw new ArgumentNullException("entity"); }

            return entity.Name + "_" + entity.WorkstationId.ToString().Substring(0, 8);
        }

        public WorkstationEntity BuildNew(
            Guid id,
            WorkstationInsert insert)
        {
            if (insert == null) { throw new ArgumentNullException("insert"); }

            return WorkstationFactory.Entity(
                id,
                insert.Name,
                insert.KeyPath);
        }

        public XElement Ser(WorkstationEntity entity)
        {
            if (entity == null) { throw new ArgumentNullException("entity"); }

            return new XElement(
                "workstation",
                new XAttribute("workstation_id", entity.WorkstationId.ToString()),
                new XAttribute("name", entity.Name),
                new XAttribute("key_path", entity.KeyPath));
        }

        public WorkstationEntity Deser(XElement xe)
        {
            if (xe == null) { throw new ArgumentNullException("xe"); }

            Guid workstationId = Guid.Parse(xe.Attribute("workstation_id").Value);
            string name = xe.Attribute("name").Value;
            string keyPath = xe.Attribute("key_path").Value;

            return WorkstationFactory.Entity(
                workstationId,
                name,
                keyPath);
        }
    }
}
