﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Pustokk.CustomExceptions.BookExceptions;
using Pustokk.CustomExceptions.SliderException;
using Pustokk.Extencions;
using Pustokk.Models;
using Pustokk.Repositories.Implementations;
using Pustokk.Repositories.Interfaces;
using Pustokk.Services.Interfaces;
using InvalidContentTypeException = Pustokk.CustomExceptions.BookExceptions.InvalidContentTypeException;
using InvalidImageSizeException = Pustokk.CustomExceptions.BookExceptions.InvalidImageSizeException;
using InvalidNullReferanceException = Pustokk.CustomExceptions.BookExceptions.InvalidNullReferanceException;

namespace Pustokk.Services.Implementations
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IGenreRepository _genreRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly ITagRepository _tagRepository;
        private readonly IBookTagRepository _bookTagRepository;
        private readonly IWebHostEnvironment _env;
        private readonly IBookImageRepository _bookImageRepository;

        public BookService(IBookRepository bookRepository,
                           IGenreRepository genreRepository,
                           IAuthorRepository authorRepository,
        ITagRepository tagRepository,
                           IBookTagRepository bookTagRepository,
                           IWebHostEnvironment env,
                           IBookImageRepository bookImageRepository)

        {
            _bookRepository = bookRepository;
            _genreRepository = genreRepository;
            _authorRepository = authorRepository;
            _tagRepository = tagRepository;
            _bookTagRepository = bookTagRepository;
            _env = env;
            _bookImageRepository = bookImageRepository;
        }

        public async Task CreateAsync(Book entity)
        {
            if (!_genreRepository.Table.Any(x => x.Id == entity.GenreId))
            {
                throw new InvalidGenreException("GenreId", "Genre not found!");
            }

            if (!_authorRepository.Table.Any(x => x.Id == entity.AuthorId))
            {
                throw new InvalidAuthorException("AuthorId", "Author not found!");
            }


            bool check = false;

            if (entity.TagIds != null)
            {
                foreach (var tagId in entity.TagIds)
                {
                    if (!_tagRepository.Table.Any(x => x.Id == tagId))
                    {
                        check = true;
                        break;
                    }
                }
            }

            if (check)
            {
                throw new InvalidTagException("TagId", "Tag not found!");
            }
            else
            {
                if (entity.TagIds != null)
                {
                    foreach (var tagId in entity.TagIds)
                    {
                        BookTag bookTag = new BookTag
                        {
                            Book = entity,
                            TagId = tagId
                        };

                        await _bookTagRepository.CreateAsync(bookTag);
                    }
                }
            }

            if (entity.BookPosterImageFile != null)
            {
                if (entity.BookPosterImageFile.ContentType != "image/jpeg" && entity.BookPosterImageFile.ContentType != "image/png")
                {
                    throw new CustomExceptions.BookExceptions.InvalidContentTypeException("BookPosterImageFile", "File must be .png or .jpeg (.jpg)");
                }
                if (entity.BookPosterImageFile.Length > 2097152)
                {
                    throw new InvalidImageSizeException("BookPosterImageFile", "File size must be lower than 2mb!");
                }

                BookImage bookImage = new BookImage
                {
                    Book = entity,
                    ImageUrl = Helper.SaveFile(_env.WebRootPath, "uploads/Books", entity.BookPosterImageFile),
                    IsPoster = true
                };

                await _bookImageRepository.CreateAsync(bookImage);
            }

            if (entity.BookHoverImageFile != null)
            {
                if (entity.BookHoverImageFile.ContentType != "image/jpeg" && entity.BookHoverImageFile.ContentType != "image/png")
                {
                    throw new InvalidContentTypeException("BookHoverImageFile", "File must be .png or .jpeg (.jpg)");
                }
                if (entity.BookHoverImageFile.Length > 2097152)
                {
                    throw new InvalidImageSizeException("BookHoverImageFile", "File size must be lower than 2mb)");
                }

                BookImage bookImage = new BookImage
                {
                    Book = entity,
                    ImageUrl = Helper.SaveFile(_env.WebRootPath, "uploads/books", entity.BookHoverImageFile),
                    IsPoster = false
                };

                await _bookImageRepository.CreateAsync(bookImage);
            }


            if (entity.ImageFiles != null)
            {
                foreach (var imageFile in entity.ImageFiles)
                {
                    if (imageFile.ContentType != "image/jpeg" && imageFile.ContentType != "image/png")
                    {
                        throw new InvalidContentTypeException("ImageFiles", "File must be .png or .jpeg (.jpg)");
                    }
                    if (imageFile.Length > 2097152)
                    {
                        throw new InvalidImageSizeException("ImageFiles", "File size must be lower than 2mb)");
                    }

                    BookImage bookImage = new BookImage
                    {
                        Book = entity,
                        ImageUrl = Helper.SaveFile(_env.WebRootPath, "uploads/books", imageFile),
                        IsPoster = null
                    };

                    await _bookImageRepository.CreateAsync(bookImage);
                }
            }

            await _bookRepository.CreateAsync(entity);
            await _bookRepository.CommitAsync();
        }

        public Task Delete(int id)
        {
            throw new InvalidNullReferanceException();
        }



        public async Task<List<Book>> GetAllAsync()
        {
            return await _bookRepository.GetAllAsync(x => x.IsDeleted == false, "BookImages", "Author");
        }

        public async Task<Book> GetByIdAsync(int id)
        {
            var entity = await _bookRepository.GetByIdAsync(x => x.Id == id && x.IsDeleted == false, "Author", "BookImages", "BookTags.Tag");

            if (entity is null) throw new NullReferenceException();

            return entity;
        }

        public async Task SoftDelete(int id)
        {
            var entity = await _bookRepository.GetByIdAsync(x => x.Id == id && x.IsDeleted == false);

            if (entity is null) throw new NullReferenceException();

            entity.IsDeleted = true;
            await _bookRepository.CommitAsync();
        }

        public Task UpdateAsync(Book entity)
        {
            throw new NotImplementedException();
        }
    }
}

