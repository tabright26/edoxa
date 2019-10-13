import { routerMiddleware, connectRouter } from "connected-react-router";

import { history } from "utils/history";

export const middleware = routerMiddleware(history);

export const reducer = connectRouter(history);
