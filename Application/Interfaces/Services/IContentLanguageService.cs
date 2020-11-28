using System.Collections.Generic;
using Application.Models;

namespace Application.Interfaces.Services
{
    public interface IContentLanguageService
    {
        public T PrepareContent<T>(ICollection<T> elements)
            where T : ILanguageContent;
    }
}