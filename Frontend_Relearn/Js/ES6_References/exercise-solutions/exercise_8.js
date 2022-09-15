function reject(array, iteratorFunction) {
  return array.filter((element) => {
    return !iteratorFunction(element);
  });
}
