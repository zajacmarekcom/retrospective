namespace Retro.Common.Results;

public record Result(bool Succeeded, IEnumerable<Error>? Errors = null);