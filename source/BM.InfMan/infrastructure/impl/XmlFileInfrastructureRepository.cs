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
    using cc.bren.infman.infrastructure;
    using infrastructure.impl;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Xml.Linq;

    public class XmlFileInfrastructureRepository : InfrastructureRepository
    {
        private DirectoryInfo _storageRoot;

        public XmlFileInfrastructureRepository(DirectoryInfo storageRoot)
        {
            if (storageRoot == null) { throw new ArgumentNullException("storageRoot"); }

            _storageRoot = storageRoot;
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

            IList<HostInstanceEntity> result = new List<HostInstanceEntity>();

            DirectoryInfo hostInstancesDir = this.HostInstancesDir();
            DirectoryInfo[] hostInstanceDirs = hostInstancesDir.GetDirectories();

            foreach (DirectoryInfo hostInstanceDir in hostInstanceDirs)
            {
                HostInstanceEntity entity = this.HostInstanceEntityLoad(hostInstanceDir);

                if (filter.Matches(entity))
                {
                    result.Add(entity);
                }
            }

            return result;
        }

        private HostInstanceEntity HostInstanceEntityLoad(
            DirectoryInfo hostInstanceDir)
        {
            if (hostInstanceDir == null) { throw new ArgumentNullException("hostInstanceDir"); }

            FileInfo file = this.HostInstanceFile(hostInstanceDir);

            XElement xe = XElement.Load(file.FullName);

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

        public HostInstanceEntity HostInstanceInsert(HostInstanceInsert request)
        {
            if (request == null) { throw new ArgumentNullException("request"); }

            Guid hostInstanceId = Guid.NewGuid();

            XElement xe = new XElement(
                "host_instance",
                new XAttribute("host_instance_id", hostInstanceId.ToString()),
                new XAttribute("name", request.Name),
                new XAttribute("host_spec_id", request.HostSpecId),
                new XAttribute("infrastructure_id", request.InfrastructureId.ToString()));

            DirectoryInfo hostInstanceDir = this.HostInstanceDir(
                hostInstanceId,
                request.Name,
                true);

            FileInfo hostInstanceFile = this.HostInstanceFile(hostInstanceDir);

            xe.Save(hostInstanceFile.FullName);

            return HostInstanceFactory.Entity(
                hostInstanceId,
                request.Name,
                request.HostSpecId,
                request.InfrastructureId);
        }

        private DirectoryInfo HostInstancesDir()
        {
            return new DirectoryInfo(Path.Combine(
                _storageRoot.FullName,
                "host_instance"));
        }

        private DirectoryInfo HostInstanceDir(
            Guid hostIntanceId,
            string name,
            bool autoCreate)
        {
            if (name == null) { throw new ArgumentNullException("name"); }

            name = this.FilenameSafe(name);

            DirectoryInfo result = new DirectoryInfo(Path.Combine(
                this.HostInstancesDir().FullName,
                name + "_" + hostIntanceId.ToString().Substring(0, 8)));

            if (autoCreate && !result.Exists)
            {
                result.Create();
                result.Refresh();
            }

            return result;
        }

        private FileInfo HostInstanceFile(
            DirectoryInfo hostInstanceDir)
        {
            if (hostInstanceDir == null) { throw new ArgumentNullException("hostInstanceDir"); }

            return hostInstanceDir.File("host_instance.xml");
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
