using Microsoft.EntityFrameworkCore;

namespace Retro.Module.Board.Infrastructure.Persistence;

public class BoardDbContext(DbContextOptions<BoardDbContext> options) : DbContext(options);