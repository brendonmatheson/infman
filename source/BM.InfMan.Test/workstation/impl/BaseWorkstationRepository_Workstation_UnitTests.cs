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

namespace cc.bren.infman.workstation.impl
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Collections.Generic;

    public abstract class BaseWorkstationRepository_Workstation_UnitTests
    {

        //
        // WorkstationList
        //

        protected void WorkstationList_ValidRequest_Succeeds(
            WorkstationRepository workstationRepository)
        {
            if (workstationRepository == null) { throw new ArgumentNullException("workstationRepository"); }

            // Setup
            WorkstationEntity w0 = workstationRepository.WorkstationInsert(WorkstationFactory.Insert("fooName", "fooKeyPath"));
            WorkstationEntity w1 = workstationRepository.WorkstationInsert(WorkstationFactory.Insert("barName", "barKeyPath"));

            // Execute
            IList<WorkstationEntity> result = workstationRepository.WorkstationList(WorkstationFilter.All());

            // Verify
            Assert.IsNotNull(result, "result");
            Asserts.AssertWorkstation("barName", "barKeyPath", result[0], "result[0]");
            Asserts.AssertWorkstation("fooName", "fooKeyPath", result[1], "result[1]");
        }

        //
        // WorkstationUpdate
        //

        protected void WorkstationUpdate_ValidRequest_Success(
            WorkstationRepository workstationRepository)
        {
            if (workstationRepository == null) { throw new ArgumentNullException("workstationRepository"); }

            // Setup
            WorkstationEntity w0 = workstationRepository.WorkstationInsert(WorkstationFactory.Insert(
                "fooName",
                "fooKeyPath"));

            // Execute
            WorkstationEntity result = workstationRepository.WorkstationUpdate(WorkstationFactory.Update(
                w0.WorkstationId,
                "updatedName",
                "updatedKeyPath"));

            // Verify
            Assert.IsNotNull(result, "result");
            Asserts.AssertWorkstation(
                w0.WorkstationId, "updatedName", "updatedKeyPath",
                result, "result");
        }
    }
}
