using GS_Model;
using GS_Model.Model;
using GS_Service;
using System;

namespace GS_View
{
    internal class Program
    {
        static void Main(string[] args)
        {
            App app = new App();
            app.LoginRegisterScreen();
        }
    }
    public class App
    {
        private readonly AppUserService _userService;
        private readonly PersonService _personService;
        private AppUser _loggedUser;

        public App()
        {
            _userService = new AppUserService(new GS_Repository.AppUserRepository(), new GS_Repository.PersonRepository());
            _personService = new PersonService(new GS_Repository.PersonRepository());
        }

        public void LoginRegisterScreen()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("===== Bem-vindo =====");
                Console.WriteLine("1 - Login");
                Console.WriteLine("2 - Registrar");
                Console.WriteLine("0 - Sair");
                Console.Write("Escolha: ");
                var option = Console.ReadLine();

                if (option == "1")
                {
                    Console.Write("Username: ");
                    var username = Console.ReadLine();

                    Console.Write("Password: ");
                    var password = Console.ReadLine();

                    if (_userService.Login(username, password))
                    {
                        _loggedUser = _userService.GetByUsername(username);
                        Console.WriteLine("Login realizado com sucesso!");
                        Console.ReadKey();
                        MainMenu();
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Usuário ou senha inválidos.");
                        Console.ReadKey();
                    }
                }
                else if (option == "2")
                {
                    Console.Write("Novo Username: ");
                    var username = Console.ReadLine();

                    Console.Write("Nova Password: ");
                    var password = Console.ReadLine();

                    Console.Write("Nome completo: ");
                    var fullName = Console.ReadLine();

                    Console.Write("Email: ");
                    var email = Console.ReadLine();

                    Console.Write("CPF: ");
                    var cpf = Console.ReadLine();

                    Console.Write("Endereço: ");
                    var endress = Console.ReadLine();

                    Console.Write("País: ");
                    var country = Console.ReadLine();

                    Console.Write("Idade: ");
                    var years = int.Parse(Console.ReadLine());

                    var person = new Person
                    {
                        FullName = fullName,
                        Email = email,
                        Cpf = cpf,
                        Endress = endress,
                        Country = country,
                        Years = years
                    };

                    var user = _userService.CreateUser(username, password, person);
                    Console.WriteLine("Usuário registrado com sucesso!");
                    Console.ReadKey();
                }
                else if (option == "0")
                {
                    Environment.Exit(0);
                }
                else
                {
                    Console.WriteLine("Opção inválida.");
                    Console.ReadKey();
                }
            }
        }

        private void MainMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("===== Menu Principal =====");
                Console.WriteLine("1 - Adicionar");
                Console.WriteLine("2 - Ver");
                Console.WriteLine("3 - Editar");
                Console.WriteLine("4 - Apagar");
                Console.WriteLine("5 - Mostrar total do mês");
                Console.WriteLine("6 - Sair");
                Console.Write("Escolha: ");
                var option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        AddMenu();
                        break;
                    case "2":
                        ViewMenu();
                        break;
                    case "3":
                        EditMenu();
                        break;
                    case "4":
                        DeleteMenu();
                        break;
                    case "5":
                        ShowTotalMenu();
                        break;
                    case "6":
                        _loggedUser = null;
                        return;
                    default:
                        Console.WriteLine("Opção inválida. Pressione uma tecla para continuar.");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private void AddMenu()
        {
            Console.Clear();
            Console.WriteLine("== Adicionar ==");
            Console.WriteLine("1 - Person");
            Console.WriteLine("2 - Equipament");
            Console.WriteLine("3 - EnergyConsume");
            Console.Write("Escolha: ");
            var option = Console.ReadLine();
            switch (option)
            {
                case "1":
                    PersonCreateScreen();
                    break;
                case "2":
                    EquipamentCreateScreen();
                    break;
                case "3":
                    EnergyConsumeCreateScreen();
                    break;
                default:
                    Console.WriteLine("Opção inválida.");
                    break;
            }
            Console.ReadKey();
        }

        private void ViewMenu()
        {
            Console.Clear();
            Console.WriteLine("== Ver ==");
            Console.WriteLine("1 - Person");
            Console.WriteLine("2 - Equipament");
            Console.WriteLine("3 - EnergyConsume");
            Console.Write("Escolha: ");
            var option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    var people = _personService.GetAll();
                    foreach (var p in people)
                    {
                        Console.WriteLine($"\nID: {p.Id}");
                        Console.WriteLine($"Nome: {p.FullName}");
                        Console.WriteLine($"Email: {p.Email}");
                        Console.WriteLine($"CPF: {p.Cpf}");
                        Console.WriteLine($"Endereço: {p.Endress}");
                        Console.WriteLine($"País: {p.Country}");
                        Console.WriteLine($"Idade: {p.Years}");
                        Console.WriteLine($"User ID: {p.UserId}");
                    }
                    break;

                case "2":
                    var equipamentService = new EquipamentService(new GS_Repository.EquipamentRepository());
                    var equipaments = equipamentService.GetAll();
                    foreach (var eq in equipaments)
                    {
                        Console.WriteLine($"\nID: {eq.Id}");
                        Console.WriteLine($"Nome: {eq.Name}");
                        Console.WriteLine($"Horas/dia: {eq.HourUsedPerDay}");
                        Console.WriteLine($"Person ID: {eq.PersonId}");
                    }
                    break;

                case "3":
                    var energyService = new EnergyConsumeService(new GS_Repository.EnergyConsumeRepository(), new GS_Repository.EquipamentRepository());
                    var consumes = energyService.GetAll();
                    foreach (var ec in consumes)
                    {
                        Console.WriteLine($"\nID: {ec.Id}");
                        Console.WriteLine($"Equipamento ID: {ec.EquipamentId}");
                        Console.WriteLine($"Preço/Hora: {ec.PricePerHour}");
                        Console.WriteLine($"Preço Fixo: {ec.FixPrice}");
                        Console.WriteLine($"Mês: {ec.Month}");
                        Console.WriteLine($"Ano: {ec.Year}");
                    }
                    break;

                default:
                    Console.WriteLine("Opção inválida.");
                    break;
            }

            Console.WriteLine("\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
        }


        private void EditMenu()
        {
            Console.Clear();
            Console.WriteLine("== Editar ==");
            Console.WriteLine("1 - Person");
            Console.WriteLine("2 - Equipament");
            Console.WriteLine("3 - EnergyConsume");
            Console.Write("Escolha: ");
            var option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    EditPerson();
                    break;
                case "2":
                    EditEquipament();
                    break;
                case "3":
                    EditEnergyConsume();
                    break;
                default:
                    Console.WriteLine("Opção inválida.");
                    break;
            }
            Console.ReadKey();
        }

        private void EditPerson()
        {
            Console.Clear();
            Console.Write("ID da pessoa a ser editada: ");
            var id = int.Parse(Console.ReadLine());

            var person = _personService.GetById(id);
            if (person == null)
            {
                Console.WriteLine("Pessoa não encontrada.");
                return;
            }

            Console.Write("Nome completo: ");
            person.FullName = Console.ReadLine();
            Console.Write("Email: ");
            person.Email = Console.ReadLine();
            Console.Write("CPF: ");
            person.Cpf = Console.ReadLine();
            Console.Write("Endereço: ");
            person.Endress = Console.ReadLine();
            Console.Write("País: ");
            person.Country = Console.ReadLine();
            Console.Write("Idade: ");
            person.Years = int.Parse(Console.ReadLine());

            _personService.Update(person);
            Console.WriteLine("Pessoa atualizada com sucesso!");
        }

        private void EditEquipament()
        {
            Console.Clear();
            Console.Write("ID do equipamento a ser editado: ");
            var id = int.Parse(Console.ReadLine());

            var service = new EquipamentService(new GS_Repository.EquipamentRepository());
            var equip = service.GetById(id);
            if (equip == null)
            {
                Console.WriteLine("Equipamento não encontrado.");
                return;
            }

            Console.Write("Nome do equipamento: ");
            equip.Name = Console.ReadLine();
            Console.Write("Horas de uso por dia: ");
            equip.HourUsedPerDay = double.Parse(Console.ReadLine());

            service.Update(equip);
            Console.WriteLine("Equipamento atualizado com sucesso!");
        }

        private void EditEnergyConsume()
        {
            Console.Clear();
            Console.Write("ID do consumo a ser editado: ");
            var id = int.Parse(Console.ReadLine());

            var service = new EnergyConsumeService(
                new GS_Repository.EnergyConsumeRepository(),
                new GS_Repository.EquipamentRepository());

            var ec = service.GetById(id);
            if (ec == null)
            {
                Console.WriteLine("Consumo não encontrado.");
                return;
            }

            Console.Write("Preço por hora: ");
            ec.PricePerHour = double.Parse(Console.ReadLine());
            Console.Write("Preço fixo: ");
            ec.FixPrice = double.Parse(Console.ReadLine());

            service.Update(ec);
            Console.WriteLine("Consumo atualizado com sucesso!");
        }


        private void DeleteMenu()
        {
            Console.Clear();
            Console.WriteLine("== Apagar ==");
            Console.WriteLine("1 - Person");
            Console.WriteLine("2 - Equipament");
            Console.WriteLine("3 - EnergyConsume");
            Console.Write("Escolha: ");
            var option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    DeletePerson();
                    break;
                case "2":
                    DeleteEquipament();
                    break;
                case "3":
                    DeleteEnergyConsume();
                    break;
                default:
                    Console.WriteLine("Opção inválida.");
                    break;
            }
            Console.ReadKey();
        }

        private void DeletePerson()
        {
            Console.Clear();
            Console.Write("ID da pessoa a ser excluída: ");
            var id = int.Parse(Console.ReadLine());

            var person = _personService.GetById(id);
            if (person == null)
            {
                Console.WriteLine("Pessoa não encontrada.");
                return;
            }

            _personService.Delete(id);
            Console.WriteLine("Pessoa removida com sucesso.");
        }

        private void DeleteEquipament()
        {
            Console.Clear();
            Console.Write("ID do equipamento a ser excluído: ");
            var id = int.Parse(Console.ReadLine());

            var service = new EquipamentService(new GS_Repository.EquipamentRepository());
            var equip = service.GetById(id);
            if (equip == null)
            {
                Console.WriteLine("Equipamento não encontrado.");
                return;
            }

            service.Delete(id);
            Console.WriteLine("Equipamento removido com sucesso.");
        }

        private void DeleteEnergyConsume()
        {
            Console.Clear();
            Console.Write("ID do consumo de energia a ser excluído: ");
            var id = int.Parse(Console.ReadLine());

            var service = new EnergyConsumeService(
                new GS_Repository.EnergyConsumeRepository(),
                new GS_Repository.EquipamentRepository());

            var ec = service.GetById(id);
            if (ec == null)
            {
                Console.WriteLine("Consumo de energia não encontrado.");
                return;
            }

            service.Delete(id);
            Console.WriteLine("Consumo removido com sucesso.");
        }


        private void ShowTotalMenu()
        {
            Console.Clear();
            Console.WriteLine("== Total gasto por mês ==");

            Console.Write("Digite o mês (1-12): ");
            if (!int.TryParse(Console.ReadLine(), out int month) || month < 1 || month > 12)
            {
                Console.WriteLine("Mês inválido.");
                Console.ReadKey();
                return;
            }

            Console.Write("Digite o ano (ex: 2025): ");
            if (!int.TryParse(Console.ReadLine(), out int year))
            {
                Console.WriteLine("Ano inválido.");
                Console.ReadKey();
                return;
            }
            Console.Write("Digite o ID do funcionario: ");
            if (!long.TryParse(Console.ReadLine(), out long PersonId))
            {
                Console.WriteLine("Usuario inválido.");
                Console.ReadKey();
                return;
            }

            var energyService = new EnergyConsumeService(
                new GS_Repository.EnergyConsumeRepository(),
                new GS_Repository.EquipamentRepository());

            double total = energyService.CalculateMonthlyTotal(PersonId, month, year);

            Console.WriteLine($"Total gasto em {month}/{year}: R${total:F2}");
            Console.ReadKey();
        }


        private void PersonCreateScreen()
        {
            Console.Clear();
            Console.WriteLine("=== Cadastro de Pessoa ===");
            Console.Write("Nome completo: ");
            var fullName = Console.ReadLine();
            Console.Write("Email: ");
            var email = Console.ReadLine();
            Console.Write("CPF: ");
            var cpf = Console.ReadLine();
            Console.Write("Endereço: ");
            var endress = Console.ReadLine();
            Console.Write("País: ");
            var country = Console.ReadLine();
            Console.Write("Idade: ");
            var years = int.Parse(Console.ReadLine());

            var person = new Person
            {
                FullName = fullName,
                Email = email,
                Cpf = cpf,
                Endress = endress,
                Country = country,
                Years = years,
                UserId = _loggedUser.Id
            };

            _personService.Create(person);
            Console.WriteLine("Pessoa cadastrada com sucesso!");
        }

        private void EquipamentCreateScreen()
        {
            Console.Clear();
            Console.WriteLine("=== Cadastro de Equipamento ===");
            Console.Write("Nome do equipamento: ");
            var name = Console.ReadLine();
            Console.Write("Horas de uso por dia: ");
            var hourUsedPerDay = double.Parse(Console.ReadLine());
            Console.Write("Qual o Id da pessoa");
            var personId = long.Parse(Console.ReadLine());

            var equipament = new Equipament
            {
                Name = name,
                HourUsedPerDay = hourUsedPerDay,
                PersonId = personId
            };

            var equipamentService = new EquipamentService(new GS_Repository.EquipamentRepository());
            equipamentService.Create(equipament);
            Console.WriteLine("Equipamento cadastrado com sucesso!");
        }

        private void EnergyConsumeCreateScreen()
        {
            Console.Clear();
            Console.WriteLine("=== Cadastro de Consumo de Energia ===");
            Console.Write("ID do Equipamento: ");
            var equipamentId = int.Parse(Console.ReadLine());
            Console.Write("Preço por hora: ");
            var pricePerHour = double.Parse(Console.ReadLine());
            Console.Write("Preço fixo: ");
            var fixPrice = double.Parse(Console.ReadLine());
            Console.Write("Mês: ");
            var month = int.Parse(Console.ReadLine());
            Console.Write("Ano: ");
            var year = int.Parse(Console.ReadLine());

            var consumo = new EnergyConsume
            {
                EquipamentId = equipamentId,
                PricePerHour = pricePerHour,
                FixPrice = fixPrice,
                Month = month,
                Year = year
            };

            var energyService = new EnergyConsumeService(new GS_Repository.EnergyConsumeRepository(), new GS_Repository.EquipamentRepository());
            energyService.Create(consumo);
            Console.WriteLine("Consumo registrado com sucesso!");
        }

    }
}
