import { createBrowserHistory } from 'history';

import { routerMiddleware, connectRouter } from 'connected-react-router';

export const history = createBrowserHistory();

export const middleware = routerMiddleware(history);

export const reducer = connectRouter(history);
