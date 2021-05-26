namespace pogoda.Models
{
    public class Weather
    {
        public string? id_stacji { get; set; }
        public string? stacja { get; set; }
        public string? data_pomiaru { get; set; }
        public string? godzina_pomiaru { get; set; }
        public string? temperatura { get; set; }
        public string? predkosc_wiatru { get; set; }
        public string? wilgotnosc_wzgledna { get; set; }
        public string? suma_opadu { get; set; }
        public string? cisnienie { get; set; }
    }
}
