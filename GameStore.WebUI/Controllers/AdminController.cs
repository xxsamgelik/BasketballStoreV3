using GameStore.Domain.Abstract;
using GameStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using GameStore.WebUI.Models;
using NPOI.XWPF.UserModel;
using OfficeOpenXml;
using Text = OfficeOpenXml.FormulaParsing.Excel.Functions.Text.Text;

namespace GameStore.WebUI.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        IGameRepository repository;

        public AdminController(IGameRepository repo)
        {
            repository = repo;
        }

        public ViewResult Index()
        {
            return View(repository.Games);
        }

        public ActionResult Edit(Game game, HttpPostedFileBase image = null)
        {
            string previousName = Request.Form["Name"];
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    game.ImageMimeType = image.ContentType;
                    game.ImageData = new byte[image.ContentLength];
                    image.InputStream.Read(game.ImageData, 0, image.ContentLength);
                }

                repository.SaveGame(game);
                TempData["message"] = string.Format("Изменения в игре \"{0}\" были сохранены", game.Name);
                return RedirectToAction("Index");
            }
            else
            {
                // Что-то не так со значениями данных
                return View(game);
            }
        }

        public ViewResult Create()
        {
            return View("Edit", new Game());
        }

        [HttpPost]
        public ActionResult Delete(int gameId)
        {
            Game deletedGame = repository.DeleteGame(gameId);
            if (deletedGame != null)
            {
                TempData["message"] = string.Format("Товар \"{0}\" был удален",
                    deletedGame.Name);
            }

            return RedirectToAction("Index");
        }

        public FileResult ExportToExcel()
        {
            IEnumerable<Game> data = repository.GetGames();
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Data");

                worksheet.Cells[1, 1].Value = "Категория";
                worksheet.Cells[1, 2].Value = "Название";
                worksheet.Cells[1, 3].Value = "Описание";
                worksheet.Cells[1, 4].Value = "Цена";
                worksheet.Cells[1, 5].Value = "Тип изображения";

                int row = 2;
                foreach (var item in data)
                {
                    worksheet.Cells[row, 1].Value = item.Category;
                    worksheet.Cells[row, 2].Value = item.Name;
                    worksheet.Cells[row, 3].Value = item.Description;
                    worksheet.Cells[row, 4].Value = item.Price;
                    worksheet.Cells[row, 5].Value = item.ImageMimeType;

                    row++;
                }

                byte[] excelBytes = package.GetAsByteArray();

                return File(excelBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                    "Товары.xlsx");
            }
        }

        public ActionResult ExportToCsv()
        {
            IEnumerable<Game> data = repository.GetGames();

            StringBuilder csvContent = new StringBuilder();
            csvContent.AppendLine("Category,Name,Description,Price,TypeImage");

            foreach (var item in data)
            {
                string csvLine = string.Format("{0},{1},{2},{3},{4}", item.Description, item.Category,
                    item.ImageMimeType, item.Name, item.Price);
                csvContent.AppendLine(csvLine);
            }

            byte[] csvBytes = Encoding.UTF8.GetBytes(csvContent.ToString());

            return File(csvBytes, "text/csv", "Товары.csv");
        }

        public ActionResult ExportToWord()
        {
            List<Game> users = repository.GetGamesList();

            using (MemoryStream stream = new MemoryStream())
            {
                XWPFDocument doc = new XWPFDocument();

                XWPFTable table = doc.CreateTable(users.Count + 1, 4);

                table.GetRow(0).GetCell(0).SetText("Название");
                table.GetRow(0).GetCell(1).SetText("Описание");
                table.GetRow(0).GetCell(2).SetText("Категория");
                table.GetRow(0).GetCell(3).SetText("Цена");

                for (int i = 0; i < users.Count; i++)
                {
                    table.GetRow(i + 1).GetCell(0).SetText(users[i].Name);
                    table.GetRow(i + 1).GetCell(1).SetText(users[i].Description);
                    table.GetRow(i + 1).GetCell(2).SetText(users[i].Category);
                    table.GetRow(i + 1).GetCell(3).SetText(users[i].Price.ToString());
                }

                doc.Write(stream);

                return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
                    "Товары.docx");
            }

        }
        public FileResult ExportToExcelUsers()
        {
            IEnumerable<Game> data = repository.GetGames();
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Data");

                worksheet.Cells[1, 1].Value = "Категория";
                worksheet.Cells[1, 2].Value = "Название";
                worksheet.Cells[1, 3].Value = "Описание";
                worksheet.Cells[1, 4].Value = "Цена";
                worksheet.Cells[1, 5].Value = "Тип изображения";

                int row = 2;
                foreach (var item in data)
                {
                    worksheet.Cells[row, 1].Value = item.Category;
                    worksheet.Cells[row, 2].Value = item.Name;
                    worksheet.Cells[row, 3].Value = item.Description;
                    worksheet.Cells[row, 4].Value = item.Price;
                    worksheet.Cells[row, 5].Value = item.ImageMimeType;

                    row++;
                }

                byte[] excelBytes = package.GetAsByteArray();

                return File(excelBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                    "Товары.xlsx");
            }
        }


    }
}