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

    public class HostInstanceXmlMapping : XmlMapping<HostInstanceEntity, HostInstanceInsert>
    {
        private Func<HostInstanceEntity, string> _nameMapper;
        private Func<Guid, HostInstanceInsert, HostInstanceEntity> _buildNew;
        private Func<HostInstanceEntity, XElement> _ser;
        private Func<XElement, HostInstanceEntity> _deser;

        public HostInstanceXmlMapping()
        {
            _nameMapper = e => e.Name + "_" + e.HostInstanceId.ToString().Substring(0, 8);
            _buildNew = (id, insert) => HostInstanceFactory.Entity(
                id,
                insert.Name,
                insert.HostSpecId,
                insert.InfrastructureId);
            _ser = e => new XElement(
                "host_instance",
                new XAttribute("host_instance_id", e.HostInstanceId.ToString()),
                new XAttribute("name", e.Name),
                new XAttribute("host_spec_id", e.HostSpecId.ToString()),
                new XAttribute("infrastructure_id", e.InfrastructureId));
            _deser = xe =>
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
            };
        }

        public Func<HostInstanceEntity, string> NameMapper
        {
            get
            {
                return _nameMapper;
            }
        }

        public Func<Guid, HostInstanceInsert, HostInstanceEntity> BuildNew
        {
            get
            {
                return _buildNew;
            }
        }

        public Func<HostInstanceEntity, XElement> Ser
        {
            get
            {
                return _ser;
            }
        }

        public Func<XElement, HostInstanceEntity> Deser
        {
            get
            {
                return _deser;
            }
        }
    }
}
