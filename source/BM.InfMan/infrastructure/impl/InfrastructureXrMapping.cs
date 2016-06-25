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

using cc.bren.infman.framework.xmlrepository;
using System;
using System.Xml.Linq;

namespace cc.bren.infman.infrastructure.impl
{
    public class InfrastructureXrMapping : XrMapping<InfrastructureEntity, InfrastructureInsert>
    {
        private Func<InfrastructureEntity, string> _nameMapper;
        private Func<Guid, InfrastructureInsert, InfrastructureEntity> _buildNew;
        private Func<InfrastructureEntity, XElement> _ser;
        private Func<XElement, InfrastructureEntity> _deser;

        public InfrastructureXrMapping()
        {
            _nameMapper = e => e.Name + "_" + e.InfrastructureId.ToString().Substring(0, 8);
            _buildNew = (id, insert) =>
            {
                return insert.InfrastructureType.Apply(
                    vmwareEsxi: () => VmwareEsxiFactory.Entity(
                        id,
                        insert.Name,
                        ((VmwareEsxiInsert)insert).IpAddress));
            };
            _ser = e =>
            {
                XElement xe = new XElement(
                    "infrastructure",
                    new XAttribute("infrastructure_id", e.InfrastructureId.ToString()),
                    new XAttribute("type", e.InfrastructureType.InfrastructureTypeCode),
                    new XAttribute("name", e.Name));

                e.InfrastructureType.Apply(
                    vmwareEsxi: () =>
                    {
                        VmwareEsxiEntity eT = (VmwareEsxiEntity)e;

                        xe.Add(new XAttribute("ip_address", eT.IpAddress));
                    });

                return xe;
            };
            _deser = xe =>
            {
                Guid infrastructureId = Guid.Parse(xe.Attribute("infrastructure_id").Value);
                InfrastructureType infrastructureType = InfrastructureType.ForCode(xe.Attribute("type").Value);
                string name = xe.Attribute("name").Value;

                InfrastructureEntity entity = infrastructureType.Apply(
                    vmwareEsxi: () =>
                    {
                        string ipAddress = xe.Attribute("ip_address").Value;

                        return VmwareEsxiFactory.Entity(
                            infrastructureId,
                            name,
                            ipAddress);
                    });

                return entity;
            };
        }

        public Func<InfrastructureEntity, string> NameMapper
        {
            get
            {
                return _nameMapper;
            }
        }

        public Func<Guid, InfrastructureInsert, InfrastructureEntity> BuildNew
        {
            get
            {
                return _buildNew;
            }
        }

        public Func<InfrastructureEntity, XElement> Ser
        {
            get
            {
                return _ser;
            }
        }

        public Func<XElement, InfrastructureEntity> Deser
        {
            get
            {
                return _deser;
            }
        }

    }
}
