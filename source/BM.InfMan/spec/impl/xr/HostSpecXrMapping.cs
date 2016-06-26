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

namespace cc.bren.infman.spec.impl.xr
{
    using cc.bren.infman.framework.xr;
    using System;
    using System.Xml.Linq;

    public class HostSpecXrMapping : XrMapping<HostSpecEntity, HostSpecInsert>
    {
        public string MapName(HostSpecEntity entity)
        {
            if (entity == null) { throw new ArgumentNullException("entity"); }

            return entity.Name + "_" + entity.HostSpecId.ToString().Substring(0, 8);
        }

        public HostSpecEntity BuildNew(
            Guid id,
            HostSpecInsert insert)
        {
            if (insert == null) { throw new ArgumentNullException("insert"); }

            return HostSpecFactory.Entity(
                id,
                insert.Name,
                insert.RamBytes);
        }

        public XElement Ser(HostSpecEntity entity)
        {
            if (entity == null) { throw new ArgumentNullException("entity"); }

            return new XElement(
                "host_spec",
                new XAttribute("host_spec_id", entity.HostSpecId.ToString()),
                new XAttribute("name", entity.Name),
                new XAttribute("ram_bytes", entity.RamBytes.ToString()));
        }

        public HostSpecEntity Deser(XElement xe)
        {
            if (xe == null) { throw new ArgumentNullException("xe"); }

            Guid hostSpecId = Guid.Parse(xe.Attribute("host_spec_id").Value);
            string name = xe.Attribute("name").Value;
            long ramBytes = long.Parse(xe.Attribute("ram_bytes").Value);

            return HostSpecFactory.Entity(
                hostSpecId,
                name,
                ramBytes);
        }
    }
}
