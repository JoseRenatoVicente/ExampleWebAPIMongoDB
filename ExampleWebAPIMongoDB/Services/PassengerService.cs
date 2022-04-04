using ExampleWebAPIMongoDB.Models;
using ExampleWebAPIMongoDB.Utils;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExampleWebAPIMongoDB.Services
{
    public class PassengerService
    {
        private readonly ViaCepService _viaCepService;
        private readonly IMongoCollection<Passenger> _passengers;

        public PassengerService(ViaCepService viaCepService, IProjMongoDotnetDatabaseSettings settings)
        {
            _viaCepService = viaCepService;

            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _passengers = database.GetCollection<Passenger>(settings.PassengerCollectionName);
        }

        public async Task<IEnumerable<Passenger>> GetPassengersAsync() =>
            await _passengers.Find(passenger => true).ToListAsync();


        public async Task<Passenger> GetPassengerByIdAsync(string id) =>
            await _passengers.Find<Passenger>(passenger => passenger.Id == id).FirstOrDefaultAsync();

        public async Task<Passenger> AddAsync(Passenger passenger)
        {
            var address = await _viaCepService.ConsultarCEP(passenger.Address.CEP);
            if (address is not null) passenger.Address = address;

            _passengers.InsertOne(passenger);

            return passenger;
        }

        public async Task<Passenger> UpdateAsync(string id, Passenger passenger)
        {
            await _passengers.ReplaceOneAsync(passenger => passenger.Id == id, passenger);
            return passenger;
        }


        public async Task RemoveAsync(Passenger passengerIn) =>
            await _passengers.DeleteOneAsync(passenger => passenger.Id == passengerIn.Id);

        public async Task RemoveAsync(string id) =>
            await _passengers.DeleteOneAsync(passenger => passenger.Id == id);
    }
}
