class Monster {
  constructor(options) {
    this.health = 100;
    this.name = options.name;
  }
}

class Snake extends Monster {
  bite(targetSnake) {
    targetSnake.health -= 10;
  }
}
