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
        public string MapName(InfrastructureEntity entity)
        {
            return entity.Name + "_" + entity.InfrastructureId.ToString().Substring(0, 8);
        }

        public InfrastructureEntity BuildNew(Guid id, InfrastructureInsert insert)
        {
            return insert.InfrastructureType.Apply(
                vmwareEsxi: () => VmwareEsxiFactory.Entity(
                    id,
                    insert.Name,
                    ((VmwareEsxiInsert)insert).IpAddress));
        }

        public XElement Ser(InfrastructureEntity entity)
        {
            XElement xe = new XElement(
                "infrastructure",
                new XAttribute("infrastructure_id", entity.InfrastructureId.ToString()),
                new XAttribute("type", entity.InfrastructureType.InfrastructureTypeCode),
                new XAttribute("name", entity.Name));

            entity.InfrastructureType.Apply(
                vmwareEsxi: () =>
                {
                    VmwareEsxiEntity eT = (VmwareEsxiEntity)entity;

                    xe.Add(new XAttribute("ip_address", eT.IpAddress));
                });

            return xe;
        }

        public InfrastructureEntity Deser(XElement xe)
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
        }
    }
}
