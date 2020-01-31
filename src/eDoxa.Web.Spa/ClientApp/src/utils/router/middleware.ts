import { routerMiddleware } from "connected-react-router";

import { history } from "utils/router/history";

export const middleware = routerMiddleware(history);
