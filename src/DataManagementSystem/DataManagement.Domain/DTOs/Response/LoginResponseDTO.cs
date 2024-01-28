namespace DataManagement.Domain.DTOs.Response
{
	public class LoginResponseDTO
	{
		public LoginResponseDTO()
		{
			Token = String.Empty;
		}

		public string Token { get; set; }
	}
}
