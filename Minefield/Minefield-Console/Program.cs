using MinefieldGameConsole;

var boardWidth = 8;
var boardHeight = 8;
var numberOfMines = 15;
var playerMaxLives = 3;
var defaultStartPosition = new Position(0, 0); // A1 on the board
var randomStartPosition = true; // set to false to start at A1
var randomTargetPosition = true; // set to false to set the target to any position on row 8

var randomPositions = new Randomise(randomStartPosition, randomTargetPosition);

var boardConfig = new BoardConfiguration(new Dimensions(boardWidth, boardHeight), numberOfMines, randomPositions, defaultStartPosition);
var playerConfig = new PlayerConfiguration(playerMaxLives);

var configuration = new Configuration(boardConfig, playerConfig);
var consoleRenderer = new ConsoleRenderer();


var gameBoard = new GameBoard(configuration, consoleRenderer);

new GameEngine(configuration).Run(gameBoard, new GamePlayer(gameBoard, consoleRenderer, playerMaxLives));
