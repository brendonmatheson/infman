﻿/*
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
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.IO;

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
        // HostSpecInsert
        //

        protected void HostSpecInsert_ValidRequest_Succeeds(
            InfManRepository infManRepository)
        {
            if (infManRepository == null) { throw new ArgumentNullException("infManRepository"); }

            // Setup
            HostSpecInsert request = HostSpecFactory.Insert("foo");

            // Execute
            HostSpecEntity result = infManRepository.HostSpecInsert(request);

            // Verify
            Assert.IsNotNull(result, "result");
        }

    }
}
