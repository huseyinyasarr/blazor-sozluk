using AutoMapper;
using BlazorSozluk.Common.Infrastructer;
using BlazorSozluk.Common.Infrastructer.Exceptions;
using BlazorSozluk.Common.Models.Queries;
using BlazorSozluk.Common.Models.RequestModels;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;



namespace BlazorSozluk.Application.Features.Commands.User.Login;

public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, LoginUserViewModel>
{
    private readonly IUserRepository userRepository;
    private readonly IMapper mapper;
    private readonly IConfiguration configuration;

    public LoginUserCommandHandler(IUserRepository userRepository, IMapper mapper, IConfiguration configuration)
    {
        this.userRepository = userRepository;
        this.mapper = mapper;
        this.configuration = configuration;
    }

    public async Task<LoginUserViewModel> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var dbUser = await userRepository.GetSingleAsync(i => i.EmailAddress == request.EmailAddress);

        if (dbUser == null)
        {
            throw new DatabaseValidationException("Böyle bir kullanıcı yok!");
        }

        var pass = PasswordEncryptor.Encrpt(request.Password);


        // password'ler, database'de md5 ile hash'lenerek saklanır. Bunun için login ekranında kullanıcının girdiği şifreyi hash'leyerek database'den gelen veri ile kıyaslamak gerekiyor.
        //aşağıdaki blok, üstte yazıldığı gibi doğru olan yönteme göre çalışmakta ancak test için kullanıcı bilgilerini post ederken password bilgisinin hash'siz halini bilmiyoruz (veriler bogus tarafından otomatik olarak oluşturuldu)
        if (dbUser.Password != pass)
        {
            throw new DatabaseValidationException("Şifre hatalı");
        }


        // kullınıcıdan gelen veriyi (yani test için postladığımız şifre) db'den manuel olarak aldığım hash'i tekrardan hash'leyip, ardından post'ladığım "pass" (üstteki satırda hash'leniyor) ile kıyaslıyorum. Bu blok tamamen test amaçlıdır
        //if (PasswordEncryptor.Encrpt(dbUser.Password) != pass)
        //{
        //    throw new DatabaseValidationException("Şifre hatalı");
        //}



        // db'den gelen hash ile db'den test için aldığım hesh'in kıyaslaması
        //if (dbUser.Password != "1F4372948CB5EFDEA04B1A855B69FED2")
        //{
        //    throw new DatabaseValidationException("Şifre hatalı");
        //}

        if (!dbUser.EmailConfirmed)
        {
            throw new DatabaseValidationException("Hesabın onaylanmamış, önce hesabını onaylamalısın");
        }

        var result = mapper.Map<LoginUserViewModel>(dbUser);

        var claims = new Claim[]
        {
            new Claim(ClaimTypes.NameIdentifier, dbUser.Id.ToString()),
            new Claim(ClaimTypes.Email, dbUser.EmailAddress),
            new Claim(ClaimTypes.Name, dbUser.UserName),
            new Claim(ClaimTypes.GivenName, dbUser.FirstName),
            new Claim(ClaimTypes.Surname, dbUser.LastName)

        };

        result.Token = GenerateToken(claims);

        return result;

    }

    private string GenerateToken(Claim[] claims)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["AuthConfig:Secret"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expiry = DateTime.Now.AddDays(10);

        var token = new JwtSecurityToken(claims: claims,
                                         expires: expiry,
                                         signingCredentials: creds,
                                         notBefore: DateTime.Now);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

}
