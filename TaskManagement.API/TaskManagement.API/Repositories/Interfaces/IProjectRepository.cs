using TaskManagement.API.Models;

namespace TaskManagement.API.Repositories.Interfaces
{
    /// <summary>
    /// Defines repository contract for data access operations related to projects.
    /// </summary>
    public interface IProjectRepository
    {
        /// <summary>
        /// Retrieves all projects from the database.
        /// </summary>
        /// <returns>A list of all <see cref="Project"/> entities.</returns>
        Task<List<Project>> GetAllProjects();

        /// <summary>
        /// Retrieves a project entity by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the project.</param>
        /// <returns>The matching <see cref="Project"/> entity if found; otherwise, <c>null</c>.</returns>
        Task<Project?> GetProjectById(int id);

        /// <summary>
        /// Persists a new project entity into the database.
        /// </summary>
        /// <param name="project">The project entity to create.</param>
        /// <returns>The created <see cref="Project"/> entity with generated identifiers.</returns>
        Task<Project> CreateProject(Project project);

        /// <summary>
        /// Updates an existing project entity in the database.
        /// </summary>
        /// <param name="project">The project entity containing updated values.</param>
        /// <returns>The updated <see cref="Project"/> entity.</returns>
        Task<Project> UpdateProject(Project project);

        /// <summary>
        /// Removes a project entity from the database.
        /// </summary>
        /// <param name="project">The project entity to delete.</param>
        /// <returns><c>true</c> if the deletion was successful; otherwise, <c>false</c>.</returns>
        Task<bool> DeleteProject(Project project);
    }
}