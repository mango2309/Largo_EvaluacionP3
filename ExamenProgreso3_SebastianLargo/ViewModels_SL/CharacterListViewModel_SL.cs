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
    public partial class CharacterListViewModel_SL : INotifyPropertyChanged
    {
        private readonly SL_NarutoAPIService _apiService;
        private readonly DatabaseService_SL _databaseService;
        private bool _isLoading;

        public ObservableCollection<NarutoCharacter_SL> Characters { get; } = new();
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

        public ICommand LoadCharactersCommand { get; }
        public ICommand SaveCharacterCommand { get; }

        public CharacterListViewModel_SL(SL_NarutoAPIService apiService, DatabaseService_SL databaseService)
        {
            _apiService = apiService;
            _databaseService = databaseService;
            LoadCharactersCommand = new Command(async () => await LoadCharactersAsync());
            SaveCharacterCommand = new Command<NarutoCharacter_SL>(async (character) => await SaveCharacterAsync(character));
        }

        private async Task LoadCharactersAsync()
        {
            if (IsLoading) return;

            try
            {
                IsLoading = true;
                var characters = await _apiService.GetCharactersAsync();
                Characters.Clear();
                foreach (var character in characters)
                {
                    Characters.Add(character);
                }
            }
            finally
            {
                IsLoading = false;
            }
        }

        private async Task SaveCharacterAsync(NarutoCharacter_SL character)
        {
            await _databaseService.SaveCharacterAsync(character);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
