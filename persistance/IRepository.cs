using lab8.Domain;
using System;
using System.Collections.Generic;

namespace lab8.Repository
{
    // CRUD operations repository interface
    // ID - type E must have an attribute of type ID
    // E - type of entities saved in repository
    public interface IRepository<ID, E> where E : Entity<ID>
    {
        // Retrieves an entity by its ID
        // Returns the entity with the specified ID, or null if not found
        // Throws an ArgumentException if the ID is null
        E FindOne(ID id);

        // Retrieves all entities
        List<E> FindAll();

        // Saves an entity
        // Returns null if the entity is saved successfully, otherwise returns the entity (if ID already exists)
        // Throws an ArgumentException if the entity is not valid
        E Save(E entity);

        // Deletes the entity with the specified ID
        // Returns the removed entity, or null if no entity with the given ID exists
        // Throws an ArgumentException if the ID is null
        E Delete(ID id);

        // Updates an entity
        // Returns null if the entity is updated successfully, otherwise returns the entity (e.g., ID does not exist)
        // Throws an ArgumentException if the entity is null or not valid
        E Update(E entity);
    }

   
  
}