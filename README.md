To launch the application, execute docker compose up command from the root directory of the solution. Once the containers are running, the api explorer will be accessible at http://localhost:5108/scalar

Value for airport field 

<img width="173" height="418" alt="image" src="https://github.com/user-attachments/assets/e773cd7f-54a1-48ee-a6fc-1ce44907ffc1" />


Notes: 
- Technical choices of Postgres as db, and Scalar (instead of Swagger) are driven by personnal preference as I use them for personal projects.
- Validation rules implementation location could be improved instead of using public static methods, I have made this choice in order to go quick and be able to test it easily.
- The separation between the persistence and business layers could be refined, particularly regarding naming conventions and folder/project organization.
- The implementation of Entity Framework Core could be improved. Having only recently resumed using EF Core through a personal project (I used EF in a past work experience but in my last experience we were not using it), I focused primarily on achieving functional results. While the current implementation works as intended, certain method choices and design decisions may not fully align with best practices.
- I used an enumeration to represent airports in order to restrict the set of permissible values. However, this approach is not the most readable or user-friendly and could be improved for better clarity and maintainability.
- Test coverage is limited. In a real production scenario, I would implement integration tests to validate the entire processing pipeline.
- Db credentials are in plain text in compose as it is a technical test but in production it should be manage using a correct secret management mechanism.
- Error management could to be improve to log and return specific error code and each error case
