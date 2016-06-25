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

namespace cc.bren.infman
{
    using infrastructure;
    using infrastructure.impl;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Collections.Generic;

    public abstract class BaseInfrastructureRepository_HostSpec_UnitTests
    {

        //
        // InfrastructureInsert
        //

        protected void InfrastructureList_FilterAll_Succeeds(
            InfrastructureRepository infrastructureRepository)
        {
            if (infrastructureRepository == null) { throw new ArgumentNullException("infrastructureRepository"); }

            // Setup
            InfrastructureEntity o1 = infrastructureRepository.InfrastructureInsert(VmwareEsxiFactory.Insert(
                "esxi01",
                "1.1.1.1"));

            InfrastructureEntity o2 = infrastructureRepository.InfrastructureInsert(VmwareEsxiFactory.Insert(
                "esxi02",
                "2.2.2.2"));

            // Execute
            IList<InfrastructureEntity> result = infrastructureRepository.InfrastructureList();

            // Verify
            Assert.IsNotNull(result, "result");
            Assert.AreEqual(2, result.Count, "result.count");

            Asserts.AssertVmwareEsxiEntity("esxi01", "1.1.1.1", result[0], "result[0]");
            Asserts.AssertVmwareEsxiEntity("esxi02", "2.2.2.2", result[1], "result[1]");
        }

        protected void InfrastructureInsert_ValidRequest_Succeeds(
            InfrastructureRepository infrastructureRepository)
        {
            if (infrastructureRepository == null) { throw new ArgumentNullException("infrastructureRepository"); }

            // Setup
            InfrastructureInsert request = VmwareEsxiFactory.Insert(
                "esxi01",
                "1.2.3.4");

            // Execute
            InfrastructureEntity inserted = infrastructureRepository.InfrastructureInsert(request);

            // Verify
            Asserts.AssertVmwareEsxiEntity(
                "esxi01", "1.2.3.4",
                inserted, "inserted");
        }

        //
        // HostInstanceSingle
        //

        protected void HostInstanceSingle_FilterByHostSpecId_Succeeds(
            InfrastructureRepository infrastructureRepository)
        {
            if (infrastructureRepository == null) { throw new ArgumentNullException("infrastructureRepository"); }

            // Setup
            HostInstanceEntity inserted = infrastructureRepository.HostInstanceInsert(HostInstanceFactory.Insert(
                "foo",
                Guid.NewGuid(),
                Guid.NewGuid()));

            // Execute
            HostInstanceEntity result = infrastructureRepository.HostInstanceSingle(HostInstanceFilter.ByHostSpecId(
                inserted.HostSpecId));

            // Verify
            Asserts.AssertHostInstanceEntity(
                inserted.HostInstanceId,
                inserted.Name,
                inserted.HostSpecId,
                inserted.InfrastructureId,
                result,
                "result");
        }

        //
        // HostInstanceList
        //

        protected void HostInstanceList_FilterByHostSpecId_Succeeds(
            InfrastructureRepository infrastructureRepository)
        {
            if (infrastructureRepository == null) { throw new ArgumentNullException("infrastructureRepository"); }

        }

        //
        // HostInstanceInsert
        //

        protected void HostInstanceInsert_ValueRequest_Succeeds(
            InfrastructureRepository infrastructureRepository)
        {
            if (infrastructureRepository == null) { throw new ArgumentNullException("infrastructureRepository"); }

            // Setup
            HostInstanceInsert request = HostInstanceFactory.Insert(
                "foo",
                Guid.NewGuid(),
                Guid.NewGuid());

            // Execute
            HostInstanceEntity result = infrastructureRepository.HostInstanceInsert(request);

            // Verify
            Asserts.AssertHostInstanceEntity(
                request.Name,
                request.HostSpecId,
                request.InfrastructureId,
                result,
                "result");
        }

    }
}
