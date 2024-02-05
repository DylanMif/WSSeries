using Microsoft.VisualStudio.TestTools.UnitTesting;
using WSSeries.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WSSeries.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WSSeries.Controllers.Tests
{
    [TestClass()]
    public class SeriesControllerTests
    {
        private SeriesDBContext context;
        private SeriesController controller;
        [TestInitialize]
        public void Init()
        {
            var builder = new DbContextOptionsBuilder<SeriesDBContext>().UseNpgsql("Server=51.83.36.122;port=5432;Database=mifdyl; uid=mifdyl;password=8dNdCb;SearchPath=dylan_schema");
            SeriesDBContext context = new SeriesDBContext(builder.Options);
            controller = new SeriesController(context);
        }

        [TestMethod]
        public void Test_GetSeries_Reussi()
        {
            //Arrange
            List<Serie> expected = new List<Serie>();
            expected.Add(new Serie(1, "Scrubs", "J.D. est un jeune médecin qui débute sa carrière dans l'hôpital du Sacré-Coeur. Il vit avec son meilleur ami Turk, qui lui est chirurgien dans le même hôpital. Très vite, Turk tombe amoureux d'une infirmière Carla. Elliot entre dans la bande. C'est une étudiante en médecine quelque peu surprenante. Le service de médecine est dirigé par l'excentrique Docteur Cox alors que l'hôpital est géré par le diabolique Docteur Kelso. A cela viennent s'ajouter plein de personnages hors du commun : Todd le chirurgien obsédé, Ted l'avocat dépressif, le concierge qui trouve toujours un moyen d'embêter JD... Une belle galerie de personnage !",
                9, 184, 2001, "ABC (US)"));
            expected.Add(new Serie(2, "James May's 20th Century", "The world in 1999 would have been unrecognisable to anyone from 1900. James May takes a look at some of the greatest developments of the 20th century, and reveals how they shaped the times we live in now.",
                1, 6, 2007, "BBC Two"));
            expected.Add(new Serie(3, "True Blood", "Ayant trouvé un substitut pour se nourrir sans tuer (du sang synthétique), les vampires vivent désormais parmi les humains. Sookie, une serveuse capable de lire dans les esprits, tombe sous le charme de Bill, un mystérieux vampire. Une rencontre qui bouleverse la vie de la jeune femme...",
                7, 81, 2008, "HBO"));

            //Act
            List<Serie> actual = controller.GetSeries().Result.Value.Where(s => s.Serieid <= 3).OrderBy(s => s.Serieid).ToList();

            //Assert
            CollectionAssert.AreEqual(expected, actual, "Les listes ne sont pas les mêmes");
        }

        [TestMethod]
        public void Test_GetSerie_Reussi()
        {
            // Arrange
            Serie expected = new Serie(1, "Scrubs", "J.D. est un jeune médecin qui débute sa carrière dans l'hôpital du Sacré-Coeur. Il vit avec son meilleur ami Turk, qui lui est chirurgien dans le même hôpital. Très vite, Turk tombe amoureux d'une infirmière Carla. Elliot entre dans la bande. C'est une étudiante en médecine quelque peu surprenante. Le service de médecine est dirigé par l'excentrique Docteur Cox alors que l'hôpital est géré par le diabolique Docteur Kelso. A cela viennent s'ajouter plein de personnages hors du commun : Todd le chirurgien obsédé, Ted l'avocat dépressif, le concierge qui trouve toujours un moyen d'embêter JD... Une belle galerie de personnage !",
                9, 184, 2001, "ABC (US)");

            // Act
            var result = controller.GetSerie(1).Result;

            // Assert
            Assert.IsInstanceOfType(result.Value, typeof(Serie), "Le résultat n'est pas une série");
            Assert.AreEqual(expected, result.Value, "Le résultat n'est pas le résultat attendu");
        }

        [TestMethod]
        public void Test_GetSerie_InexistingId()
        {
            // Act
            var result = controller.GetSerie(99999999).Result;

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult), "Le résultat n'est pas un not found");
        }

        [TestMethod]
        public void Test_DeleteSerie_Reussi()
        {
            // Act
            var result = controller.DeleteSerie(1).Result;

            // Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult), "Le résultat n'est pas un no content");
        }

        [TestMethod]
        public void Test_DeleteSerie_InexistingId()
        {
            // Act
            var resultat = controller.DeleteSerie(9999999).Result;

            // Assert
            Assert.IsInstanceOfType(resultat, typeof(NotFoundResult), "Le résultat n'est pas un not found");
        }

        [TestMethod]
        [ExpectedException(typeof(System.AggregateException))]
        public void Test_PostSerie_MissingTitle()
        {
            // Arrange
            Serie serieToAdd = new Serie(789, null, "Test",
                1, 10, 2020, "ABC");

            // Act
            var resultat = controller.PostSerie(serieToAdd).Result;
        }

        [TestMethod]
        public void Test_PostSerie_Reussi()
        {
            // Arrange
            Serie serieToAdd = new Serie(1, "Test", "Test",
                1, 10, 2020, "ABC");

            // Act
            var resultat = controller.PostSerie(serieToAdd).Result;


            // Assert
            Assert.IsInstanceOfType(resultat.Result, typeof(CreatedAtActionResult), "Le résultat n'est pas un CreatedAt");
            CreatedAtActionResult routeResult = (CreatedAtActionResult)resultat.Result;
            Assert.AreEqual(serieToAdd, routeResult.Value);
        }

        [TestCleanup]
        public void CleanUp()
        {
            var res = controller.GetSerie(1).Result;
            if(res != null && res.Value == null)
            {
                Serie expected = new Serie(1, "Scrubs", "J.D. est un jeune médecin qui débute sa carrière dans l'hôpital du Sacré-Coeur. Il vit avec son meilleur ami Turk, qui lui est chirurgien dans le même hôpital. Très vite, Turk tombe amoureux d'une infirmière Carla. Elliot entre dans la bande. C'est une étudiante en médecine quelque peu surprenante. Le service de médecine est dirigé par l'excentrique Docteur Cox alors que l'hôpital est géré par le diabolique Docteur Kelso. A cela viennent s'ajouter plein de personnages hors du commun : Todd le chirurgien obsédé, Ted l'avocat dépressif, le concierge qui trouve toujours un moyen d'embêter JD... Une belle galerie de personnage !",
                9, 184, 2001, "ABC (US)");
                controller.PostSerie(expected);
            }
            controller.DeleteSerie(789);
        }
    }
}