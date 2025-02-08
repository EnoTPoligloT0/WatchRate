# Domain Model for MovieRate Platform

## Aggregates

### 1. Movie Aggregate

Root: Movie

```json
{
  "id": {"value": "00000000-0000-0000-0000-000000000000"},
  "dbid": {"value": "tt000000"},
  "title": "The Shawshank Redemption",
  "description": "Two imprisoned men bond over a number of years...",
  "year": 1994,
  "runtime": 142,
  "maturityRating": "R",
  "averageRating": 9.3,
  "posterUrl": "https://...",
  "backdropUrl": "https://...",
  "trailerUrl": "https://...",
  "streamingUrls": [
    {
      "platform": "Netflix",
      "url": "https://..."
    }
  ],
  "genres": [
    {
      "id": {"value": "00000000-0000-0000-0000-000000000000"},
      "name": "Drama"
    }
  ],
  "cast": [
    {
      "id": {"value": "00000000-0000-0000-0000-000000000000"},
      "personId": {
        "value": "00000000-0000-0000-0000-000000000000"},
      "character": "Andy Dufresne",
      "order": 1
    }
  ],
  "crew": [
    {
      "id": {
        "value": "00000000-0000-0000-0000-000000000000"
      },
      "personId": {
        "value": "00000000-0000-0000-0000-000000000000"
      },
      "role": "Director",
      "department": "Directing"
    }
  ],
  "createdDateTime": "2024-01-01T00:00:00.0000000Z",
  "updatedDateTime": "2024-01-01T00:00:00.0000000Z"
}
```

### 2. Person Aggregate

Root: Person

```json
{
  "id": {
    "value": "00000000-0000-0000-0000-000000000000"
  },
  "name": "Tim Robbins",
  "biography": "Born in West Covina, California...",
  "birthDate": "1958-10-16T00:00:00.0000000Z",
  "birthPlace": "West Covina, California, USA",
  "profileImageUrl": "https://...",
  "createdDateTime": "2024-01-01T00:00:00.0000000Z",
  "updatedDateTime": "2024-01-01T00:00:00.0000000Z"
}
```

### 3. User Aggregate

Root: User

```json
{
  "id": {
    "value": "00000000-0000-0000-0000-000000000000"
  },
  "email": "user@example.com",
  "username": "moviefan123",
  "passwordHash": "hashedPassword",
  "profilePictureUrl": "https://...",
  "watchlist": [
    {
      "id": {
        "value": "00000000-0000-0000-0000-000000000000"
      },
      "movieId": {
        "value": "00000000-0000-0000-0000-000000000000"
      },
      "addedDateTime": "2024-01-01T00:00:00.0000000Z"
    }
  ],
  "favorites": [
    {
      "id": {
        "value": "00000000-0000-0000-0000-000000000000"
      },
      "movieId": {
        "value": "00000000-0000-0000-0000-000000000000"
      },
      "addedDateTime": "2024-01-01T00:00:00.0000000Z"
    }
  ],
  "ratings": [
    {
      "id": {
        "value": "00000000-0000-0000-0000-000000000000"
      },
      "movieId": {
        "value": "00000000-0000-0000-0000-000000000000"
      },
      "value": 9,
      "review": "One of the best movies ever made...",
      "createdDateTime": "2024-01-01T00:00:00.0000000Z",
      "updatedDateTime": "2024-01-01T00:00:00.0000000Z"
    }
  ],
  "createdDateTime": "2024-01-01T00:00:00.0000000Z",
  "updatedDateTime": "2024-01-01T00:00:00.0000000Z"
}
```

## Value Objects

### Common

- MovieId
- PersonId
- UserId
- RatingId
- WatchlistItemId
- FavoriteId
- StreamingUrl
- RatingValue (1-10)

### Movie-specific

- Genre
- CastMember
- CrewMember
- MaturityRating

## Domain Events

### Movie

- MovieCreated
- MovieUpdated
- MovieDeleted
- MovieRatingUpdated

### User

- UserCreated
- UserRatingAdded
- UserRatingUpdated
- UserRatingRemoved
- MovieAddedToWatchlist
- MovieRemovedFromWatchlist
- MovieAddedToFavorites
- MovieRemovedFromFavorites

## Database Schema

