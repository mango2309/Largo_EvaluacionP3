using ExamenProgreso3_SebastianLargo.Models_SL;
using ExamenProgreso3_SebastianLargo.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ExamenProgreso3_SebastianLargo.ViewModels_SL
{
    public class SavedCharactersViewModel_SL : INotifyPropertyChanged
    {
        private readonly DatabaseService_SL _databaseService;
        private bool _isLoading;

        public ObservableCollection<NarutoCharacter_SL> SavedCharacters { get; } = new();

        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                if (_isLoading != value)
                {
                    _isLoading = value;
                    OnPropertyChanged(nameof(IsLoading));
                }
            }
        }

        public ICommand LoadSavedCharactersCommand { get; }

        public SavedCharactersViewModel_SL(DatabaseService_SL databaseService)
        {
            _databaseService = databaseService;
            LoadSavedCharactersCommand = new Command(async () => await LoadSavedCharactersAsync());
        }

        private async Task LoadSavedCharactersAsync()
        {
            if (IsLoading) return;

            try
            {
                IsLoading = true;
                var characters = await _databaseService.GetCharactersAsync();
                SavedCharacters.Clear();
                foreach (var character in characters)
                {
                    SavedCharacters.Add(character);
                }
            }
            finally
            {
                IsLoading = false;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
