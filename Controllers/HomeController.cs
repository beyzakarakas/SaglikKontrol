using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DiabetWebSite.Models;
using System;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using DiabetWebSite.Models.ViewModels;

namespace DiabetWebSite.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public IActionResult BmiCalculate()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            var bmiRecords = _context.BodyMassIndexes
                .Where(b => b.UserId == int.Parse(userId))
                .ToList();

            var model = new BmiCalculateViewModel
            {
                BmiRecords = bmiRecords
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult BmiCalculate(decimal height, decimal weight)
        {
            if (height <= 0 || weight <= 0)
            {
                ViewBag.ErrorMessage = "Geçersiz boy veya kilo değeri.";
                return View();
            }

            var bmi = weight / ((height / 100) * (height / 100));
            ViewBag.BmiResult = bmi;

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            var userExists = _context.Users.Any(u => u.UserId == int.Parse(userId));
            if (!userExists)
            {
                return BadRequest("Kullanıcı bulunamadı.");
            }

            var bmiRecord = new BodyMassIndex
            {
                UserId = int.Parse(userId),
                HeightCm = height,
                WeightKg = weight,
                BMICalculated = bmi, // Hesaplanan BMI değerini kaydediyoruz
                MeasurementTime = DateTime.Now,
                Notes = "Hesaplanan BMI"
            };

            _context.BodyMassIndexes.Add(bmiRecord);
            _context.SaveChanges();

            var bmiRecords = _context.BodyMassIndexes
                .Where(b => b.UserId == int.Parse(userId))
                .ToList();

            var model = new BmiCalculateViewModel
            {
                BmiRecords = bmiRecords
            };

            return View(model);
        }

        [HttpGet]
        [Authorize]
        public IActionResult UserInfo()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            var user = _context.Users.SingleOrDefault(u => u.UserId == int.Parse(userId));
            if (user == null)
            {
                return NotFound();
            }

            var model = new UserViewModel
            {
                Username = user.Username,
                Email = user.Email,
                CreatedAt = user.CreatedAt
            };

            return View(model);
        }


        [HttpGet]
        [Authorize]
        public IActionResult BloodPressure()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            var bloodPressureRecords = _context.BloodPressures
                .Where(bp => bp.UserId == int.Parse(userId))
                .OrderByDescending(bp => bp.MeasurementTime)
                .ToList();

            var model = new BloodPressureViewModel
            {
                BloodPressureRecords = bloodPressureRecords
            };

            return View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> BloodPressure(BloodPressureViewModel model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return Unauthorized();
            }

            var bloodPressure = new BloodPressure
            {
                UserId = int.Parse(userId),
                Systolic = model.Systolic,
                Diastolic = model.Diastolic,
                MeasurementTime = DateTime.Now,
                Notes = model.Notes
            };

            _context.BloodPressures.Add(bloodPressure);
            await _context.SaveChangesAsync();

            return RedirectToAction("BloodPressure");
        }

        [HttpGet]
        public async Task<IActionResult> BloodSugar()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Unauthorized();
            }

            var bloodSugarRecords = await _context.BloodSugars
                .Where(bs => bs.UserId == int.Parse(userId))
                .ToListAsync();

            var model = new BloodSugarViewModel
            {
                BloodSugarRecords = bloodSugarRecords
            };

            return View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> BloodSugar(BloodSugarViewModel model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return Unauthorized();
            }

            var bloodSugar = new BloodSugar
            {
                UserId = int.Parse(userId),
                MeasurementValue = model.NewMeasurementValue,
                MeasurementTime = DateTime.Now
            };

            _context.BloodSugars.Add(bloodSugar);
            await _context.SaveChangesAsync();

            return RedirectToAction("BloodSugar");
        }


        public IActionResult InsulinInfo()
        {
            return View();
        }

        public IActionResult Exercises()
        {
            return View();
        }

        public IActionResult OurServices()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult FindRisc()
        {
            var questions = GetQuestions();
            return View(questions);

        }

        public IActionResult Recommendations()
        {
            return View();
        }

        private List<Question> GetQuestions()
        {
            return new List<Question>
    {
        new Question
        {
            Id = 1,
            QuestionText = "Kaç yaşındasınız?",
            Answers = new List<Answer>
            {
                new Answer { Id = 1, AnswerText = "45'den küçük", Points = 0 },
                new Answer { Id = 2, AnswerText = "45-54 yaş arası", Points = 2 },
                new Answer { Id = 3, AnswerText = "55-64 yaş arası", Points = 3 },
                new Answer { Id = 4, AnswerText = "65 yaş ve üzeri", Points = 4 }
            }
        },
        new Question
        {
            Id = 2,
            QuestionText = "Vücut kitle indeksi ölçünüz kaç?",
            Answers = new List<Answer>
            {
                new Answer { Id = 1, AnswerText = "25 kg/m²'den küçük", Points = 0 },
                new Answer { Id = 2, AnswerText = "25-30 kg/m² arası", Points = 1 },
                new Answer { Id = 3, AnswerText = "30 kg/m²'den büyük", Points = 3 }
            }
        },
        new Question
        {
            Id = 3,
            QuestionText = "Bel çevreniz kaç cm?",
            Answers = new List<Answer>
            {
                new Answer { Id = 1, AnswerText = "Erkekler: 94 cm'den küçük", Points = 0 },
                new Answer { Id = 2, AnswerText = "Erkekler: 94-102 cm arası", Points = 3 },
                new Answer { Id = 3, AnswerText = "Erkekler: 102 cm'den büyük", Points = 4 },
                new Answer { Id = 4, AnswerText = "Kadınlar: 80 cm'den küçük", Points = 0 },
                new Answer { Id = 5, AnswerText = "Kadınlar: 80-88 cm arası", Points = 3 },
                new Answer { Id = 6, AnswerText = "Kadınlar: 88 cm'den büyük", Points = 4 }
            }
        },
        new Question
        {
            Id = 4,
            QuestionText = "Günde en az 30 dk aktif misiniz?",
            Answers = new List<Answer>
            {
                new Answer { Id = 1, AnswerText = "Evet", Points = 0 },
                new Answer { Id = 2, AnswerText = "Hayır", Points = 2 }
            }
        },
        new Question
        {
            Id = 5,
            QuestionText = "Hangi sıklıkta sebze-meyve tüketiyorsunuz?",
            Answers = new List<Answer>
            {
                new Answer { Id = 1, AnswerText = "Her gün", Points = 0 },
                new Answer { Id = 2, AnswerText = "Her gün değil", Points = 2 }
            }
        },
        new Question
        {
            Id = 6,
            QuestionText = "Kan basıncı yüksekliği için hiç ilaç kullandınız mı veya sizde yüksek tansiyon bulundu mu?",
            Answers = new List<Answer>
            {
                new Answer { Id = 1, AnswerText = "Hayır", Points = 0 },
                new Answer { Id = 2, AnswerText = "Evet", Points = 2 }
            }
        },
        new Question
        {
            Id = 7,
            QuestionText = "Hiç kan şekeriniz yüksek veya sınırda oldu mu?",
            Answers = new List<Answer>
            {
                new Answer { Id = 1, AnswerText = "Hayır", Points = 0 },
                new Answer { Id = 2, AnswerText = "Evet", Points = 5 }
            }
        },
        new Question
        {
            Id = 8,
            QuestionText = "Aile bireylerinizden herhangi birinde diyabet tanısı konulmuş muydu?",
            Answers = new List<Answer>
            {
                new Answer { Id = 1, AnswerText = "Hayır", Points = 0 },
                new Answer { Id = 2, AnswerText = "Evet (2. derece yakınlarda)", Points = 3 },
                new Answer { Id = 3, AnswerText = "Evet (1. derece yakınlarda)", Points = 5 }
            }
        }
    };
        }

        [HttpPost]
        public IActionResult FindRisc(List<UserAnswer> userAnswers)
        {
            int totalRiskPoints = 0;
            var questions = GetQuestions();

            foreach (var userAnswer in userAnswers)
            {
                var question = questions.FirstOrDefault(s => s.Id == userAnswer.QuestionId);
                if (question != null)
                {
                    var answer = question.Answers.FirstOrDefault(c => c.Id == userAnswer.AnswerId);
                    if (answer != null)
                    {
                        totalRiskPoints += answer.Points;
                    }
                }
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!int.TryParse(userId, out var intUserId))
            {
                return BadRequest("Geçersiz kullanıcı kimliği.");
            }

            var riskResult = new FindRiscResult
            {
                UserId = intUserId,
                TestDate = DateTime.Now,
                TotalRiskPoints = totalRiskPoints,
                DegreeOfRisk = DegreeOfRisk(totalRiskPoints),
                TenYearRiskRating = TenYearRiskRating(totalRiskPoints),
                RoutineScreening = RoutineScreening(totalRiskPoints),
                UserAnswers = userAnswers
            };

            _context.FindRiscResults.Add(riskResult);
            _context.SaveChanges();

            // Prepare the ViewModel and pass it to the RiscResults view
            var viewModel = new RiscResultsViewModel
            {
                TotalRiskPoints = riskResult.TotalRiskPoints,
                DegreeOfRisk = riskResult.DegreeOfRisk,
                TenYearRiskRating = riskResult.TenYearRiskRating,
                RoutineScreening = riskResult.RoutineScreening
            };

            return View("RiscResults", viewModel);
        }

        public async Task<IActionResult> RiscResults(int userId)
        {
            var result = await _context.FindRiscResults
                .Where(r => r.UserId == userId)
                .OrderByDescending(r => r.TestDate)
                .FirstOrDefaultAsync();

            if (result == null)
            {
                return NotFound();
            }

            var viewModel = new RiscResultsViewModel
            {
                TotalRiskPoints = result.TotalRiskPoints,
                DegreeOfRisk = result.DegreeOfRisk,
                TenYearRiskRating = result.TenYearRiskRating,
                RoutineScreening = result.RoutineScreening
            };

            return View(viewModel);
        }

        public string DegreeOfRisk(int totalRiskPoints)
        {
            if (totalRiskPoints < 7)
            {
                return "Düşük";
            }
            else if (totalRiskPoints >= 7 && totalRiskPoints <= 11)
            {
                return "Hafif";
            }
            else if (totalRiskPoints >= 12 && totalRiskPoints <= 14)
            {
                return "Orta";
            }
            else if (totalRiskPoints >= 15 && totalRiskPoints <= 20)
            {
                return "Yüksek";
            }
            else
            {
                return "Çok Yüksek";
            }
        }

        public string TenYearRiskRating(int totalRiskPoints)
        {
            if (totalRiskPoints < 7)
            {
                return "%1";
            }
            else if (totalRiskPoints >= 7 && totalRiskPoints <= 11)
            {
                return "%4";
            }
            else if (totalRiskPoints >= 12 && totalRiskPoints <= 14)
            {
                return "%16";
            }
            else if (totalRiskPoints >= 15 && totalRiskPoints <= 20)
            {
                return "%33";
            }
            else
            {
                return "%50";
            }
        }

        public string RoutineScreening(int totalRiskPoints)
        {
            if (totalRiskPoints < 15)
            {
                return "Önerilmez";
            }
            else
            {
                return "Önerilir";
            }
        }

        public async Task<IActionResult> HomePage()
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userIdString) || !int.TryParse(userIdString, out int userId))
            {
                return Unauthorized();
            }

            var latestBmi = await _context.BodyMassIndexes
                .Where(b => b.UserId == userId)
                .OrderByDescending(b => b.MeasurementTime)
                .FirstOrDefaultAsync();

            var latestBloodPressure = await _context.BloodPressures
                .Where(bp => bp.UserId == userId)
                .OrderByDescending(bp => bp.MeasurementTime)
                .FirstOrDefaultAsync();

            var latestBloodSugar = await _context.BloodSugars
                .Where(bs => bs.UserId == userId)
                .OrderByDescending(bs => bs.MeasurementTime)
                .FirstOrDefaultAsync();

            var latestFindRiscResult = await _context.FindRiscResults
                .Where(frr => frr.UserId == userId)
                .OrderByDescending(frr => frr.TestDate)
                .FirstOrDefaultAsync();

            var model = new HomePageViewModel
            {
                LatestBmi = latestBmi,
                LatestBloodPressure = latestBloodPressure,
                LatestBloodSugar = latestBloodSugar,
                LatestFindRiscResult = latestFindRiscResult
            };

            return View(model);
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            if (ModelState.IsValid)
            {
                var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == email);
                if (user != null && BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
                {
                    var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Name, user.Username)
            };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    var authProperties = new AuthenticationProperties
                    {
                        IsPersistent = true
                    };

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

                    return RedirectToAction("HomePage");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Geçersiz giriş bilgileri.");
                }
            }
            return View();
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult SigninPage()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SigninPage(string username, string email, string password)
        {
            if (ModelState.IsValid)
            {
                // E-posta adresinin daha önceden kullanılıp kullanılmadığını kontrol et
                if (_context.Users.Any(u => u.Email == email))
                {
                    ModelState.AddModelError("Email", "Bu e-posta adresi zaten kullanılıyor.");
                    return View();
                }

                var user = new User
                {
                    Username = username,
                    Email = email,
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword(password),
                    CreatedAt = DateTime.Now
                };

                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction("Login", "Home");
            }
            return View();
        }


        [HttpGet]
        [Authorize]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Formu düzgün doldurunuz.";
                return View(model);
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                TempData["ErrorMessage"] = "Kullanıcı oturumu bulunamadı.";
                return Unauthorized();
            }

            var user = await _context.Users.FindAsync(int.Parse(userId));
            if (user == null)
            {
                TempData["ErrorMessage"] = "Kullanıcı bulunamadı.";
                return NotFound();
            }

            if (!BCrypt.Net.BCrypt.Verify(model.OldPassword, user.PasswordHash))
            {
                TempData["ErrorMessage"] = "Geçersiz eski şifre.";
                return View(model);
            }

            if (model.NewPassword != model.ConfirmPassword)
            {
                TempData["ErrorMessage"] = "Şifreler eşleşmiyor.";
                return View(model);
            }

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.NewPassword);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Parolanız başarıyla güncellendi.";
            return RedirectToAction("ChangePassword");
        }

        public async Task<IActionResult> DeleteAccount()
        {
            // Kullanıcıyı bulalım (örneğin, şu an oturum açmış olan kullanıcıyı alıyoruz)
            var username = User.Identity.Name;
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Username == username);

            if (user == null)
            {
                return NotFound();
            }

            // Kullanıcıyı silme işlemi
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            // Oturumu temizleyelim (isteğe bağlı)
            // await HttpContext.SignOutAsync();

            // Kullanıcıyı başka bir sayfaya yönlendirme (örneğin, ana sayfa veya çıkış sayfası)
            return RedirectToAction("Index", "Home");
        }
        public IActionResult AccessDenied()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
