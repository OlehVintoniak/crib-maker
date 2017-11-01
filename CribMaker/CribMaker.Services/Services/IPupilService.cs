using CribMaker.Core.Data.Entities;

namespace CribMaker.Services.Services
{
    public interface IPupilService
    {
        void Create(Pupil pupil);
        void Remove(Pupil pupil);
        Pupil GetById(int id);
    }
}