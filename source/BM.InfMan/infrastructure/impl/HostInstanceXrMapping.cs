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

namespace cc.bren.infman.infrastructure.impl
{
    using cc.bren.infman.framework.xmlrepository;
    using System;
    using System.Xml.Linq;

    public class HostInstanceXrMapping : XrMapping<HostInstanceEntity, HostInstanceInsert>
    {
        public string MapName(HostInstanceEntity entity)
        {
            return entity.Name + "_" + entity.HostInstanceId.ToString().Substring(0, 8);
        }

        public HostInstanceEntity BuildNew(Guid id, HostInstanceInsert insert) 
        {
            return HostInstanceFactory.Entity(
                id,
                insert.Name,
                insert.HostSpecId,
                insert.InfrastructureId);
        }

        public XElement Ser(HostInstanceEntity entity)
        {
            return new XElement(
                "host_instance",
                new XAttribute("host_instance_id", entity.HostInstanceId.ToString()),
                new XAttribute("name", entity.Name),
                new XAttribute("host_spec_id", entity.HostSpecId.ToString()),
                new XAttribute("infrastructure_id", entity.InfrastructureId));
        }

        public HostInstanceEntity Deser(XElement xe)
        {
            Guid hostInstanceId = Guid.Parse(xe.Attribute("host_instance_id").Value);
            string name = xe.Attribute("name").Value;
            Guid hostSpecId = Guid.Parse(xe.Attribute("host_spec_id").Value);
            Guid infrastructureId = Guid.Parse(xe.Attribute("infrastructure_id").Value);

            return HostInstanceFactory.Entity(
                hostInstanceId,
                name,
                hostSpecId,
                infrastructureId);
        }
    }
}
