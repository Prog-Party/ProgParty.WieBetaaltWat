using ProgParty.WieBetaaltWat.Api.Result;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using Windows.UI.Xaml;
using System;
using System.Collections.Generic;

namespace ProgParty.WieBetaaltWat
{
    public class WieBetaaltWatDataContext : INotifyPropertyChanged
    {
        public ObservableCollection<OverviewResult> Lijsten { get; set; } = new ObservableCollection<OverviewResult>();
        public ObservableCollection<LijstResult> LijstResults { get; set; } = new ObservableCollection<LijstResult>();
        public ObservableCollection<InvoerItemPerson> LijstPersons { get; set; } = new ObservableCollection<InvoerItemPerson>();
        
        public int ProjectId { get; set; } = -1;

        private bool _galleryItemsLoading = false;
        public bool GalleryItemsLoading
        {
            get { return _galleryItemsLoading; }
            set
            {
                _galleryItemsLoading = value;

                OnPropertyChanged("GalleryItemsLoadingVisibility");
            }
        }
        public Visibility GalleryItemsLoadingVisibility => GalleryItemsLoading ? Visibility.Visible : Visibility.Collapsed;

        public WieBetaaltWatDataContext()
        {
        }

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler PropertyChanged;

        internal void SetResult(List<OverviewResult> result)
        {
            Lijsten.Clear();
            foreach (var singleResult in result)
                Lijsten.Add(singleResult);
            GalleryItemsLoading = false;
        }

        internal void SetLijstResult(List<LijstResult> result)
        {
            LijstResults.Clear();
            foreach (var singleResult in result)
                LijstResults.Add(singleResult);
            GalleryItemsLoading = false;
        }

        internal void SetLijstPersons(List<InvoerItemPerson> persons)
        {
            LijstPersons.Clear();
            foreach (var singleResult in persons)
                LijstPersons.Add(singleResult);
            GalleryItemsLoading = false;
        }
    }
}