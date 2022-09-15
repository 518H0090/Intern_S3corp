function pluck(array, property) {
  return array.map(function (element) {
    return element[property];
  });
}
