using InsuranceAPI.Models;
using InsuranceAPI.Repositories;

namespace InsuranceAPI.Services {
    public interface IProducerService {
        public Task<List<Producer>> getAll();
    }

    public class ProducerService : IProducerService {
        private readonly IProducerRepository _producerRepo;

        public ProducerService(IProducerRepository repo){
            _producerRepo = repo;
        }

        public async Task<List<Producer>> getAll() {
            return await Task.Run(() => {
                return _producerRepo.getAll();
            });
        }
    }
}
