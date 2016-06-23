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
    using cc.bren.infman.spec;
    using infrastructure;
    using infrastructure.impl;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Collections.Generic;

    public abstract class BaseInfManRepository_HostSpec_UnitTests
    {

        //
        // HostSpecSingle
        //

        protected void HostSpecSingle_ValidRequest_Succeeds()
        {

        }

        protected void HostSpecSingle_NullFilter_Throws()
        {

        }

        //
        // HostSpecList
        //

        protected void HostSpecList_FilterAll_Succeeds(
            InfManRepository infManRepository)
        {
            if (infManRepository == null) { throw new ArgumentNullException("infManRepository"); }

            // Setup
            HostSpecInsert hs1 = HostSpecFactory.Insert("foo", Constants.Size1GB);
            HostSpecEntity e1 = infManRepository.HostSpecInsert(hs1);
            HostSpecInsert hs2 = HostSpecFactory.Insert("bar", 500 * Constants.Size1MB);
            HostSpecEntity e2 = infManRepository.HostSpecInsert(hs2);

            // Execute
            IList<HostSpecEntity> result = infManRepository.HostSpecList(HostSpecFilter.All());

            // Verify
            Assert.IsNotNull(result, "result");
            Assert.AreEqual(2, result.Count, "result.Count");

            Asserts.AssertHostSpecEntity(
                e2.HostSpecId, e2.Name, e2.RamBytes,
                result[0], "result[0]");
            Asserts.AssertHostSpecEntity(
                e1.HostSpecId, e1.Name, e1.RamBytes,
                result[1], "result[1]");
        }

        //
        // HostSpecInsert
        //

        protected void HostSpecInsert_ValidRequest_Succeeds(
            InfManRepository infManRepository)
        {
            if (infManRepository == null) { throw new ArgumentNullException("infManRepository"); }

            // Setup
            HostSpecInsert request = HostSpecFactory.Insert("foo", 4 * Constants.Size1GB);

            // Execute
            HostSpecEntity result = infManRepository.HostSpecInsert(request);

            // Verify
            Asserts.AssertHostSpecEntity(
                "foo", 4 * Constants.Size1GB,
                result, "result");
        }

        //
        // InfrastructureInsert
        //

        protected void InfrastructureList_FilterAll_Succeeds(
            InfManRepository infManRepository)
        {
            if (infManRepository == null) { throw new ArgumentNullException("infManRepository"); }

            // Setup
            InfrastructureEntity o1 = infManRepository.InfrastructureInsert(VmwareEsxiFactory.Insert(
                "esxi01",
                "1.1.1.1"));

            InfrastructureEntity o2 = infManRepository.InfrastructureInsert(VmwareEsxiFactory.Insert(
                "esxi02",
                "2.2.2.2"));

            // Execute
            IList<InfrastructureEntity> result = infManRepository.InfrastructureList();

            // Verify
            Assert.IsNotNull(result, "result");
            Assert.AreEqual(2, result.Count, "result.count");

            Asserts.AssertVmwareEsxiEntity("esxi01", "1.1.1.1", result[0], "result[0]");
            Asserts.AssertVmwareEsxiEntity("esxi02", "2.2.2.2", result[1], "result[1]");
        }

        protected void InfrastructureInsert_ValidRequest_Succeeds(
            InfManRepository infManRepository)
        {
            if (infManRepository == null) { throw new ArgumentNullException("infManRepository"); }

            // Setup
            InfrastructureInsert request = VmwareEsxiFactory.Insert(
                "esxi01",
                "1.2.3.4");

            // Execute
            InfrastructureEntity inserted = infManRepository.InfrastructureInsert(request);

            // Verify
            Asserts.AssertVmwareEsxiEntity(
                "esxi01", "1.2.3.4",
                inserted, "inserted");
        }

    }
}
