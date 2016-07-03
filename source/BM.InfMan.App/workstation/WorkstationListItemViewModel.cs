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

namespace cc.bren.infman.workstation
{
    using System;
    using System.ComponentModel;

    public class WorkstationListItemViewModel : INotifyPropertyChanged
    {
        #region PropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(
            string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion

        public WorkstationListItemViewModel(
            Guid workstationId,
            string name,
            string keyPath)
        {
            if (name == null) { throw new ArgumentNullException("name"); }
            if (keyPath == null) { throw new ArgumentNullException("keyPath"); }

            this.WorkstationId = workstationId;
            this.Name = name;
            this.KeyPath = keyPath;
        }

        public Guid WorkstationId { get; private set; }

        #region Name

        private string _name;

        public string Name
        {
            get
            {
                return _name;
            }
            private set
            {
                _name = value;
                this.OnPropertyChanged(nameof(this.Name));
            }
        }

        #endregion

        #region KeyPath

        private string _keyPath;

        public string KeyPath
        {
            get
            {
                return _keyPath;
            }
            private set
            {
                _keyPath = value;
                this.OnPropertyChanged(nameof(this.KeyPath));
            }
        }

        #endregion

        public void RefreshFrom(WorkstationEntity entity)
        {
            if (entity == null) { throw new ArgumentNullException("entity"); }

            this.Name = entity.Name;
            this.KeyPath = entity.KeyPath;
        }
    }
}