```sql
-- Enum for Maturity Ratings
CREATE TYPE maturity_rating AS ENUM ('G', 'PG', 'PG13', 'R', 'NC17');

-- Movies Table
CREATE TABLE Movies
(
    Id              UUID PRIMARY KEY,
    ImdbId          TEXT            NOT NULL,
    Title           TEXT            NOT NULL,
    Description     TEXT,
    Year            INT             NOT NULL,
    ReleaseDate     DATE,
    Runtime         INT,
    MaturityRating  maturity_rating NOT NULL,
    AverageRating   DECIMAL(3, 1),
    TotalRatings    INT,
    PosterUrl       TEXT,
    BackdropUrl     TEXT,
    TrailerUrl      TEXT,
    CreatedDateTime TIMESTAMP       NOT NULL,
    UpdatedDateTime TIMESTAMP       NOT NULL
);

-- Persons Table
CREATE TABLE Persons
(
    Id              UUID PRIMARY KEY,
    Name            TEXT      NOT NULL,
    Biography       TEXT,
    BirthDate       DATE,
    BirthPlace      TEXT,
    ProfileImageUrl TEXT,
    CreatedDateTime TIMESTAMP NOT NULL,
    UpdatedDateTime TIMESTAMP NOT NULL
);

-- Users Table
CREATE TABLE Users
(
    Id                UUID PRIMARY KEY,
    Email             TEXT      NOT NULL UNIQUE,
    Username          TEXT      NOT NULL UNIQUE,
    PasswordHash      TEXT      NOT NULL,
    ProfilePictureUrl TEXT,
    CreatedDateTime   TIMESTAMP NOT NULL,
    UpdatedDateTime   TIMESTAMP NOT NULL
);

-- Genres Table
CREATE TABLE Genres
(
    Id   UUID PRIMARY KEY,
    Name TEXT NOT NULL UNIQUE
);

-- MovieCast Table
CREATE TABLE MovieCast
(
    Id        UUID PRIMARY KEY,
    MovieId   UUID NOT NULL REFERENCES Movies (Id) ON DELETE CASCADE,
    PersonId  UUID NOT NULL REFERENCES Persons (Id) ON DELETE CASCADE,
    Character TEXT NOT NULL,
    "Order"   INT  NOT NULL
);

-- MovieCrew Table
CREATE TABLE MovieCrew
(
    Id         UUID PRIMARY KEY,
    MovieId    UUID NOT NULL REFERENCES Movies (Id) ON DELETE CASCADE,
    PersonId   UUID NOT NULL REFERENCES Persons (Id) ON DELETE CASCADE,
    Role       TEXT NOT NULL,
    Department TEXT NOT NULL
);

-- MovieGenres Table
CREATE TABLE MovieGenres
(
    MovieId UUID NOT NULL REFERENCES Movies (Id) ON DELETE CASCADE,
    GenreId UUID NOT NULL REFERENCES Genres (Id) ON DELETE CASCADE,
    PRIMARY KEY (MovieId, GenreId)
);

-- StreamingPlatforms Table
CREATE TABLE StreamingPlatforms
(
    Id       UUID PRIMARY KEY,
    MovieId  UUID NOT NULL REFERENCES Movies (Id) ON DELETE CASCADE,
    Platform TEXT NOT NULL,
    Url      TEXT NOT NULL
);

-- UserRatings Table
CREATE TABLE UserRatings
(
    Id              UUID PRIMARY KEY,
    UserId          UUID      NOT NULL REFERENCES Users (Id) ON DELETE CASCADE,
    MovieId         UUID      NOT NULL REFERENCES Movies (Id) ON DELETE CASCADE,
    Value           INT       NOT NULL CHECK (Value BETWEEN 1 AND 10),
    Review          TEXT,
    CreatedDateTime TIMESTAMP NOT NULL,
    UpdatedDateTime TIMESTAMP NOT NULL,
    UNIQUE (UserId, MovieId)
);

-- UserWatchlist Table
CREATE TABLE UserWatchlist
(
    Id            UUID PRIMARY KEY,
    UserId        UUID      NOT NULL REFERENCES Users (Id) ON DELETE CASCADE,
    MovieId       UUID      NOT NULL REFERENCES Movies (Id) ON DELETE CASCADE,
    AddedDateTime TIMESTAMP NOT NULL,
    UNIQUE (UserId, MovieId)
);

-- UserFavorites Table
CREATE TABLE UserFavorites
(
    Id            UUID PRIMARY KEY,
    UserId        UUID      NOT NULL REFERENCES Users (Id) ON DELETE CASCADE,
    MovieId       UUID      NOT NULL REFERENCES Movies (Id) ON DELETE CASCADE,
    AddedDateTime TIMESTAMP NOT NULL,
    UNIQUE (UserId, MovieId)
);
```