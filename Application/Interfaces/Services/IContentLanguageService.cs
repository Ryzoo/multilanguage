using System.Collections.Generic;
using Application.Interfaces.Database.Models;

namespace Application.Interfaces.Services
{
    public interface IContentLanguageService
    {
        public T PrepareContent<T>(ICollection<T> elements)
            where T : ILanguageContent;
    }
}