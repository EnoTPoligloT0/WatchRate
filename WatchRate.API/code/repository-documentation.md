# WatchRate Repository Interfaces Documentation

## IMovieRepository

Handles operations related to movies and their associated data.

```csharp
public interface IMovieRepository
{
    // Core Movie Operations
    Task<Movie?> GetByIdAsync(MovieId id);
    Task<Movie?> GetByDbIdAsync(string dbId);
    Task<IEnumerable<Movie>> GetAllAsync(int page = 1, int pageSize = 10);
    Task<Movie> AddAsync(Movie movie);
    Task UpdateAsync(Movie movie);
    Task DeleteAsync(MovieId id);
    
    // Search and Filter Operations
    Task<IEnumerable<Movie>> SearchAsync(string searchTerm, int page = 1, int pageSize = 10);
    Task<IEnumerable<Movie>> GetByYearAsync(short year, int page = 1, int pageSize = 10);
    Task<IEnumerable<Movie>> GetByGenreAsync(string genre, int page = 1, int pageSize = 10);
    Task<IEnumerable<Movie>> GetByMaturityRatingAsync(MaturityRating rating, int page = 1, int pageSize = 10);
    
    // Rating Operations
    Task UpdateRatingAsync(MovieId movieId, decimal newRating, int totalRatings);
    Task<decimal> GetAverageRatingAsync(MovieId movieId);
    
    // Cast and Crew Operations
    Task<IEnumerable<MovieCast>> GetCastAsync(MovieId movieId);IUse appropriate indexes in the database
   - Implement caching where beneficial
   - Use projection queries for read operations where possible
   - Consider implementing lazy loading for related entities

4. Transaction Management:
   - Operations modifying multiple entities should be transactional
   - Use Unit of Work pattern for transaction management

5. Filtering and Sorting:
   - Consider implementing specification pattern for complex queries
   - Add sorting parameters to relevant methods
   - Implement filter builders for complex search operations

6. Security:
   - Implement row-level security where needed
   - Validate user permissions before operations
   - Sanitize all input parameters

