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

namespace cc.bren.infman.impl
{
    using cc.bren.infman.framework.xmlrepository;
    using cc.bren.infman.infrastructure;
    using cc.bren.infman.infrastructure.impl;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Xml.Linq;

    public class XmlInfrastructureRepository : InfrastructureRepository
    {
        private DirectoryInfo _storageRoot;
        private XmlRepositoryConnection _hostInstanceConn;

        public XmlInfrastructureRepository(DirectoryInfo storageRoot)
        {
            if (storageRoot == null) { throw new ArgumentNullException("storageRoot"); }

            _storageRoot = storageRoot;
            _hostInstanceConn = new XmlRepositoryConnection(
                storageRoot,
                "host_instance");
        }

        //
        // Infrastructure
        //

        public IList<InfrastructureEntity> InfrastructureList()
        {
            DirectoryInfo infrastructuresDir = this.InfrastructuresDir();
            DirectoryInfo[] infrastructureDirs = infrastructuresDir.GetDirectories();

            IList<InfrastructureEntity> result = new List<InfrastructureEntity>();

            foreach (DirectoryInfo infrastructureDir in infrastructureDirs)
            {
                result.Add(this.InfrastructureEntityLoad(infrastructureDir));
            }

            return result;
        }

        private InfrastructureEntity InfrastructureEntityLoad(
            DirectoryInfo infrastructureDir)
        {
            if (infrastructureDir == null) { throw new ArgumentNullException("infrastructureDir"); }

            FileInfo infrastructureFile = this.InfrastructureFile(infrastructureDir);

            XElement xe = XElement.Load(infrastructureFile.FullName);

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

        public InfrastructureEntity InfrastructureInsert(InfrastructureInsert request)
        {
            if (request == null) { throw new ArgumentNullException("request"); }

            Guid infrastructureId = Guid.NewGuid();

            XElement infrastructureXe = new XElement(
                "infrastructure",
                new XAttribute("infrastructure_id", infrastructureId.ToString()),
                new XAttribute("type", request.InfrastructureType.InfrastructureTypeCode),
                new XAttribute("name", request.Name));

            request.InfrastructureType.Apply(
                vmwareEsxi: () =>
                {
                    VmwareEsxiInsert requestT = (VmwareEsxiInsert)request;

                    infrastructureXe.Add(new XAttribute("ip_address", requestT.IpAddress));
                });

            DirectoryInfo infrastructureDir = this.InfrastructureDir(
                infrastructureId,
                request.Name,
                true);

            FileInfo infrastructureFile = this.InfrastructureFile(infrastructureDir);

            infrastructureXe.Save(infrastructureFile.FullName);

            InfrastructureEntity result = request.InfrastructureType.Apply(
                vmwareEsxi: () =>
                {
                    VmwareEsxiInsert requestT = (VmwareEsxiInsert)request;

                    return VmwareEsxiFactory.Entity(
                        infrastructureId,
                        request.Name,
                        requestT.IpAddress);
                });

            return result;
        }

        private DirectoryInfo InfrastructuresDir()
        {
            return new DirectoryInfo(Path.Combine(
                _storageRoot.FullName,
                "infrastructure"));
        }

        private DirectoryInfo InfrastructureDir(
            Guid infrastructureId,
            string name,
            bool autoCreate)
        {
            if (name == null) { throw new ArgumentNullException("name"); }

            name = this.FilenameSafe(name);

            DirectoryInfo result = new DirectoryInfo(Path.Combine(
                this.InfrastructuresDir().FullName,
                name + "_" + infrastructureId.ToString().Substring(0, 8)));

            if (autoCreate && !result.Exists)
            {
                result.Create();
                result.Refresh();
            }

            return result;
        }

        private FileInfo InfrastructureFile(
            DirectoryInfo infrastructureDir)
        {
            if (infrastructureDir == null) { throw new ArgumentNullException("infrastructureDir"); }

            return infrastructureDir.File("infrastructure.xml");
        }

        //
        // HostInstance
        //

        public HostInstanceEntity HostInstanceSingle(HostInstanceFilter filter)
        {
            if (filter == null) { throw new ArgumentNullException("filter"); }

            IList<HostInstanceEntity> list = this.HostInstanceList(filter);

            return list.Single();
        }

        public IList<HostInstanceEntity> HostInstanceList(HostInstanceFilter filter)
        {
            if (filter == null) { throw new ArgumentNullException("filter"); }

            return XR.List(
                _hostInstanceConn,
                filter,
                new HostInstanceXmlMapping());
        }

        public HostInstanceEntity HostInstanceInsert(HostInstanceInsert request)
        {
            if (request == null) { throw new ArgumentNullException("request"); }

            return XR.Insert(
                _hostInstanceConn,
                new HostInstanceXmlMapping(),
                request);
        }

        //
        // Helper
        //

        private string FilenameSafe(string value)
        {
            if (value == null) { throw new ArgumentNullException("value"); }

            value = value.Replace(" ", "_");

            return value;
        }
    }
}
