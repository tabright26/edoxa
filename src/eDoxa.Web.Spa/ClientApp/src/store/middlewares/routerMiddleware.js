import { createBrowserHistory } from 'history';

import { routerMiddleware } from 'connected-react-router';

export const history = createBrowserHistory();

export function middleware() {
  return routerMiddleware(history);
}
