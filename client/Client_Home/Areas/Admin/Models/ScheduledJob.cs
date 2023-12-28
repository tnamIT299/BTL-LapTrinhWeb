using Client_Home.Areas.Admin.Services.SendEmail;
using Client_Home.Data;
using Client_Home.Models;
using System.Configuration;

namespace Client_Home.Areas.Admin.Models
{
    public class ScheduledJob : IHostedService, IDisposable
    {
        private readonly ILogger<ScheduledJob> _logger;
        private readonly IEmailService _emailService; // Thay thế IEmailService bằng service gửi email của bạn
        private Timer _timer;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public ScheduledJob(ILogger<ScheduledJob> logger, IEmailService emailService, IServiceScopeFactory serviceScopeFactory)
        {
            _logger = logger;
            _emailService = emailService;
            _serviceScopeFactory = serviceScopeFactory;
        }
        public ScheduledJob(ILogger<ScheduledJob> logger, IEmailService emailService)
        {
            _logger = logger;
            _emailService = emailService;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromHours(24));
            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            _logger.LogInformation("Scheduled job is running at: " + DateTimeOffset.Now);

            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<Data.ConveniencestoreContext>();
                // Lấy ngày hiện tại
                var currentDate = DateTime.Now.Date;

                // Lấy tất cả ProductBatch từ cơ sở dữ liệu
                var allProductBatches = dbContext.ProductBatches.ToList();

                // Tạo danh sách để lưu các ProductBatch cần thông báo
                var batchesToNotify = new List<ProductBatch>();

                // Kiểm tra từng ProductBatch và thêm vào danh sách cần thông báo
                foreach (var batch in allProductBatches)
                {
                    // Kiểm tra ExpiryDate để xác định nội dung email
                    if (DateTime.Compare(batch.ExpiryDate ?? DateTime.MaxValue, currentDate) <= 0 ||
                        DateTime.Compare(batch.ExpiryDate ?? DateTime.MaxValue, currentDate) < 5)
                    {
                        batchesToNotify.Add(batch);
                    }
                }

                // Gửi một email thông báo nếu có ProductBatch cần thông báo
                if (batchesToNotify.Any())
                {
                    // Tạo nội dung email dựa trên danh sách batchesToNotify
                    var emailSubject = "Thông báo về hạn sử dụng sản phẩm";
                    var emailBody = "Danh sách các sản phẩm gần hết hạn hoặc đã hết hạn:\n";

                    foreach (var batch in batchesToNotify)
                    {
                        emailBody += $"- Sản phẩm có BatchID {batch.BatchId}, ExpiryDate: {batch.ExpiryDate}\n";
                    }

                    // Gửi email
                    _emailService.SendEmail("Sếp Phú Huy", "phuhuy12330@gmail.com", emailSubject, emailBody);
                }
            }
        }



        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Scheduled job is stopping.");
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }


}
