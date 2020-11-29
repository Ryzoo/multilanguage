using System.Collections.Generic;
using Application.Interfaces.Database.Models;

namespace Application.Interfaces.ChainOfResponsibility.Language
{
    public interface ILanguageHandler
    {
        ILanguageHandler SetNext(ILanguageHandler handler);
        T Handle<T>(ICollection<T> request) where T : ILanguageContent;
    }
}