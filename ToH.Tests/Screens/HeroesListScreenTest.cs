using System.Collections.Generic;
using Moq;
using ToH.Data;
using ToH.Log;
using ToH.Screens;
using Xunit;

namespace ToH.Tests.Screens;


public class HeroesListScreenTest
{
    private HeroesListScreen uut;
    private Mock<IDatabase> _db;
    private Mock<ILog> _log;
    private Mock<IPrinter> _printer;

    public HeroesListScreenTest()
    {
        _db = new Mock<IDatabase>();
        _log = new Mock<ILog>();
        _printer = new Mock<IPrinter>();
        uut = new HeroesListScreen(_db.Object, _log.Object, _printer.Object);
    }

    [Fact]
    public void ShouldPrintTowLines_WithNoActionAndOneHero()
    {
        // Arrange
        _db.Setup(db => db.GetAllHeroes()).Returns(new List<Hero>()
        {
            new () { Id = 1, Name = "TestHero1"},
        });
        
        // Act
        uut.Print(Action.None);

        // Assert
        _printer.Verify(printer => printer.PrintLine(
                It.IsAny<string>()),
            Times.Exactly(2));
    }

    [Fact]
    public void ShouldAddCursorOnFirstHero_WhenNoActionIsGiven()
    {
        // Arrange
        _db.Setup(db => db.GetAllHeroes()).Returns(new List<Hero>()
        {
            new () { Id = 1, Name = "TestHero1"},
        });
        
        // Act
        uut.Print(Action.None);

        // Assert
        _printer.Verify(printer => printer.PrintLine(
            It.Is<string>(s => s.Contains("*"))), 
            Times.Exactly(1));
    }

    [Fact]
    public void ShouldShowHeroNameInUppercase()
    {
        // Arrange
        _db.Setup(db => db.GetAllHeroes()).Returns(new List<Hero>()
        {
            new () { Id = 1, Name = "TestHero1"},
        });
        
        // Act
        uut.Print(Action.None);

        // Assert
        _printer.Verify(printer => printer.PrintLine(
                It.Is<string>(s => s.Contains("TESTHERO1"))), 
            Times.Exactly(1));   
    }


}