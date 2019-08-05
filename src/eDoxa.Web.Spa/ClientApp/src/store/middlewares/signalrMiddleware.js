export function middleware() {
  return store => next => action => {
    return next(action);
  };
}
